using csumathboy.Infrastructure.Caching;
using InternetCoffee.Application.Common.Caching;
using InternetCoffee.Application.Common.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InternetCoffee.Infrastructure.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        private readonly WeatherSettings? _weatherSettings;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICacheService _cacheService;

        public WeatherService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor,ICacheService cacheService)
        {
            // Bind WeatherSettings from configuration

            var settings = configuration.GetSection(nameof(WeatherSettings)).Get<WeatherSettings>();
            _weatherSettings = settings;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;
        }
        /// <summary>
        /// Get CurrentXRealIp
        /// </summary>
        /// <returns></returns>
        private string GetCurrentXRealIp()
        {
            string userIP = string.Empty;
            if (_httpContextAccessor.HttpContext == null)
            {
                return "127.0.0.1";
            }
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"]))
                userIP = Convert.ToString(_httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"]);
            if (string.IsNullOrEmpty(userIP))
            {
                userIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            }
            return userIP;
        }
        /// <summary>
        /// return md5 hash of input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// Get Temperature
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetCurrentTemperatureAsync(string? city)
        {
            if (_weatherSettings == null)
            {
                return 0;
            }
            string requestUrl = string.Format(_weatherSettings.WeatherBaseUrl, _weatherSettings.WeatherApiKey, city);
            string cacheKey= GetMd5Hash(requestUrl);
            // get temperature from cache
            double temp = _cacheService.GetData<double>(cacheKey);
            if(temp > 0)
            {
                return temp;
            }
            // get temperature from api
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }
            var responseText = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseText))
            {
                return 0;
            }
          
            using var doc = JsonDocument.Parse(responseText);
            temp = doc.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
            if(temp> 0)
            {
                temp -= 273.15; // Convert to Celsius
                _cacheService.SetData(cacheKey, temp); //set cache
            }
            return temp;
        }
        /// <summary>
        /// Get UserCity
        /// local request will return Melbourne
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserCityAsync(string? userip)
        {
            string userCity = "Melbourne";
            if (_weatherSettings == null)
            {
                return userCity;
            }
            //get Request IP from header if userip is null
            if (string.IsNullOrEmpty(userip))
            {
                userip = GetCurrentXRealIp();
            }

            string requestUrl = string.Format(_weatherSettings.LocationBaseUrl, _weatherSettings.LocationApiKey, userip);

            //get user city from cache
            string cacheKey = GetMd5Hash(requestUrl);
            var cacheData = _cacheService.GetData<string>(cacheKey);
            if(!string.IsNullOrEmpty(cacheData))
            {
                return cacheData;
            }
            //get user city from api
            var response= await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return userCity;
            }
            var responseText = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseText))
            {
                return userCity;
            }
            var list = responseText.Split(";");
            if (list.Length > 6 && !list[6].Equals("-"))
            {
                userCity = list[6];
            }
            _cacheService.SetData(cacheKey, userCity);
            return userCity;
        }
    }
}
