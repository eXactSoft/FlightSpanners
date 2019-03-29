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

		Spanner GetSpannerByCode(string spannerCode);
		Qualification GetQualificationOfSpanner(string codspannerCodee);
		List<SelectListItem> GetApprovalSelectListItems(string spannerCode);
		//List<SelectListItem> GetInActivePeriodListItems(string code);
		IEnumerable<Spanner> GetSpannersFromGroupName(string groupName);
		double GetFlightsAll(string spannerCode);
		double GetFlightsShort(string spannerCode);
		double GetFlightsLong(string spannerCode);
		double GetFlightsExtra(string spannerCode);
		double GetFlightsMultiple(string spannerCode);
		double GetFlightsApology(string spannerCode);
		double GetFlightsBonus(string spannerCode);
		double GetFlightsHoliday(string spannerCode);
		double GetFlightsCurrentMonth(string spannerCode);
		int InActiveDaysCount(string spannerCode);
		bool IsCurrentlyInActive(string spannerCode);
		double GetSpannerBalance(string spannerCode);
		double GetSpannerDeservedFlights(string spannerCode);
		string GetSpannerGroup(string code);

		bool ValidateOrganizerCode(string code);
		bool ValidateSpannerCode(string code);

		DistanceType GetDistanceType(string distanceTypeName);

		bool UpdatePassword(string code, string newPassword);
		//int[] ConvertLengthToIntArray(int length);
		string ControllerName(string name);
	}
}
