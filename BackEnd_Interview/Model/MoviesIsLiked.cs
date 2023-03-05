namespace BackEnd_Interview.Model
{
    public class MoviesIsLiked
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string? Url_Image { get; set; }
        public bool? IsLiked { get; set; }

        //public List<UserMoviePreferences> Movies { get; set; }
    }
}
