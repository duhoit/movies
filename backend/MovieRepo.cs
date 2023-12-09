﻿using backend.Model;
using backend.Repository;
using Backend;
using Backend;
// Repositories/MovieRepository.cs

namespace backend
{
    public class Crud
    {
        private readonly string connectionString;
        private Connection con;

        public Crud(string connectionString)
        {
            this.connectionString = connectionString;
            con = new(this.connectionString);
        }

        public void CreateFood(Movie movie)
        {
            string sql = "INSERT INTO movie (title, genre, iscompleted) VALUES (@title, @genre, @iscompleted);";
            con.sqlCommand(sql, new { movie.title, movie.genre, movie.isCompleted}, connectionString);
        }

        public void DeleteFood(int id)
        {
            string sql = "DELETE FROM movie WHERE ID = @Id;";
            con.sqlCommand(sql, new { Id = id }, connectionString);
        }

        public List<Movie> GetAllMovies()
        {
            string sql = "SELECT * FROM movie;";
            return con.loadDB<Movie, object>(sql, null, connectionString);
        }

        public void UpdateFood(Movie movie)
        {
            string sql = "UPDATE movie SET title = @title, genre = @genre, iscompleted = @iscompleted WHERE ID = @Id;";
            con.sqlCommand(sql, new { movie.title, movie.genre, movie.isCompleted, movie.Id }, connectionString);
        }

        public Movie GetById(int id)
        {
            string sql = "SELECT * FROM food WHERE ID = @Id;";
            var parameters = new { Id = id };

            var result = con.loadDB<Movie, object>(sql, parameters, this.connectionString);

            return result.Count > 0 ? result[0] : null;
        }
    }

}
