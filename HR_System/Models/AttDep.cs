﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models
{
    public partial class AttDep
    {
     
        public int AttId { get; set; }

        public int EmpId { get; set; }

        [Required(ErrorMessage = "* Date is Required")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "*")]
        public TimeSpan Attendance { get; set; }

        [Required(ErrorMessage = "*")]
        public TimeSpan Departure { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int workedHours { get; set; }
        public virtual Employee? Emp { get; set; } = null!;
    }
}
