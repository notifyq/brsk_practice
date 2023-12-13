using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace todolist_api1.Model
{
    public partial class Priority
    {
        public Priority()
        {
            UserTasks = new HashSet<UserTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
