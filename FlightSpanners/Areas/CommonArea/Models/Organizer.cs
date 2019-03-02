using System;
using System.Collections.Generic;

namespace FlightSpanners.Areas.CommonArea.Models
{
    public partial class Organizer
    {
        public Organizer()
        {
            OrganizerGroup = new HashSet<OrganizerGroup>();
        }

        public string OrganizerCode { get; set; }
        public string OrganizerFName { get; set; }
        public string OrganizerM1Name { get; set; }
        public string OrganizerM2Name { get; set; }
        public string OrganizerLName { get; set; }
        public string OrganizerEmail { get; set; }
        public string OrganizerPassword { get; set; }
        public string OrganizerMobile1 { get; set; }
        public string OrganizerMobile2 { get; set; }
        public string OrganizerOccupation { get; set; }

        public ICollection<OrganizerGroup> OrganizerGroup { get; set; }
    }
}
