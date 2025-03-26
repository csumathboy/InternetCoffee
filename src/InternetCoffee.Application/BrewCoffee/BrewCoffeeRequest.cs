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
            
            return await Task.FromResult(respone);
        }

    }

}
