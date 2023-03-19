namespace WAD_MVC_12252.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }

        public int MovieGenreId { get; set; }
    }
}
