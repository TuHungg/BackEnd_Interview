namespace BackEnd_Interview.Model
{
    public class UserMoviePreferences
    {
        public int UserMoviePreferencesId { get; set; }
        // Like
        public bool IsLiked { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
