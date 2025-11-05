using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiendita.DTOs;
using Tiendita.Services;

namespace Tiendita.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuariosController(IUsuarioService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var id = await _service.CreateAsync(dto);
            if (id <= 0) return Conflict("Email already exists.");
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            return await _service.UpdateAsync(id, dto) ? NoContent() : BadRequest();
        }

        [Authorize]
        [HttpPut("{id:int}/password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            if (id != dto.IdUsuario) return BadRequest();
            return await _service.ChangePasswordAsync(id, dto.Password) ? NoContent() : NotFound();
        }

        // Logical delete: Estado = false
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id) ? NoContent() : NotFound();
        }

        [Authorize]
        [HttpPut("{id:int}/estado")]
        public async Task<IActionResult> SetEstado(int id, [FromQuery] bool estado)
        {
            return await _service.SetEstadoAsync(id, estado) ? NoContent() : NotFound();
        }
    }
}