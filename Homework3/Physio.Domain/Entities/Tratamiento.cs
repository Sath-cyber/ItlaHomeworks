using Physio.Domain.Core;

namespace Physio.Domain.Entities;

public class Tratamiento : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public decimal Costo { get; set; }
    public int DuracionMinutos { get; set; }

    public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
}
