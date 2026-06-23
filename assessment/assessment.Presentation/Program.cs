using assessment.Application;
using assessment.Infrastructure;
using assessment.Presentation;
using assessment.Presentation.Endpoints;
using assessment.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.MapOrderEndpoints();
app.MapCustomerEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

