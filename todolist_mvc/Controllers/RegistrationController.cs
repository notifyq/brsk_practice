using Microsoft.AspNetCore.Mvc;
using todolist_mvc.ApiRequests;
using todolist_mvc.Models;

namespace todolist_mvc.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        Api api;
        public RegistrationController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            api = new Api();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Registration(/*[Bind("Login,Password")]*/ UserRegistration loginModel)
        {
            await api.UserRegistrationAsync(loginModel.UserName, loginModel.UserPassword);
            return RedirectToAction("Index", "Home");
        }
    }
}
