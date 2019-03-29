
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.OrganizerArea.ViewModels 
{
	public class FlightSummaryViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//public FlightSummaryViewModel() //: this(httpContext: _httpContext, flightSpannersData: _flightSpannersData)
		//{ }

		//Constructor injection
		public FlightSummaryViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;

			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			//Return the SummaryList for the current organizer according to the current group name
			SummaryList = GetSummaryList(currentGroup); //"AirbusProduction");//
		}

		/*public void InitializeSummaryViewModel(HttpContext httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;

			string organizerCode = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			//Return the organizer based on the code
			var organizer = flightSpannersData.GetOrganizerByCode(organizerCode);

			//Get the OrganizerGroupList item of the model using GetOrganizerGroupNames(code) method
			this.OrganizerGroupsList = flightSpannersData.GetOrganizerGroupSelectListItems(organizerCode);

			//Get the OrganizerGroupListValues array from the length of OrganizerGroupList
			//this.OrganizerGroupListValues = flightSpannersData.ConvertLengthToIntArray(this.OrganizerGroupList.Count);

			OrganizerGroupId = 0;

			//Return the SummaryList for the current organizer according to the first group name
			//To be re-implemented to be according to selected group name not first group name
			SummaryList = GetSummaryList(OrganizerGroupsList[OrganizerGroupId].Text); //"AirbusProduction");//
																																						//SummaryList = GetSummaryList(OrganizerGroups[0].Text);
		}
		*/

		public List<Summary> SummaryList { get; set; }

		public class Summary
		{
			public string SpannerCode { get; set; }
			public string SpannerName { get; set; }
			public double DeservedFlights { get; set; }
			public double AllFlights { get; set; }
			public string SpannerLicenseNo { get; set; }
			public double Balance { get; set; }
			public double ShortFlights { get; set; }
			public double LongFlights { get; set; }
			public double ExtraFlights { get; set; }
			public double MultipleFlights { get; set; }
			public double ApologyFlights { get; set; }
			public double BonusFlights { get; set; }
			public double HolidayFlights { get; set; }
			public double CurrentMonthFlights { get; set; }
			public bool IsCurrentlyInActive { get; set; }
		}

		private List<Summary> GetSummaryList(string groupName)
		{
			SummaryList = new List<Summary>();
			var spannerQuery = _flightSpannersData.GetSpannersFromGroupName(groupName);
			foreach (var spanner in spannerQuery)
			{
				SummaryList.Add(new Summary {
					SpannerCode = spanner.SpannerCode,
					SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name,
					DeservedFlights = _flightSpannersData.GetSpannerDeservedFlights(spanner.SpannerCode),
					AllFlights = _flightSpannersData.GetFlightsAll(spanner.SpannerCode),
					SpannerLicenseNo = spanner.SpannerLicenseNo,
					ShortFlights = _flightSpannersData.GetFlightsShort(spanner.SpannerCode),
					LongFlights = _flightSpannersData.GetFlightsLong(spanner.SpannerCode),
					ExtraFlights = _flightSpannersData.GetFlightsExtra(spanner.SpannerCode),
					MultipleFlights = _flightSpannersData.GetFlightsMultiple(spanner.SpannerCode),
					ApologyFlights = _flightSpannersData.GetFlightsApology(spanner.SpannerCode),
					BonusFlights = _flightSpannersData.GetFlightsBonus(spanner.SpannerCode),
					HolidayFlights = _flightSpannersData.GetFlightsHoliday(spanner.SpannerCode),
					CurrentMonthFlights = _flightSpannersData.GetFlightsCurrentMonth(spanner.SpannerCode),
					IsCurrentlyInActive = _flightSpannersData.IsCurrentlyInActive(spanner.SpannerCode),
					Balance = _flightSpannersData.GetSpannerBalance(spanner.SpannerCode)
				});
			}
			return SummaryList;
		}
	}

}
