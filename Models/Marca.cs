using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiendita.Models
{
    [Table("Marcas")]
    public class Marca
    {
        [Key]
        [Column("ID_Marca")]
        public int IdMarca { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Descripcion")]
        public string Descripcion { get; set; } = null!;

        public ICollection<Producto>? Productos { get; set; }
    }
}