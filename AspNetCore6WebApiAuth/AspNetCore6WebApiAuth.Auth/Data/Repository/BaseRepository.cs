using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCore6WebApiAuth.Auth.Data.Repository
{
    public class BaseRepository<T> where T : new()
    {
        protected readonly string _connectionString;
        private readonly ILogger<BaseRepository<T>> _logger;        
        public BaseRepository(IConfiguration configuration, ILogger<BaseRepository<T>> logger)
        {           
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        public async Task<List<T>> GetAllAsync(Func<SqlDataReader, T> map, string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<T> response = new List<T>();
                    await connection.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(map(reader));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // throw;
                        _logger.LogError(ex.Message);
                    }

                    return response;
                }
            }
        }
        public IEnumerable<T> GetAll(Func<SqlDataReader, T> map, string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<T> response = new List<T>();
                    connection.Open();

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Add(map(reader));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // throw;
                        _logger.LogError(ex.Message);
                    }

                    return response;
                }
            }
        }
        public async Task<List<T>> GetByConditionAsync(Func<SqlDataReader, T> map, string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    List<T> response = new List<T>();
                    await connection.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(map(reader));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // throw;
                        _logger.LogError(ex.Message);
                    }

                    return response;
                }
            }
        }
        public List<T> GetByCondition(Func<SqlDataReader, T> map, string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    List<T> response = new List<T>();
                    connection.Open();

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Add(map(reader));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // throw;
                        _logger.LogError(ex.Message);
                    }

                    return response;
                }
            }
        }
        public async Task<T> GetByIdAsync(Func<SqlDataReader, T> map, string query, int id)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    T response = new T();
                    await connection.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                response = map(reader);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                    return response;
                }
            }

        }
        public T GetById(Func<SqlDataReader, T> map, string query, int id)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    T response = new T();
                    connection.Open();

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response = map(reader);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                    return response;
                }
            }

        }
        public async Task<T> GetById(Func<SqlDataReader, T> map, string query, string id)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    T response = new T();
                    await connection.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                response = map(reader);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                    return response;
                }
            }

        }

        public async Task<object> GetAnonymousObject(Func<SqlDataReader, object> map, string query, Action<SqlCommand> sqlCmd)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.Add(new SqlParameter("@Id", id));
                    sqlCmd(command);
                    object response = new object();
                    await connection.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                response = map(reader);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                    return response;
                }
            }

        }
        public async Task<IEnumerable<object>> GetAnonymousObjectList(Func<SqlDataReader, object> map, string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    List<object> response = new List<object>();
                    await connection.OpenAsync();

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(map(reader));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // throw;
                        _logger.LogError(ex.Message);
                    }

                    return response;
                }
            }
        }
        public async Task InsertAsync(string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                }
            }
        }
        public void Insert(string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                }
            }
        }

        public async Task<object?> GetSingleValueAsync(string query, Action<SqlCommand> sqlCmd)
        {
            object? result = default;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);

                    try
                    {
                       await  connection.OpenAsync();
                        result = await command.ExecuteScalarAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                        _logger.LogError(ex.Message);
                    }

                }
            }
            return result;
        }

        public async Task UpdateAsync(string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                }
            }
        }
        public void Update(string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        //throw;
                    }

                }
            }
        }
        public async Task DeleteAsync(string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        // throw;
                    }

                }
            }
        }
        public void Delete(string query, Action<SqlCommand> sqlCmd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlCmd(command);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        // throw;
                    }

                }
            }
        }


    }
}
