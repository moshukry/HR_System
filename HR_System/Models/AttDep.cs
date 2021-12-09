using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_System.Models
{
    public partial class AttDep
    {
        public int AttId { get; set; }

        public int EmpId { get; set; }

        [Required(ErrorMessage = "* Date is Required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "* Attendance Time is Required")]
        public TimeSpan? Attendance { get; set; } = null!;

        [Required(ErrorMessage = "* Departure Time is Required")]
        public TimeSpan? Departure { get; set; } = null!;

        public virtual Employee? Emp { get; set; }
    }
}
