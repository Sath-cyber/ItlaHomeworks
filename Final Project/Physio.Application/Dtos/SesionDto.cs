using System;
using System.ComponentModel.DataAnnotations;

namespace Physio.Application.Dtos
{
    public class SesionDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [StringLength(500)]
        public string? Notas { get; set; }

        [Range(1, int.MaxValue)]
        public int PacienteId { get; set; }

        [Range(1, int.MaxValue)]
        public int FisioterapeutaId { get; set; }

        [Range(1, int.MaxValue)]
        public int TratamientoId { get; set; }
    }
}
