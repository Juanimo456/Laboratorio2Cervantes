using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiendita.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        [Column("Id_Categoria")]
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Descripcion")]
        public string Descripcion { get; set; } = null!;

        public ICollection<Producto>? Productos { get; set; }
    }
}