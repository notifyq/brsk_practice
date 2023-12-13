using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using todolist_api1.Model;

namespace todolist_api1.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserTask>>> GetUserTasks()
        {
            User user = GetCurrectUser();
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                List<UserTask> tasks = ApplicationContext.Context.UserTasks
                    .Include(x => x.PriorityNavigation)
                    .Where(x => x.UserId == user.UserId).ToList();
                return Ok(tasks);
            }

        }
        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<UserTask>> GetUserTask(int? id)
        {
            User user = GetCurrectUser();
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                UserTask userTask = ApplicationContext.Context.UserTasks.FirstOrDefault(x => x.Id == id && x.UserId == user.UserId);
                if (userTask != null)
                {
                    return Ok(userTask);
                }
                else
                {
                    return BadRequest();
                }
               
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<UserTask>>> AddUserTask(UserTask task) 
        {
            User user = GetCurrectUser();
            UserTask userTask = new UserTask() { 
                Name = task.Name,
                Deadline = task.Deadline,
                Description = task.Description,
                Priority = task.Priority,
                UserId = user.UserId,
            };
            if (userTask != null)
            {
                ApplicationContext.Context.UserTasks.Add(userTask);
                ApplicationContext.Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
           
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<List<UserTask>>> UpdateUserTask(UserTask task)
        {
            User user = GetCurrectUser();
            if (user == null)
            {
                return BadRequest();
            }
            UserTask userTask = ApplicationContext.Context.UserTasks.FirstOrDefault(x => x.Id == task.Id && x.UserId == user.UserId);
            if (userTask == null)
            {
                return NotFound();
            }
            else
            {
                userTask.Deadline = task.Deadline;
                userTask.Description = task.Description;
                userTask.Name = task.Name;
                userTask.Priority = task.Priority;
                ApplicationContext.Context.SaveChanges();
                return Ok();
            }

            
        }
        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<UserTask>> DeleteUserTask(int? id)
        {
            User user = GetCurrectUser();
            if (user == null)
            {
                return BadRequest();
            }
            UserTask task = ApplicationContext.Context.UserTasks.FirstOrDefault(x => x.Id == id && x.UserId == user.UserId);
            if (task == null)
            {
                return BadRequest();
            }
            else
            {
                ApplicationContext.Context.UserTasks.Remove(task);
                ApplicationContext.Context.SaveChanges();
                return Ok();
            }
            
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
