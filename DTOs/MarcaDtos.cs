using System.ComponentModel.DataAnnotations;

namespace Tiendita.DTOs
{
    public class MarcaDto
    {
        public int IdMarca { get; set; }
        public string Descripcion { get; set; } = null!;
    }

    public class CreateMarcaDto
    {
        [Required, MaxLength(100)]
        public string Descripcion { get; set; } = null!;
    }

    public class UpdateMarcaDto : CreateMarcaDto
    {
        [Required]
        public int IdMarca { get; set; }
    }
}