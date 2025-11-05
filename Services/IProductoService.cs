using Tiendita.DTOs;

namespace Tiendita.Services
{
    public interface IProductoService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateProductDto input);
        Task<bool> UpdateAsync(int id, UpdateProductDto input);
        Task<bool> DeleteAsync(int id);

        Task<bool> SetEstadoAsync(int id, bool estado);
    }
}