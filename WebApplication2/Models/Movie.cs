namespace WebApplication2.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string[] Stars { get; set; }
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; }
    }

}
