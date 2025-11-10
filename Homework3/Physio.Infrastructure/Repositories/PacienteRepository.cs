using Physio.Domain.Entities;
using Physio.Infrastructure.Context;
using Physio.Infrastructure.Core;
using Physio.Infrastructure.Interfaces;

namespace Physio.Infrastructure.Repositories;

public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
{
    public PacienteRepository(PhysioContext db) : base(db) { }
}
