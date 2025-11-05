using System.ComponentModel.DataAnnotations;

namespace Tiendita.DTOs
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = null!;
    }

    public class CreateCategoriaDto
    {
        [Required, MaxLength(100)]
        public string Descripcion { get; set; } = null!;
    }

    public class UpdateCategoriaDto : CreateCategoriaDto
    {
        [Required]
        public int IdCategoria { get; set; }
    }
}