
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.OrganizerArea.ViewModels //.Home
{
  public class OrganizerDataViewModel
	{
		//Constructor injection
		public OrganizerDataViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			string code = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			//Return the organizer based on the code
			var organizer = flightSpannersData.GetOrganizerByCode(code);

			this.OrganizerCode = organizer.OrganizerCode;
			this.OrganizerEmail = organizer.OrganizerEmail;
			this.OrganizerFName = organizer.OrganizerFName;
			this.OrganizerLName = organizer.OrganizerLName;
			this.OrganizerM1Name = organizer.OrganizerM1Name;
			this.OrganizerM2Name = organizer.OrganizerM2Name;
			this.OrganizerMobile1 = organizer.OrganizerMobile1;
			this.OrganizerMobile2 = organizer.OrganizerMobile2;
			this.OrganizerOccupation = organizer.OrganizerOccupation;

			//Get the OrganizerGroupList item of the model using GetOrganizerGroupNames(code) method
			this.OrganizerGroupList = flightSpannersData.GetOrganizerGroupSelectListItems(code);
			//ViewBag.OrganizerGroupList = model.OrganizerGroupList;
		}

		public string OrganizerCode { get; set; }
		public string OrganizerFName { get; set; }
		public string OrganizerM1Name { get; set; }
		public string OrganizerM2Name { get; set; }
		public string OrganizerLName { get; set; }
		public string OrganizerEmail { get; set; }
		public string OrganizerMobile1 { get; set; }
		public string OrganizerMobile2 { get; set; }
		public string OrganizerOccupation { get; set; }
		public List<SelectListItem> OrganizerGroupList { get; set; }
		public int[] OrganizerGroupListValues { get; set; }
	}
}
