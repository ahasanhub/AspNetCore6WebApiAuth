using AspNetCore6WebApiAuth.Auth.Data.Extensions;
using AspNetCore6WebApiAuth.Auth.Data.Models;
using System.Data.SqlClient;

namespace AspNetCore6WebApiAuth.Auth.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration, ILogger<BaseRepository<User>> logger) : base(configuration, logger)
        {
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            string query = @"SELECT [Id],[UserName],[Email],[PasswordHash],[PasswordSalt],[SecurityId],[VerificationToken],[VerifiedAt],
                           [RefreshToken],[TokenCreated],[TokenExpires]  FROM [CommonAuthDB].[dbo].[User] WHERE [UserName]=@Id";
            return await GetById(UserMapToValue,query,userName);
        }
        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            string query = @"SELECT [Id],[UserName],[Email],[PasswordHash],[PasswordSalt],[SecurityId],[VerificationToken],[VerifiedAt],
                           [RefreshToken],[TokenCreated],[TokenExpires]  FROM [CommonAuthDB].[dbo].[User] WHERE [RefreshToken]=@Id";
            return await GetById(UserMapToValue, query, refreshToken);
        }
        private User UserMapToValue(SqlDataReader reader)
        {

            return new User()
            {                
                Id = reader.Get<int>("Id"),
                Username = reader.Get<string>("Username"),
                Email = reader.Get<string>("Email"),
                PasswordHash = reader.Get<byte[]>("PasswordHash"),
                PasswordSalt = reader.Get<byte[]>("PasswordSalt"),
                SecurityId = new Guid(reader.Get<string>("SecurityId")),
                VerificationToken = reader.Get<string>("VerificationToken"),
                VerifiedAt = reader.Get<DateTime?>("VerifiedAt"),
                RefreshToken = reader.Get<string>("RefreshToken"),
                TokenCreated = reader.Get<DateTime?>("TokenCreated"),
                TokenExpires = reader.Get<DateTime?>("TokenExpires"),
                
            };
        }
        public async Task<object?> InsertUser(User user)
        {
            string query = @"INSERT INTO [dbo].[User]([UserName],[Email],[PasswordHash],[PasswordSalt],[SecurityId],[VerificationToken],[VerifiedAt],[RefreshToken],
                            [TokenCreated],[TokenExpires])
                            VALUES (@UserName,@Email,@PasswordHash,@PasswordSalt,@SecurityId,@VerificationToken,@VerifiedAt,@RefreshToken,@TokenCreated,@TokenExpires)"
                                + "SELECT CAST(scope_identity() AS int)";

            return (await GetSingleValueAsync(query, cmd =>
            {                
                cmd.Parameters.Add(new SqlParameter("@UserName", user.Username));
                cmd.Parameters.Add(new SqlParameter("@Email", user.Email.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@PasswordHash", user.PasswordHash));
                cmd.Parameters.Add(new SqlParameter("@PasswordSalt", user.PasswordSalt));
                cmd.Parameters.Add(new SqlParameter("@SecurityId", user.SecurityId));
                cmd.Parameters.Add(new SqlParameter("@VerificationToken", user.VerificationToken.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@VerifiedAt", user.VerifiedAt.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@RefreshToken", user.RefreshToken.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@TokenCreated", user.TokenCreated.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@TokenExpires", user.TokenExpires.DBNullHandler()));

            }));
        }
        public async Task UpdateUserRefreshToken(int id,User user)
        {
            string query = @"UPDATE [dbo].[User]   SET [RefreshToken] = @RefreshToken,[TokenCreated] = @TokenCreated,[TokenExpires] = @TokenExpires
                                     WHERE [Id]=@Id";                               

            await UpdateAsync(query, cmd =>
            {
                cmd.Parameters.Add(new SqlParameter("@Id", id));                
                cmd.Parameters.Add(new SqlParameter("@RefreshToken", user.RefreshToken.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@TokenCreated", user.TokenCreated.DBNullHandler()));
                cmd.Parameters.Add(new SqlParameter("@TokenExpires", user.TokenExpires.DBNullHandler()));
            });
        }
    }
}
