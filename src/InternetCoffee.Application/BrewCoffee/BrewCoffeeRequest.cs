using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InternetCoffee.Application.Common.Caching;
using InternetCoffee.Application.Common.Weather;
using MediatR;

namespace InternetCoffee.Application.BrewCoffee
{
    /// <summary>
    /// brew coffee Query
    /// </summary>
    public class BrewCoffeeRequest : IRequest<BrewCoffeeResponse>
    {
        public DateTime? requestTime { get; set; }

    }
    /// <summary>
    /// brew coffee Response
    /// </summary>
    public class BrewCoffeeQueryHandler : IRequestHandler<BrewCoffeeRequest, BrewCoffeeResponse>
    {
        /// <summary>
        /// Cache service
        /// </summary>
        private readonly ICacheService _cacheService;

        private readonly IWeatherService _weatherService;
        /// <summary>
        /// constructor initializes cache service and other dependencies
        /// </summary>
        /// <param name="cacheService"></param>
        public BrewCoffeeQueryHandler(ICacheService cacheService,IWeatherService weatherService)
        {
            _cacheService = cacheService;
            _weatherService = weatherService;
        }
        /// <summary>
        /// handle brew coffee request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BrewCoffeeResponse> Handle(BrewCoffeeRequest request, CancellationToken cancellationToken)
        {
            var respone = new BrewCoffeeResponse();
            DateTime dateTime = request.requestTime ?? DateTime.UtcNow;
            // April fools day
            if (dateTime.Month == 4 && dateTime.Day == 1)
            {
                respone.StatusCode = 418;
                respone.Message = string.Empty;
                return await Task.FromResult(respone);
            }
            //get request count fifth request of 200 will be 418
            int requestCount = _cacheService.GetData<int>("requestCount");
            requestCount++;
            if (requestCount % 5 == 0)
            {
                respone.StatusCode = 503;
                respone.Message = string.Empty;
            }
            //ordinary request
            else
            {
                respone= OrdinaryResponse(dateTime).Result;
            }
            //update cache
            _cacheService.SetData("requestCount", requestCount);

            //return response
            return await Task.FromResult(respone);
        }

        /// <summary>
        /// Ordinary response
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private async Task<BrewCoffeeResponse> OrdinaryResponse(DateTime dateTime)
        {
            var respone = new BrewCoffeeResponse();
            var city = await _weatherService.GetUserCityAsync(string.Empty);
            var temperature = await _weatherService.GetCurrentTemperatureAsync(city);
            #if DEBUG
            //temperature = 40;
            #endif
            if (Math.Floor(temperature) > 30)
            {
                respone.Message = "Your refreshing iced coffee is ready";
            }
            else
            {
                respone.Message = "Your piping hot coffee is ready";
            }

            respone.StatusCode = 200;
            respone.Prepared = dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            return respone;
        }
    }

}

