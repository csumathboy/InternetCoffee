using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace InternetCoffee.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BrewCoffeeController : ControllerBase
{

    private readonly ILogger<BrewCoffeeController> _logger;

    public BrewCoffeeController(ILogger<BrewCoffeeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<BrewCoffee> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new BrewCoffee
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55)
        })
        .ToArray();
    }

    [HttpGet("brew-coffee")]
    public async Task<IActionResult> BrewCoffeeAsync()
    {

        return StatusCode(503, "Service Unavailable");
    }
}