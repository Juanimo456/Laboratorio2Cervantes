using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiendita.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [MaxLength(100)]
        public string? Nombre { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(255)]
        [Column("Contrasena")]
        public string Contrasena { get; set; } = null!;

        [Column("IdRol")]
        public int? IdRol { get; set; }

        [ForeignKey(nameof(IdRol))]
        public Rol? Rol { get; set; }

        [Column("Estado")]
        public bool? Estado { get; set; } = true;
    }
}