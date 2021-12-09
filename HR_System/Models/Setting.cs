using System;
using System.Collections.Generic;

namespace HR_System.Models
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public float PlusPerhour { get; set; }
        public float MinusPerhour { get; set; }
        public string Dayoff1 { get; set; }
        public string Dayoff2 { get; set; }
    }
}
