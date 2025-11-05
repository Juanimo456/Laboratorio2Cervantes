using System.ComponentModel.DataAnnotations;

namespace Tiendita.DTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string Email { get; set; } = null!;
        public int IdUsuario { get; set; }
        public string? Role { get; set; }
    }

    public class RegisterUserDto
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public int? IdRol { get; set; }
    }
}