using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WAD_Portfolio_12252.Models;
using WAD_Portfolio_12252.MovieDbContext;

namespace WAD_Portfolio_12252.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieContext _context;
        public GenreRepository(MovieContext context)
        {
            _context = context;
        }

        public void DeleteGenre(int genreId)
        {
            var genre = _context.Genres.Find(genreId);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public Genre GetGenreById(int genreId)
        {
            var genre = _context.Genres.Find(genreId);
            return genre;
        }

        public void InsertGenre(Genre genre)
        {
            _context.Add(genre);
            _context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
        }
    }
}
