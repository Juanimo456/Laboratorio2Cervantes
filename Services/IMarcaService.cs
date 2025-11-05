using Tiendita.DTOs;

namespace Tiendita.Services
{
    public interface IMarcaService
    {
        Task<List<MarcaDto>> GetAllAsync();
        Task<MarcaDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateMarcaDto dto);
        Task<bool> UpdateAsync(int id, UpdateMarcaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}