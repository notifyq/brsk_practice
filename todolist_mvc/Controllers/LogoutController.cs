using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using todolist_mvc.ApiRequests;

namespace todolist_mvc.Controllers
{
    public class LogoutController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        Api api;
        public LogoutController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            api = new Api(token);
        }
        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext.Items.Remove("token");
            _httpContextAccessor.HttpContext.SignOutAsync();
/*            HttpContext.Items.Remove("token");
            HttpContext.SignOutAsync();*/
            return RedirectToAction("Index","Home");
        }
    }
}
