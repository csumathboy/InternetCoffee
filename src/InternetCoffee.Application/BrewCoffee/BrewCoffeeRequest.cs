using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetCoffee.Application.Common.Caching;
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
        /// <summary>
        /// constructor initializes cache service and other dependencies
        /// </summary>
        /// <param name="cacheService"></param>
        public BrewCoffeeQueryHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
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
                respone.StatusCode = 200;
                respone.Message = "Your piping hot coffee is ready";
                respone.Prepared = dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            }
            //update cache
            _cacheService.SetData("requestCount", requestCount);

            //return response
            return await Task.FromResult(respone);
        }

    }

}
