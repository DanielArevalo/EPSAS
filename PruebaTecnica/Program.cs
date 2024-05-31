using DataBase;
using EPSA.Services.Interfaces;
using EPSA.Services.Services;
using EPSAS.Repository.Interfaces;
using EPSAS.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PruebaTecnica;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EPSAContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EPSAConnection")));

// Add services to the container
builder.Services.AddTransient<IHistoricalRepository, HistoricalRepository>();
builder.Services.AddTransient<IHistoricalService, HistoricalService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EPSAContext>();
    context.Database.Migrate();
    SeedData.Initialize(scope.ServiceProvider);
}

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
