using InternetCoffee.Application.BrewCoffee;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace InternetCoffee.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewCoffeeController : BaseApiController
{

    private readonly ILogger<BrewCoffeeController> _logger;

    public BrewCoffeeController(ILogger<BrewCoffeeController> logger)
    {
        _logger = logger;
    }

    [Route("/brew-coffee")]
    [HttpGet("brew-coffee")]
    public async Task<IActionResult> BrewCoffeeAsync()
    {
        var result = await Mediator.Send(new BrewCoffeeRequest());
        switch (result.StatusCode)
        {
            case 418:
                return StatusCode(418, "I'm a teapot");
            case 503:
                return StatusCode(503, "Service Unavailable");
            default:
                return new ObjectResult(new
                {
                    message = result.Message,
                    prepared = result.Prepared
                })
                {
                    StatusCode = 200
                };
        }
       
    }
}