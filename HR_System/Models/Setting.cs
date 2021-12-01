using System;
using System.Collections.Generic;

namespace HR_System.Models
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public double? PlusPerhour { get; set; }
        public double? MinusPerhour { get; set; }
        public string? Dayoff1 { get; set; }
        public string? Dayoff2 { get; set; }
    }
}
