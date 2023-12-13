using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using todolist_mvc.ApiRequests;
using todolist_mvc.Models;

namespace todolist_mvc.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        Api api;
        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            api = new Api();
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Login(/*[Bind("Login,Password")]*/ UserLogin loginModel)
        {
            if (await Authorization(loginModel))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private async Task<bool> Authorization(UserLogin loginModel)
        {
            string token = await api.UserLoginAsync(loginModel.Login, loginModel.Password);
            if (token.Length != 0)
            {
                Authenticate(token);
                await api.SetTokenForClientAsync(token);

                var _token = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var claims = _token.Claims;
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
                return true;
            }
            return false;
           
            
        }

        private async Task Authenticate(string token)
        {
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
