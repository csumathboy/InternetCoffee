using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCoffee.Application.BrewCoffee
{
    public class BrewCoffeeResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Prepared { get; set; }

        public BrewCoffeeResponse() { }
        /// <summary>
        /// Constructor for BrewCoffeeResponse
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="prepared"></param>
        public BrewCoffeeResponse(int statusCode, string? message, string prepared)
        {
            StatusCode = statusCode;
            Message = message;
            Prepared = prepared;
        }
    }
}
