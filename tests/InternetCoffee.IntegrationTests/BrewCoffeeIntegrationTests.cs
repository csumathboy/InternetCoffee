using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using InternetCoffee.WebAPI;
using System.Net;
using System.Text.Json;
using System.Reflection.Metadata;
using System.Threading;

namespace InternetCoffee.IntegrationTests;

public class BrewCoffeeIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
 
    public BrewCoffeeIntegrationTests(WebApplicationFactory<Program> factory)
    {

        _client=factory.CreateClient();
    }

    [Fact]
    public async Task BrewCoffee_Get()
    {
        //request 1st time
        var response = await _client.GetAsync("/brew-coffee");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(content);
        Assert.Equal("Your piping hot coffee is ready", jsonResponse.GetProperty("message").GetString());

        //request 3 times
        for (int i = 1; i < 4; i++)
        {
            await _client.GetAsync("/brew-coffee");
        }
        // return 503 on 5th request
        response = await _client.GetAsync("/brew-coffee");
        Assert.Equal(StatusCodes.Status503ServiceUnavailable, (int)response.StatusCode);

        //return 200 on 6th request
        response = await _client.GetAsync("/brew-coffee");
        response.EnsureSuccessStatusCode();
        content = await response.Content.ReadAsStringAsync();
        jsonResponse = JsonSerializer.Deserialize<JsonElement>(content);

        Assert.Equal("Your piping hot coffee is ready", jsonResponse.GetProperty("message").GetString());

    }

     
}
