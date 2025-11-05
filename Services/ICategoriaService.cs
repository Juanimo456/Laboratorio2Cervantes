using Tiendita.DTOs;

namespace Tiendita.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> GetAllAsync();
        Task<CategoriaDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateCategoriaDto dto);
        Task<bool> UpdateAsync(int id, UpdateCategoriaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}