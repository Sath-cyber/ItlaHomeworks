using System.ComponentModel.DataAnnotations;

namespace Physio.Application.Dtos
{
    public class TratamientoDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0.01, 9999999)]
        public decimal Costo { get; set; }

        [Range(1, 600)]
        public int DuracionMinutos { get; set; }
    }
}
