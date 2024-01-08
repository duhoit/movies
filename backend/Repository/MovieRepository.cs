using backend.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace backend.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration _configuration;

        public MovieRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<IDbConnection> GetOpenConnectionAsync()
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
            await connection.OpenAsync();
            return connection;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                return await connection.QueryAsync<Movie>("SELECT * FROM movie");
            }
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                return await connection.QueryFirstOrDefaultAsync<Movie>("SELECT * FROM movie WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<int> AddMovieAsync(Movie movie)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                return await connection.ExecuteScalarAsync<int>("INSERT INTO movie (title, genre, \"isCompleted\") VALUES (@Title, @Genre, @IsCompleted) RETURNING id", movie);
            }
        }
   
        public async Task<bool> UpdateMovieAsync(int id, Movie movie)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                var affectedRows = await connection.ExecuteAsync("UPDATE movie SET title = @Title, genre = @Genre, \"isCompleted\" = @IsCompleted WHERE id = @Id", new { Id = id, Title = movie.title, Genre = movie.genre, IsCompleted = movie.isCompleted });
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            using (var connection = await GetOpenConnectionAsync())
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM movie WHERE id = @Id", new { Id = id });
                return affectedRows > 0;
            }
        }
    }
}
