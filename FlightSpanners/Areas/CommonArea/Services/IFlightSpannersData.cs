using FlightSpanners.Areas.CommonArea.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.CommonArea.Services
{
	public interface IFlightSpannersData
	{
		bool ValidateCode(string code);
		bool ValidatePasswordOrganizer(string code, string password);
		bool ValidatePasswordSpanner(string code, string password);
		Organizer GetOrganizerByCode(string code);
		List<SelectListItem> GetOrganizerGroupSelectListItems(string code);

		Spanner GetSpannerByCode(string code);
		Qualification GetQualificationOfSpanner(string code);
		List<SelectListItem> GetApprovalSelectListItems(string code);
		//List<SelectListItem> GetInActivePeriodListItems(string code);

		bool UpdatePassword(string code, string newPassword);
		int[] ConvertLengthToIntArray(int length);
		string ControllerName(string name);
	}
}
