using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class OriginType
    {
        public OriginType()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public string OriginTypeName { get; set; }
        public double OriginTypeConstant { get; set; }

        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
