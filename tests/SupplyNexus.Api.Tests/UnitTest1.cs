
using Microsoft.AspNetCore.Mvc.Testing;

namespace SupplyNexus.Api.Tests;

public sealed class HealthEndpointTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHealth_ReturnsHealthyResponse()
    {
        var response = await _client.GetAsync("/api/health");

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("Healthy", body);
        Assert.Contains("SupplyNexus.Api", body);
    }
}
