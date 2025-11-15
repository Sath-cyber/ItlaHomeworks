using System.ComponentModel.DataAnnotations;

namespace Physio.Application.Dtos
{
    public class PacienteDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Telefono { get; set; } = string.Empty;

        [Range(1, 120)]
        public int Edad { get; set; }
    }
}
