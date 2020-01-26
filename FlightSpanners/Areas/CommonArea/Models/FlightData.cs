using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class FlightData
    {
        public FlightData()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public int FlightDataId { get; set; }
        public string AirportOrigin { get; set; }
        public string AirportDestination { get; set; }
        public TimeSpan DefaultSectorTime { get; set; }
        public double FlightDataConstant { get; set; }
        public bool IsFlightLocal { get; set; }
        public bool IsFlightCargo { get; set; }
        public int FlightBonusCard { get; set; }

        public Airport AirportDestinationNavigation { get; set; }
        public Airport AirportOriginNavigation { get; set; }
        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
