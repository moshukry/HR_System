using System;
using System.Collections.Generic;

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
        public string GroupName { get; set; } = null!;

        public virtual ICollection<Crud> Cruds { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
