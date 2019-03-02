using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Holiday
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public bool IsHolidayRepeated { get; set; }
        public string HolidayDay { get; set; }
        public string HolidayMonth { get; set; }
        public string HolidayYear { get; set; }
        public string HolidayTypeName { get; set; }

        public HolidayType HolidayTypeNameNavigation { get; set; }
    }
}
