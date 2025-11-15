using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Physio.Application.Contract;
using Physio.Application.Interfaces;
using Physio.Application.Service;
using Physio.Application.Services;
using Physio.Infrastructure.Context;
using Physio.Infrastructure.Interfaces;
using Physio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<IPacienteService>();

builder.Services.AddDbContext<PhysioContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("Physio.Infrastructure")));

builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IFisioterapeutaService, FisioterapeutaService>();
builder.Services.AddScoped<ITratamientoService, TratamientoService>();
builder.Services.AddScoped<ISesionService, SesionService>();

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IFisioterapeutaRepository, FisioterapeutaRepository>();
builder.Services.AddScoped<ITratamientoRepository, TratamientoRepository>();
builder.Services.AddScoped<ISesionRepository, SesionRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
