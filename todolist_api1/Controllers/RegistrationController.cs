using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todolist_api1.Model;

namespace todolist_api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<User>> Registration(UserRegistration registration_model)
        {
            if (registration_model == null || registration_model.UserName == String.Empty || registration_model.UserPassword == string.Empty)
            {
                return BadRequest("Пустые поля");
            }
            else if (ApplicationContext.Context.Users.FirstOrDefault(x => x.Username == registration_model.UserName) != null)
            {
                return Conflict("Логин уже существует");
            }
            else
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registration_model.UserPassword); // Хеширование пароля с помощью bcrypt

                User new_user = new User()
                {
                    Username = registration_model.UserName,
                    PasswordHash = hashedPassword,
                };
                ApplicationContext.Context.Add(new_user);
                ApplicationContext.Context.SaveChanges();
                return Ok();
            }
        }
    }
}
