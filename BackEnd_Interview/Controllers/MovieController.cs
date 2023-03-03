﻿using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;
using BackEnd_Interview.Services.MovieServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Interview.Controllers
{
    [Route("api/Movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet(Name = "getAllMovie"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Movie>>> GetMovie()
        {
            var listUser = _movieService.GetMovies();

            return Ok(listUser);
        }

        [HttpGet("getMoviePage")]
        public async Task<ActionResult<List<Movie>>> GetMoviePage(int currentPage)
        {
            if (string.IsNullOrEmpty(currentPage.ToString()) || currentPage < 0)
            {
                return BadRequest("CurrentPage greater than > 0");
            }
            var paged = _movieService.GetMoviePage(currentPage);

            return Ok(paged);
        }

        [HttpPost("CreateMovie"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Movie>>> CreateMovie(CreateMovieDto req)
        {
            if (req.Title == null || req.Url_Image == null)
            {
                return BadRequest("Titel or url Image incorrect");
            }
            return _movieService.CreateMovie(req);
        }

        [HttpPost("Liked"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Movie>>> PostStatus(StatusMovieDto req)
        {
            if (req.UserId == null || req.MovieId == null || req.IsLiked == null)
            {
                return BadRequest();
            }

            return Ok(_movieService.SetStatus(req));
        }
    }
}