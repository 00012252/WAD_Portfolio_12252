using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WAD_Portfolio_12252.Models;

namespace WAD_Portfolio_12252.MovieDbContext
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
