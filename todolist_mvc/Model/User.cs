using System;
using System.Collections.Generic;

namespace todolist_mvc.Model
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

    }
}
