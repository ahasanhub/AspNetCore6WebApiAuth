using AspNetCore6WebApiAuth.Auth.Data.Models;

namespace AspNetCore6WebApiAuth.Auth.Data.Repository
{
    public interface IUserRepository
    {        
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task<object?> InsertUser(User user);
        Task UpdateUserRefreshToken(int id, User user);
    }
}
