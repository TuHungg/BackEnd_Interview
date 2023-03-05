using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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


        public async Task<List<MoviesIsLiked>> FindUserIsLikeMovie(int userID)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                // STORE PROCEDURE: FindUserIsLikeMovie
                var command = new SqlCommand("FindUserIsLikeMovie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userID);
                var reader = command.ExecuteReader();

                var moviesIsLike = new Dictionary<int, MoviesIsLiked>();

                while (reader.Read())
                {
                    var movieId = reader.GetInt32(reader.GetOrdinal("MovieId"));

                    if (!moviesIsLike.ContainsKey(movieId))
                    {
                        var movie = new MoviesIsLiked();
                        movie.MovieId = movieId;
                        movie.Title = reader.GetString(reader.GetOrdinal("Title"));
                        movie.Url_Image = reader.GetString(reader.GetOrdinal("Url_Image"));
                        movie.IsLiked = reader.GetBoolean(reader.GetOrdinal("IsLiked"));
                        //movie.Movies = new List<UserMoviePreferences>();

                        moviesIsLike.Add(movieId, movie);
                    }
                    //var ListMovie = await _db.Movies.SingleOrDefaultAsync(mv => mv.MovieId != movieId);
                    //var ListMovieIsLike = new MoviesIsLiked();
                    //ListMovieIsLike.Title = ListMovie.Title;
                    //ListMovieIsLike.Url_Image = ListMovie.Url_Image;

                    //moviesIsLike.Add(movieId, ListMovieIsLike);
                }
                return moviesIsLike.Values.ToList();
            }
        }
    }
}
