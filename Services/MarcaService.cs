using Microsoft.EntityFrameworkCore;
using Tiendita.Data;
using Tiendita.DTOs;
using Tiendita.Models;

namespace Tiendita.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly AppDbContext _db;
        public MarcaService(AppDbContext db) => _db = db;

        public async Task<List<MarcaDto>> GetAllAsync() =>
            await _db.Marcas.AsNoTracking()
                .Select(m => new MarcaDto { IdMarca = m.IdMarca, Descripcion = m.Descripcion })
                .ToListAsync();

        public async Task<MarcaDto?> GetByIdAsync(int id)
        {
            var m = await _db.Marcas.AsNoTracking().FirstOrDefaultAsync(x => x.IdMarca == id);
            return m == null ? null : new MarcaDto { IdMarca = m.IdMarca, Descripcion = m.Descripcion };
        }

        public async Task<int> CreateAsync(CreateMarcaDto dto)
        {
            var entity = new Marca { Descripcion = dto.Descripcion };
            _db.Marcas.Add(entity);
            await _db.SaveChangesAsync();
            return entity.IdMarca;
        }

        public async Task<bool> UpdateAsync(int id, UpdateMarcaDto dto)
        {
            if (id != dto.IdMarca) return false;
            var m = await _db.Marcas.FindAsync(id);
            if (m == null) return false;
            m.Descripcion = dto.Descripcion;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var m = await _db.Marcas.FindAsync(id);
            if (m == null) return false;
            _db.Marcas.Remove(m);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}