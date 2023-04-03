using System.Collections.Generic;
using WAD_Portfolio_12252.Models;

namespace WAD_Portfolio_12252.Repository
{
    public interface IMovieRepository
    {
        void InsertMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int movieId);
        Movie GetMovieById(int movieId);
        IEnumerable<Movie> GetAllMovies();

    }
}
