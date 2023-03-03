using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;

namespace BackEnd_Interview.Services.UserService
{
    public interface IUserService
    {
        public User LogIn(LoginDto req);
        public string SignIn(LoginDto req);
        public void Logout();
    }
}
