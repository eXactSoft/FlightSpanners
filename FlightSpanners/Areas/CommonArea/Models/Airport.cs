using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightDataAirportDestinationNavigation = new HashSet<FlightData>();
            FlightDataAirportOriginNavigation = new HashSet<FlightData>();
        }

        public string Iatacode { get; set; }
        public string Icaocode { get; set; }
        public string AirportCountry { get; set; }
        public string AirportCity { get; set; }
        public string AirportName { get; set; }

        public ICollection<FlightData> FlightDataAirportDestinationNavigation { get; set; }
        public ICollection<FlightData> FlightDataAirportOriginNavigation { get; set; }
    }
}
