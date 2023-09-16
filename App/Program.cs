using Application.Extensions;
using Infrastructure.DependencyExtension;
using Infrastructure.SqlDatabase;
using Presentation;
using Serilog;
using static Infrastructure.SerilogConfig.SerilogConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.AddSerilog();

builder.Services.RegisterInfrastructureDependencies();
builder.Services.AddControllers().AddApplicationPart(typeof(PresentationAssembly).Assembly);

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
