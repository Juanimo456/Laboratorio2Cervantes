using Tiendita.DTOs;
using Tiendita.Models;

namespace Tiendita.Services
{
    public interface IUsuarioService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUserDto dto);
        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);                   // Logical delete (Estado = false)
        Task<bool> SetEstadoAsync(int id, bool estado);
        Task<bool> ChangePasswordAsync(int id, string newPassword);

        // Auth helpers
        Task<Usuario?> ValidateCredentialsAsync(string email, string password);
        Task<int> RegisterAsync(RegisterUserDto dto);    // Uses AuthDtos.RegisterUserDto
    }
}