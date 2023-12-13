using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todolist_mvc.Model
{
    public partial class UserTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        public string? Deadline { get; set; }
        public int? Priority { get; set; }
        public virtual Priority? PriorityNavigation { get; set; }
        /*        [BindNever]
                public virtual User User { get; set; } = null!;*/
 
    }
}
