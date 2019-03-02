using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class AircraftType
    {
        public AircraftType()
        {
            Approval = new HashSet<Approval>();
            FlightRecord = new HashSet<FlightRecord>();
        }

        public int AircraftTypeId { get; set; }
        public string AircrfatModel { get; set; }
        public string EngineModel { get; set; }
        public double AircraftTypeCostant { get; set; }

        public ICollection<Approval> Approval { get; set; }
        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
