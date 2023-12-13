using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace todolist_api1.Model
{
    public partial class UserTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? Deadline { get; set; }
        public int? Priority { get; set; }
        public virtual Priority? PriorityNavigation { get; set; }
/*        [JsonIgnore]
        public virtual User User { get; set; }*/
    }
}
