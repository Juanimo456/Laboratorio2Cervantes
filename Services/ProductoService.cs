using Microsoft.EntityFrameworkCore;
using Tiendita.Data;
using Tiendita.DTOs;
using Tiendita.Models;

namespace Tiendita.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _db;

        public ProductoService(AppDbContext db) => _db = db;

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _db.Productos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Select(p => new ProductDto
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    PrecioUnidad = p.PrecioUnidad,
                    Descripcion = p.Descripcion,
                    ImagenBase64 = p.Imagen != null ? Convert.ToBase64String(p.Imagen) : null,
                    Calificacion = p.Calificacion,
                    UnidadesDisponibles = p.UnidadesDisponibles,
                    IdCategoria = p.IdCategoria,
                    IdMarca = p.IdMarca,
                    CategoriaNombre = p.Categoria != null ? p.Categoria.Descripcion : null,
                    MarcaNombre = p.Marca != null ? p.Marca.Descripcion : null,
                    Estado = p.Estado
                })
                .ToListAsync();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _db.Productos
                .AsNoTracking()
                .Include(x => x.Categoria)
                .Include(x => x.Marca)
                .FirstOrDefaultAsync(x => x.IdProducto == id);

            if (p == null) return null;

            return new ProductDto
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                PrecioUnidad = p.PrecioUnidad,
                Descripcion = p.Descripcion,
                ImagenBase64 = p.Imagen != null ? Convert.ToBase64String(p.Imagen) : null,
                Calificacion = p.Calificacion,
                UnidadesDisponibles = p.UnidadesDisponibles,
                IdCategoria = p.IdCategoria,
                IdMarca = p.IdMarca,
                CategoriaNombre = p.Categoria?.Descripcion,
                MarcaNombre = p.Marca?.Descripcion,
                Estado = p.Estado
            };
        }

        public async Task<int> CreateAsync(CreateProductDto input)
        {
            var entity = new Producto
            {
                NombreProducto = input.NombreProducto,
                PrecioUnidad = input.PrecioUnidad,
                Descripcion = input.Descripcion,
                Imagen = string.IsNullOrWhiteSpace(input.ImagenBase64) ? null : Convert.FromBase64String(input.ImagenBase64),
                Calificacion = input.Calificacion,
                UnidadesDisponibles = input.UnidadesDisponibles,
                IdCategoria = input.IdCategoria,
                IdMarca = input.IdMarca,
                Estado = input.Estado ?? true
            };

            _db.Productos.Add(entity);
            await _db.SaveChangesAsync();
            return entity.IdProducto;
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto input)
        {
            if (id != input.IdProducto) return false;

            var p = await _db.Productos.FindAsync(id);
            if (p == null) return false;

            p.NombreProducto = input.NombreProducto;
            p.PrecioUnidad = input.PrecioUnidad;
            p.Descripcion = input.Descripcion;
            if (!string.IsNullOrWhiteSpace(input.ImagenBase64))
                p.Imagen = Convert.FromBase64String(input.ImagenBase64);
            p.Calificacion = input.Calificacion;
            p.UnidadesDisponibles = input.UnidadesDisponibles;
            p.IdCategoria = input.IdCategoria;
            p.IdMarca = input.IdMarca;
            if (input.Estado.HasValue)
                p.Estado = input.Estado;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) return false;

            _db.Productos.Remove(p);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetEstadoAsync(int id, bool estado)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p == null) return false;

            p.Estado = estado;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}