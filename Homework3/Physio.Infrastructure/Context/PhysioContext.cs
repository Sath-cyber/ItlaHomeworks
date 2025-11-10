using Microsoft.EntityFrameworkCore;
using Physio.Domain.Entities;

namespace Physio.Infrastructure.Context;

public class PhysioContext : DbContext
{
    public PhysioContext(DbContextOptions<PhysioContext> options) : base(options) { }

    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Fisioterapeuta> Fisioterapeutas => Set<Fisioterapeuta>();
    public DbSet<Tratamiento> Tratamientos => Set<Tratamiento>();
    public DbSet<Sesion> Sesiones => Set<Sesion>();
}
