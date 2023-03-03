using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;

namespace BackEnd_Interview.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly MyDbContext _db;
        private readonly IConfiguration _configuration;
        public MovieService(MyDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public List<Movie> GetMovies()
        {
            var ListMovie = _db.Movies.ToList();

            return ListMovie;
        }

        public List<Movie> CreateMovie(CreateMovieDto req)
        {
            var newMovie = new Movie();
            newMovie.Title = req.Title;
            newMovie.Url_Image = req.Url_Image;

            _db.Movies.Add(newMovie);
            _db.SaveChanges();

            var listMovie = GetMovies();
            return listMovie;
        }

        public List<Movie> SetStatus(StatusMovieDto req)
        {
            var favoriteMovie = new UserMoviePreferences();

            favoriteMovie.UserId = req.UserId;
            favoriteMovie.MovieId = req.MovieId;
            favoriteMovie.IsLiked = req.IsLiked;

            _db.FavoriteMovies.Add(favoriteMovie);

            return _db.Movies.ToList();
        }

        public List<Movie> GetMoviePage(int currentPage)
        {
            var pageSize = 5;
            var paged = _db.Movies.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return paged;
        }
    }
}
