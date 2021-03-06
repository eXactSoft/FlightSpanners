﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.CommonArea.ViewModels //.Home
{
    public class SignInViewModel
    {
        public string Code { get; set; }
				// To force the signin role to be spanner for the user who has double account (spanner & organizer)
				// If true the spanner is select to sign in as spanner (select only 7 not means it is already a spanner)
				public string SignInUserRole { get; set; } //Default value: Organizer
				public string ReturnUrl { get; set; }

				[Required(ErrorMessage = "Please Enter the Password")]
        public string Password { get; set; }

				public List<SelectListItem> OrganizerGroups { get; set; }

				[Required(ErrorMessage = "Please Select The Group")]
				public string OrganizerGroupId { get; set; } 
	}
}
