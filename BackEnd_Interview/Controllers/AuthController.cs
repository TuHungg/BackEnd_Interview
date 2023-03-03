//using Microsoft.AspNetCore.Http;
using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Interview.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<string>> SignIn(LoginDto req)
        {
            if (string.IsNullOrEmpty(req.UserName) && string.IsNullOrEmpty(req.Password))
            {
                return BadRequest("UserName or Password incorrect");
            }

            return Ok(_userService.SignIn(req));
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LogIn(LoginDto req)
        {
            if (string.IsNullOrEmpty(req.UserName) && string.IsNullOrEmpty(req.Password))
            {
                return BadRequest("UserName or Password incorrect");
            }

            var checkAccount = _userService.LogIn(req);

            // check account not found and verify password
            if (checkAccount == null)
            {
                return BadRequest("UserName or Password incorrect");
            }

            return Ok(checkAccount);
        }

    }
}
//https://github.com/patrickgod/JwtWebApiTutorial/blob/master/JwtWebApiTutorial/Controllers/AuthController.cs