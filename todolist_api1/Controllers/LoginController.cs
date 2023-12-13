using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using todolist_api1.Model;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using BCrypt;

namespace todolist_api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<User>> Get([FromBody] UserLogin userLogin)
        {

            var user = Authentecate(userLogin);
            /*User user = ApplicationContext.Context.Users.FirstOrDefault(x => x.UserPassword == password && x.UserLogin == login);*/
            if (user == null)
            {
                return new ObjectResult("Неверный логин или пароль") { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                var token = Generate(user);
                Console.WriteLine("Вход");
                return Ok(token);
            }
        }

        private string Generate(User user) // генерация токена для пользователя
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var token = new JwtSecurityToken(AuthOptions.ISSUER, AuthOptions.AUDIENCE,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authentecate(UserLogin userLogin) // получение пользователя
        {

            User user = ApplicationContext.Context.Users.FirstOrDefault(x => x.Username == userLogin.Login);
            if (user == null)
            {
                return null;
            }
            else
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.PasswordHash);
               
                User current_user = ApplicationContext.Context.Users.FirstOrDefault(x => isPasswordCorrect && x.Username == userLogin.Login);
                return current_user;
            }
        }
    }
}
