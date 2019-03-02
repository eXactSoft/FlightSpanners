using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Admin
    {
        public int AdminCode { get; set; }
        public string AdminFName { get; set; }
        public string AdminM1Name { get; set; }
        public string AdminM2Name { get; set; }
        public string AdminLName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminMobile1 { get; set; }
        public string AdminMobile2 { get; set; }
        public string AdminOccupation { get; set; }
    }
}
