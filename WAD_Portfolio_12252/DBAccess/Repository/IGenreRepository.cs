using System.Collections.Generic;
using WAD_Portfolio_12252.Models;

namespace WAD_Portfolio_12252.Repository
{
    public interface IGenreRepository
    {

        void InsertGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int genreId);
        Genre GetGenreById(int genreId);
        IEnumerable<Genre> GetAllGenres();
    }
}
