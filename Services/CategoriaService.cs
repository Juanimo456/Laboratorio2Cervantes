using Microsoft.EntityFrameworkCore;
using Tiendita.Data;
using Tiendita.DTOs;
using Tiendita.Models;

namespace Tiendita.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _db;
        public CategoriaService(AppDbContext db) => _db = db;

        public async Task<List<CategoriaDto>> GetAllAsync() =>
            await _db.Categorias.AsNoTracking()
                .Select(c => new CategoriaDto { IdCategoria = c.IdCategoria, Descripcion = c.Descripcion })
                .ToListAsync();

        public async Task<CategoriaDto?> GetByIdAsync(int id)
        {
            var c = await _db.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.IdCategoria == id);
            return c == null ? null : new CategoriaDto { IdCategoria = c.IdCategoria, Descripcion = c.Descripcion };
        }

        public async Task<int> CreateAsync(CreateCategoriaDto dto)
        {
            var entity = new Categoria { Descripcion = dto.Descripcion };
            _db.Categorias.Add(entity);
            await _db.SaveChangesAsync();
            return entity.IdCategoria;
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoriaDto dto)
        {
            if (id != dto.IdCategoria) return false;
            var c = await _db.Categorias.FindAsync(id);
            if (c == null) return false;
            c.Descripcion = dto.Descripcion;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var c = await _db.Categorias.FindAsync(id);
            if (c == null) return false;
            _db.Categorias.Remove(c);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}