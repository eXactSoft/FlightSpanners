using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class DistanceType
    {
        public string DistanceTypeName { get; set; }
        public double DistanceTypeCostant { get; set; }
        public double? UpperSectorTime { get; set; }
        public string UpperOperator { get; set; }
        public double? LowerSectorTime { get; set; }
        public string LowerOperator { get; set; }
    }
}
