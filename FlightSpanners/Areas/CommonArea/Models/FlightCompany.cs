using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class FlightCompany
    {
        public FlightCompany()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public string FlightCompanyName { get; set; }
        public double FlightCompanyCostant { get; set; }

        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
