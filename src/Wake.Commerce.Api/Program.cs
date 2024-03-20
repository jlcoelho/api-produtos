using Wake.Commerce.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddConnection(builder.Configuration)
    .AddUseCases()
    .AddAndConfigureControllers();
var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

