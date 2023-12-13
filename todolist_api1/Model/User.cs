using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace todolist_api1.Model
{
    public partial class User
    {
        public User()
        {
            UserTasks = new HashSet<UserTask>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
