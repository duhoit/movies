using Microsoft.AspNetCore.Mvc;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace backend.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>();

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            var movie = movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public ActionResult<Movie> Post([FromBody] Movie movie)
        {
            movie.Id = movies.Count + 1;
            movies.Add(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie updatedMovie)
        {
            var existingMovie = movies.Find(m => m.Id == id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.title = updatedMovie.title;
            existingMovie.genre = updatedMovie.genre;
            existingMovie.isCompleted = updatedMovie.isCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            movies.Remove(movie);
            return NoContent();
        }
    }

}