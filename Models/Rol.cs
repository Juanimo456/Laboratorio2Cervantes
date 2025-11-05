using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiendita.Models
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        [Column("IdRol")]
        public int IdRol { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("NombreRol")]
        public string NombreRol { get; set; } = null!;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}