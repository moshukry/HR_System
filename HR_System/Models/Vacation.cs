using System;
using System.Collections.Generic;

namespace HR_System.Models
{
    public partial class Vacation
    {
        public int VacId { get; set; }
        public DateTime? VacationDate { get; set; }
        public string? VacationName { get; set; }
    }
}
