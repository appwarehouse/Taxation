using Microsoft.EntityFrameworkCore;
using Taxation.Data;
using Taxation.Repositories;
using Taxation.Services;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("TaxationDBConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonalTax, TaxCalculator>();
builder.Services.AddScoped<ITaxCalculationRepository, TaxCalculationsRepository>();
builder.Services.AddDbContext<TaxationDBContext>(options =>
        options.UseSqlServer(conn));

var app = builder.Build();

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
