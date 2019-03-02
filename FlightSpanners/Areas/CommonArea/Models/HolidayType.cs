using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class HolidayType
    {
        public HolidayType()
        {
            Holiday = new HashSet<Holiday>();
        }

        public string HolidayTypeName { get; set; }
        public double HolidayTypeConstant { get; set; }

        public ICollection<Holiday> Holiday { get; set; }
    }
}
