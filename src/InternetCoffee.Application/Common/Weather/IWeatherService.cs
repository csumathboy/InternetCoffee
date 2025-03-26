using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCoffee.Application.Common.Weather
{
    public interface IWeatherService
    {
        /// <summary>
        /// Get Temperature
        /// </summary>
        /// <returns></returns>
        Task<double> GetCurrentTemperatureAsync(string? city);
        /// <summary>
        /// Get UserCity by request IP
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserCityAsync(string? userip);
    }
}
