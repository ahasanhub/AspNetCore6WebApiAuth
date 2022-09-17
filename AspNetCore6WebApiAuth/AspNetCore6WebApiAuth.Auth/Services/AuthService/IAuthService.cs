using AspNetCore6WebApiAuth.Auth.Data.Dtos;
using AspNetCore6WebApiAuth.Auth.Data.Models;

namespace AspNetCore6WebApiAuth.Auth.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> RegisterUser(UserDto userDto);
        Task<AuthResponseDto> Login(LoginDto request);
        Task<AuthResponseDto> RefreshToken();
    }
}
