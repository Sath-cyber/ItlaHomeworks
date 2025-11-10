using Physio.Domain.Core;

namespace Physio.Domain.Entities;

public class Sesion : BaseEntity
{
    public DateTime FechaHora { get; set; }
    public string? Notas { get; set; }

    public int PacienteId { get; set; }
    public int FisioterapeutaId { get; set; }
    public int TratamientoId { get; set; }

    public Paciente? Paciente { get; set; }
    public Fisioterapeuta? Fisioterapeuta { get; set; }
    public Tratamiento? Tratamiento { get; set; }
}
