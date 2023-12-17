using backend.DTO;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Model;
[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var movieDtos = MapMoviesToDtos(movies);
        return Ok(movieDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovieById(int id)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        var movieDto = MapMovieToDto(movie);
        return Ok(movieDto);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> AddMovie([FromBody] MovieDto movieDto)
    {
        if (movieDto == null)
        {
            return BadRequest("MovieDto is null");
        }

        var newMovieId = await _movieRepository.AddMovieAsync(MapDtoToMovie(movieDto));

        // You can customize the response if needed
        return CreatedAtAction(nameof(GetMovieById), new { id = newMovieId }, movieDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieDto>> UpdateMovie(int id, [FromBody] MovieDto movieDto)
    {
        var existingMovie = await _movieRepository.GetMovieByIdAsync(id);

        if (existingMovie == null)
        {
            return NotFound();
        }

        await _movieRepository.UpdateMovieAsync(id, MapDtoToMovie(movieDto));

        // You can customize the response if needed
        return Ok(movieDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var existingMovie = await _movieRepository.GetMovieByIdAsync(id);

        if (existingMovie == null)
        {
            return NotFound();
        }

        await _movieRepository.DeleteMovieAsync(id);

        // You can customize the response if needed
        return NoContent();
    }

    private MovieDto MapMovieToDto(Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            title = movie.title,
            isCompleted = movie.isCompleted,
            genre = movie.genre
        };
    }

    private IEnumerable<MovieDto> MapMoviesToDtos(IEnumerable<Movie> movies)
    {
        var movieDtos = new List<MovieDto>();
        foreach (var movie in movies)
        {
            movieDtos.Add(MapMovieToDto(movie));
        }
        return movieDtos;
    }

    private Movie MapDtoToMovie(MovieDto movieDto)
    {
        return new Movie
        {
            Id = movieDto.Id,
            title = movieDto.title,
            isCompleted = movieDto.isCompleted,
            genre = movieDto.genre
        };
    }
}
