using Microsoft.AspNetCore.Mvc;
using Tiendita.DTOs;
using Tiendita.Services;

namespace Tiendita.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _service.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost] // no [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductDto input)
        {
            var id = await _service.CreateAsync(input);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:int}")] // no [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto input)
        {
            if (!await _service.UpdateAsync(id, input)) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id:int}")] // no [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _service.DeleteAsync(id)) return NotFound();
            return NoContent();
        }
    }
}