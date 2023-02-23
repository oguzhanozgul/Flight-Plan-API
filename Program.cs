global using FlightPlanApi.Models;
global using FlightPlanApi.Services.AirportService;
global using Microsoft.EntityFrameworkCore;

using FlightPlanApi.Services.AirportService;
using FlightPlanApi.Services.ConnectionService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
