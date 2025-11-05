using System.ComponentModel.DataAnnotations;

namespace Tiendita.DTOs
{
    public class UserDto
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public int? IdRol { get; set; }
        public string? RolNombre { get; set; }
        public bool? Estado { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public int? IdRol { get; set; }
        public bool? Estado { get; set; } = true;
    }

    public class UpdateUserDto
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public int? IdRol { get; set; }
        public bool? Estado { get; set; }
    }

    public class ChangePasswordDto
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string Password { get; set; } = null!;
    }
}