using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HR_System.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AttDeps = new HashSet<AttDep>();
        }
        [Required(ErrorMessage ="*")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100,MinimumLength =3,ErrorMessage ="less than 3")]
        public string EmpName { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        [RegularExpression("^1[0-2]{1}[0-9]{8}", ErrorMessage ="Invalid Phone Number")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        public string Nationality { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        [Remote("daterange", "employees",ErrorMessage ="Emp must be greatear than twenty")]
        public DateTime Birthdate { get; set; }


        [Required]
        [RegularExpression("^[0-9]{14}$",ErrorMessage ="Invalid.. must be 14 degit number")]
        public string NationalId { get; set; } = null!;

        [Required(ErrorMessage = "*")]
        public DateTime Hiredate { get; set; }

        [Required(ErrorMessage = "*")]
        public int FixedSalary { get; set; }

        [Required(ErrorMessage = "*")]
        public TimeSpan AttTime { get; set; }

        [Required(ErrorMessage = "*")]
        public TimeSpan DepartureTime { get; set; }
        public int? DeptId { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual ICollection<AttDep> AttDeps { get; set; }
    }
}
