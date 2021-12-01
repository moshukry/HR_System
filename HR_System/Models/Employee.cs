using System;
using System.Collections.Generic;

namespace HR_System.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AttDeps = new HashSet<AttDep>();
        }

        public int EmpId { get; set; }
        public string EmpName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Phone { get; set; }
        public string Gender { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string NationalId { get; set; } = null!;
        public DateTime Hiredate { get; set; }
        public int FixedSalary { get; set; }
        public TimeSpan AttTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int? DeptId { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual ICollection<AttDep> AttDeps { get; set; }
    }
}
