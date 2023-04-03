using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WAD_Portfolio_12252.Models;
using WAD_Portfolio_12252.MovieDbContext;

namespace WAD_Portfolio_12252.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context) { _context = context; }
        public void DeleteMovie(int movieId)
        {
            var movie = _context.Movies.Find(movieId);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.Include(g => g.Genre).ToList();
        }

        public Movie GetMovieById(int movieId)
        {
            var movie = _context.Movies.Find(movieId);
            _context.Entry(movie).Reference(s => s.Genre).Load();
            return movie;

        }

        public void InsertMovie(Movie movie)
        {
            movie.Genre = _context.Genres.Find(movie.Genre.Id);
            _context.Add(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            movie.Genre = _context.Genres.Find(movie.Genre.Id);
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
