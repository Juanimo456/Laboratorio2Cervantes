using System.ComponentModel.DataAnnotations;

namespace Tiendita.DTOs
{
    public class ProductDto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public decimal PrecioUnidad { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenBase64 { get; set; }
        public decimal? Calificacion { get; set; }
        public int? UnidadesDisponibles { get; set; }

        public int? IdCategoria { get; set; }
        public int? IdMarca { get; set; }

        // Por conveniencia, nombres relacionados
        public string? CategoriaNombre { get; set; }
        public string? MarcaNombre { get; set; }

        public bool? Estado { get; set; }
    }

    public class CreateProductDto
    {
        [Required, MaxLength(150)]
        public string NombreProducto { get; set; } = null!;
        public decimal PrecioUnidad { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenBase64 { get; set; }
        public decimal? Calificacion { get; set; }
        public int? UnidadesDisponibles { get; set; }

        public int? IdCategoria { get; set; }
        public int? IdMarca { get; set; }

        public bool? Estado { get; set; } = true;
    }

    public class UpdateProductDto : CreateProductDto
    {
        [Required]
        public int IdProducto { get; set; }
    }
}