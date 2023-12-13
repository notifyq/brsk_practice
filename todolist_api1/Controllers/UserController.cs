using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using todolist_api1.Model;

namespace todolist_api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetCurrentUserInfo")]
        public async Task<ActionResult<User>> GetCurrentUserInfo()
        {
            User currectUser = GetCurrectUser();

            if (currectUser == null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(currectUser);
        }
        [NonAction]
        public User GetCurrectUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    UserId = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                };
            }
            return null;
        }
    }
}

