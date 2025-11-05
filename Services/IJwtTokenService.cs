using Tiendita.Models;

namespace Tiendita.Services
{
    public interface IJwtTokenService
    {
        (string Token, DateTime Expires) CreateToken(Usuario user);
    }
}