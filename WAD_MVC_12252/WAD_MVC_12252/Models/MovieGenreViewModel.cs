using Microsoft.AspNetCore.Mvc.Rendering;

namespace WAD_MVC_12252.Models
{
    public class MovieGenreViewModel
    {
        public Movie Movie { get; set; }
        public SelectList Genres { get; set; }

    }
}
