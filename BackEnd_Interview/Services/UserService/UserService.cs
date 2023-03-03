using BackEnd_Interview.Dto;
using BackEnd_Interview.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace BackEnd_Interview.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _db;
        private readonly IConfiguration _configuration;
        public UserService(MyDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public User LogIn(LoginDto req)
        {
            var account = _db.Users.SingleOrDefault(x => x.UserName == req.UserName);

            // check account not found and verify password
            if (account == null || !BC.Verify(req.Password, account.Password))
            {
                return null;
            }

            string token = CreateToken(account);

            account.Token = token;
            _db.SaveChanges();

            //var newToken = new RefreshToken();
            //newToken.Token = token;

            return account;
        }
        public void Logout()
        {
            throw new NotImplementedException();
        }

        public string SignIn(LoginDto req)
        {
            var account = new User();

            // check user exiting 
            var listAccount = _db.Users.SingleOrDefault(u => u.UserName == req.UserName);
            if (listAccount != null)
            {
                return "UserName existing";
            }
            if (req.Password.Length <= 6)
            {
                return "Password have to greater than or equal to 6 character!";
            }

            // hash password
            account.Password = BC.HashPassword(req.Password);
            account.UserName = req.UserName;
            account.Roles = 2000;



            // save account 
            _db.Users.Add(account);
            _db.SaveChanges();

            return "Sign In Sucessfully!";
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value
                ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



    }
}
