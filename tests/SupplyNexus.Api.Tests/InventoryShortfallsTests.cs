
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SupplyNexus.Api.Tests;

public sealed class InventoryShortfallsTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public InventoryShortfallsTests(
        WebApplicationFactory<Program> factory)
    {
        var configuredFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["DemoTenantId"] =
                            "11111111-1111-4111-8111-111111111111"
                    });
            });
        });

        _client = configuredFactory.CreateClient();
    }

    [Fact]
    public async Task GetShortfalls_ReturnsExpectedSeededShortfall()
    {
        var response = await _client.GetAsync(
            "/api/inventory/shortfalls");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var results = await response.Content
            .ReadFromJsonAsync<List<InventoryShortfallDto>>();

        Assert.NotNull(results);

        var item = Assert.Single(results);

        Assert.Equal("ORD-10001", item.OrderNumber);
        Assert.Equal("SKU-10023", item.Sku);
        Assert.Equal(12, item.OrderedQuantity);
        Assert.Equal(8, item.AvailableQuantity);
        Assert.Equal(4, item.QuantityShortfall);
    }

    private sealed record InventoryShortfallDto(
        string OrderNumber,
        string Sku,
        int OrderedQuantity,
        int OnHandQuantity,
        int ReservedQuantity,
        int AvailableQuantity,
        int QuantityShortfall);
}
