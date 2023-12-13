namespace todolist_api1.Model
{
    public class TaskAddModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? Deadline { get; set; }
        public int? Priority { get; set; }
    }
}
