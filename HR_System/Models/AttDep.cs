using System;
using System.Collections.Generic;

namespace HR_System.Models
{
    public partial class AttDep
    {
        public int AttId { get; set; }
        public int EmpId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? Attendance { get; set; } = null!;
        public TimeSpan? Departure { get; set; } = null!;

        public virtual Employee Emp { get; set; } = null!;
    }
}
