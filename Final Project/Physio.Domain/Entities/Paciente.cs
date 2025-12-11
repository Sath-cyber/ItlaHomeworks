using Physio.Domain.Core;

namespace Physio.Domain.Entities;

public class Paciente : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string Apellido { get; set; } = default!;
    public string Telefono { get; set; } = default!;
    public int Edad { get; set; }

    public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
}
