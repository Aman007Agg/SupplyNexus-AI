var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
