namespace BackEnd_Interview.Model
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string? Url_Image { get; set; }
        public ICollection<UserMoviePreferences> FavoriteMovies { get; set; }
    }
}
