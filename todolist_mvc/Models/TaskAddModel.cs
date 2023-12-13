using static todolist_mvc.Model.UserTask;

namespace todolist_mvc.Models
{
    public class TaskAddModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? Deadline { get; set; }
        public int? Priority { get; set; }
    }
}
