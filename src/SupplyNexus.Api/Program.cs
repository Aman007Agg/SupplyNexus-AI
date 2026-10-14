using Npgsql;
using SupplyNexus.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SupplyNexus") ?? throw new InvalidOperationException("ConnectionStrings:SupplyNexus is not configured."); 

builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));

builder.Services.AddScoped<InventoryService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var tenantValue = builder.Configuration["DemoTenantId"] ?? throw new InvalidOperationException("DemoTenantId is not configured.");
    var demoTenantId = Guid.Parse(tenantValue);
    app.MapGet("/api/inventory/shortfalls",
        async (
            InventoryService service,
            CancellationToken cancellationToken) =>
        {
            var results = await service.GetShortfallsAsync(
                demoTenantId, cancellationToken);

            return Results.Ok(results);
        })
        .WithName("GetInventoryShortfalls")
        .WithTags("Inventory");

    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "SupplyNexus API v1"));
}

// Health endpoint: confirms that the API process is responding.
app.MapGet("/api/health", () => Results.Ok(new
{
    status = "Healthy",
    service = "SupplyNexus.Api"
})).WithName("GetHealth").WithTags("Health");

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }