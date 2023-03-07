using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;

namespace BackEnd_Interview.Services.MovieServices
{
    public interface IMovieService
    {
        public List<Movie> GetMovies();
        public List<Movie> SetStatus(StatusMovieDto req);
        public List<Movie> CreateMovie(CreateMovieDto req);

        public List<MoviePages> GetMoviePage(int page, int pageSize);
        public Task<List<MoviesIsLiked>> FindUserIsLikeMovie(int userID);
    }
}
