using Physio.Domain.Core;

namespace Physio.Domain.Entities;

public class Fisioterapeuta : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string Apellido { get; set; } = default!;
    public string Especialidad { get; set; } = default!;
    public string Telefono { get; set; } = default!;

    public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
}
