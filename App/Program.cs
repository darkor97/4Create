using Application.Extensions;
using Infrastructure.DependencyExtension;
using Infrastructure.SqlDatabase;
using Presentation;
using Presentation.Extensions;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.AddSerilog();

builder.Services.RegisterInfrastructureDependencies();
builder.Services.RegisterPresentationDependencies();
builder.Services
    .AddControllers().AddApplicationPart(typeof(PresentationAssembly).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddMediatR();

var app = builder.Build();

app.Services.ApplyEFMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
