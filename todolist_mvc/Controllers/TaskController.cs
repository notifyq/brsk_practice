using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todolist_mvc.ApiRequests;
using todolist_mvc.Model;

namespace todolist_mvc.Controllers
{
    public class TaskController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        Api api;
        public TaskController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
           string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
           api = new Api(token);
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.PriorityList = await api.GetPriorityList();
            List<UserTask> tasks = await api.GetTasks();
            Console.WriteLine(tasks.Count.ToString());
            return View(tasks);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.PriorityList = await api.GetPriorityList();
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("UserId,Name,Description,Deadline,Priority")]UserTask task)
        {
            await api.AddTask(task);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.PriorityList = await api.GetPriorityList();
            UserTask userTask = await api.GetTask(id);

            if (userTask != null)
            {
                return View(userTask);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description,Deadline,Priority")]UserTask userTask)
        {
            await api.EditTask(userTask);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.PriorityList = await api.GetPriorityList();
            UserTask userTask = await api.GetTask(id);
            if (userTask != null)
            {      
                return View(userTask);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await api.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
