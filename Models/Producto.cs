using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiendita.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreProducto { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnidad { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        public byte[]? Imagen { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? Calificacion { get; set; }

        public int? UnidadesDisponibles { get; set; }

        // FKs hacia tablas relacionadas
        [Column("Id_Categoria")]
        public int? IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }

        [Column("ID_Marca")]
        public int? IdMarca { get; set; }
        public Marca? Marca { get; set; }

        [Column("Estado")]
        public bool? Estado { get; set; } = true;
    }
}