using ApiFinance.Models;

namespace ApiFinance.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
