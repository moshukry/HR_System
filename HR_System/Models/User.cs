using System;
using System.Collections.Generic;

namespace HR_System.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? GroupId { get; set; }

        public virtual Group? Group { get; set; }
    }
}
