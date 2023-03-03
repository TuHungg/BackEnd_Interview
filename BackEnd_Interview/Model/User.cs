namespace BackEnd_Interview.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // 2000: Admin
        // 2001: User
        public int Roles { get; set; }
        public string? Token { get; set; } = string.Empty;
        public ICollection<UserMoviePreferences> FavoriteMovies { get; set; }
    }
}
