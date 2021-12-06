using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_System.Models
{
    public partial class Group
    {
        public Group()
        {
            Cruds = new HashSet<Crud>();
            Users = new HashSet<User>();
        }

        public int GroupId { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; } = null!;

        public virtual ICollection<Crud> Cruds { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
