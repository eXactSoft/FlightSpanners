using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class OrganizerGroup
    {
        public OrganizerGroup()
        {
            FlightRecord = new HashSet<FlightRecord>();
        }

        public int OrganizerGroupId { get; set; }
        public string OrganizerCode { get; set; }
        public string GroupName { get; set; }

        public GroupOfSpanners GroupNameNavigation { get; set; }
        public Organizer OrganizerCodeNavigation { get; set; }
        public ICollection<FlightRecord> FlightRecord { get; set; }
    }
}
