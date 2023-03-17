using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WAD_Portfolio_12252.Models;
using WAD_Portfolio_12252.Repository;

namespace WAD_Portfolio_12252.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public IActionResult Get()
        {
            var genre = _genreRepository.GetAllGenres();
            return new OkObjectResult(genre);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var genre = _genreRepository.GetGenreById(id);
            return new OkObjectResult(genre);
        }

        // POST api/<GenreController>
        [HttpPost]
        public IActionResult Post([FromBody] Genre genre)
        {
            using (var scope = new TransactionScope())
            {
                _genreRepository.InsertGenre(genre);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Genre genre)
        {
            if (genre != null)
            {
                using (var scope = new TransactionScope())
                {
                    _genreRepository.UpdateGenre(genre);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _genreRepository.DeleteGenre(id);
            return new OkResult();
        }
    }
}
