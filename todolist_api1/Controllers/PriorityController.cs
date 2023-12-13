using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todolist_api1.Model;

namespace todolist_api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Priority>>> Get()
        {
            List<Priority> priorityList = new List<Priority>();
            priorityList = ApplicationContext.Context.Priorities.ToList();
            if (priorityList.Count == 0)
            {
                return NotFound();
            }
            return priorityList;
        }
    }
}
