using Microsoft.EntityFrameworkCore;
using Tiendita.Data;
using Tiendita.DTOs;
using Tiendita.Models;

namespace Tiendita.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _db;

        public UsuarioService(AppDbContext db) => _db = db;

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _db.Usuarios
                .AsNoTracking()
                .Include(u => u.Rol)
                .Select(u => new UserDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    IdRol = u.IdRol,
                    RolNombre = u.Rol != null ? u.Rol.NombreRol : null,
                    Estado = u.Estado
                }).ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var u = await _db.Usuarios
                .AsNoTracking()
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.IdUsuario == id);
            if (u == null) return null;

            return new UserDto
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Email = u.Email,
                IdRol = u.IdRol,
                RolNombre = u.Rol?.NombreRol,
                Estado = u.Estado
            };
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            var emailExists = await _db.Usuarios.AnyAsync(x => x.Email == dto.Email);
            if (emailExists) return -1;

            var entity = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IdRol = dto.IdRol,
                Estado = dto.Estado ?? true
            };

            _db.Usuarios.Add(entity);
            await _db.SaveChangesAsync();
            return entity.IdUsuario;
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            if (id != dto.IdUsuario) return false;

            var u = await _db.Usuarios.FindAsync(id);
            if (u == null) return false;

            if (!string.Equals(u.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var duplicate = await _db.Usuarios.AnyAsync(x => x.Email == dto.Email && x.IdUsuario != id);
                if (duplicate) return false;
            }

            u.Nombre = dto.Nombre;
            u.Email = dto.Email;
            u.IdRol = dto.IdRol;
            if (dto.Estado.HasValue) u.Estado = dto.Estado.Value;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var u = await _db.Usuarios.FindAsync(id);
            if (u == null) return false;

            u.Estado = false;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetEstadoAsync(int id, bool estado)
        {
            var u = await _db.Usuarios.FindAsync(id);
            if (u == null) return false;

            u.Estado = estado;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(int id, string newPassword)
        {
            var u = await _db.Usuarios.FindAsync(id);
            if (u == null) return false;

            u.Contrasena = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> ValidateCredentialsAsync(string email, string password)
        {
            var u = await _db.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (u == null) return null;
            if (u.Estado.HasValue && !u.Estado.Value) return null;

            return BCrypt.Net.BCrypt.Verify(password, u.Contrasena) ? u : null;
        }

        public async Task<int> RegisterAsync(RegisterUserDto dto)
        {
            var create = new CreateUserDto
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password,
                IdRol = dto.IdRol,
                Estado = true
            };

            return await CreateAsync(create);
        }
    }
}