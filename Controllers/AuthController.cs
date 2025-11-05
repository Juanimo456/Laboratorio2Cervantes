using Microsoft.AspNetCore.Mvc;
using Tiendita.DTOs;
using Tiendita.Services;

namespace Tiendita.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _users;
        private readonly IJwtTokenService _jwt;

        public AuthController(IUsuarioService users, IJwtTokenService jwt)
        {
            _users = users;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _users.ValidateCredentialsAsync(request.Email, request.Password);
            if (user == null) return Unauthorized();

            var (token, expires) = _jwt.CreateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                Expires = expires,
                Email = user.Email ?? string.Empty,
                IdUsuario = user.IdUsuario,
                Role = user.Rol?.NombreRol
            });
        }

        // Optional: public registration endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var id = await _users.RegisterAsync(dto);
            if (id <= 0) return Conflict("Email already exists.");
            return CreatedAtAction(nameof(Register), new { id }, new { id });
        }
    }
}