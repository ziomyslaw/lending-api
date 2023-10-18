using Lending.Application;
using Lending.Infrastructure;
using Lending.Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.MapEndpoints();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.Run();

public partial class Program { }