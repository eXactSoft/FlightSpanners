using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class DestinationType
    {
        public DestinationType()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public string DestinationTypeName { get; set; }
        public double DestinationTypeCostant { get; set; }

        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
