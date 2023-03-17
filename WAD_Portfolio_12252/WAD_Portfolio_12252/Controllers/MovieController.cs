using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WAD_Portfolio_12252.Models;
using WAD_Portfolio_12252.Repository;

namespace WAD_Portfolio_12252.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        // GET: api/<MovieController>
        [HttpGet]

        public IActionResult Get()
        {
            var movies = _movieRepository.GetAllMovies();
            return new OkObjectResult(movies);
        }

        // GET api/<MovieController>/5
        [HttpGet, Route("{id}", Name = "GetM")]
        public IActionResult Get(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            return new OkObjectResult(movie);
        }

        // POST api/<MovieController>
        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            using (var scope = new TransactionScope())
            {
                _movieRepository.InsertMovie(movie);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
            }
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Movie movie)
        {
            if (movie != null)
            {
                using (var scope = new TransactionScope())
                {
                    _movieRepository.UpdateMovie(movie);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _movieRepository.DeleteMovie(id);
            return new OkResult();
        }
    }
}
