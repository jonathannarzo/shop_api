using shop.Models;

namespace shop.Services;

public interface IAuthManager
{
    public Task<ApiUser> ValidateUser(string email);
    public Task<string> CreateToken();
    public Task<string> CreateRefreshToken();
    public bool ValidateRefreshToken(string token, string email);
}