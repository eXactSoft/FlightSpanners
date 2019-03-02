using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class EligibilityType
    {
        public EligibilityType()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public string EligibilityTypeName { get; set; }
        public double EligibilityTypeCostant { get; set; }

        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
