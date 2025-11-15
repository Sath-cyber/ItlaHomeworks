using Physio.Domain.Core;
using Physio.Domain.Entities;
using Physio.Infrastructure.Context;
using Physio.Infrastructure.Core;
using Physio.Infrastructure.Interfaces;

namespace Physio.Infrastructure.Repositories
{
    public class FisioterapeutaRepository
        : BaseRepository<Fisioterapeuta>, IFisioterapeutaRepository
    {
        public FisioterapeutaRepository(PhysioContext db) : base(db) { }
    }
}
