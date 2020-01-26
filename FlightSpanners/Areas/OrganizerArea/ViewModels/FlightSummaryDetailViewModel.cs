
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
	public class FlightSummaryDetailViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//public FlightSummaryViewModel() //: this(httpContext: _httpContext, flightSpannersData: _flightSpannersData)
		//{ }

		//Constructor injection
		public FlightSummaryDetailViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		public void SetFlightSummaryDetailViewModelProperties(string spannerCode)
		{
			GroupName = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;
			//string spannerCode = _httpContext.HttpContext.Request.Query["SpannerCode"];
			Spanner spanner = _flightSpannersData.GetSpannerByCode(spannerCode);
			SpannerCode = spannerCode;
			SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name;
			DeservedFlights = _flightSpannersData.GetSpannerDeservedFlights(spannerCode);
			AllFlights = _flightSpannersData.GetFlightsAll(spannerCode);
			SpannerLicenseNo = spanner.SpannerLicenseNo;
			ShortFlights = _flightSpannersData.GetFlightsShort(spannerCode);
			LongFlights = _flightSpannersData.GetFlightsLong(spannerCode);
			ExtraFlights = _flightSpannersData.GetFlightsExtra(spannerCode);
			MultipleFlights = _flightSpannersData.GetFlightsMultiple(spannerCode);
			ApologyFlights = _flightSpannersData.GetFlightsApology(spannerCode);
			BonusFlights = _flightSpannersData.GetFlightsBonus(spannerCode);
			HolidayFlights = _flightSpannersData.GetFlightsHoliday(spannerCode);
			CurrentMonthFlights = _flightSpannersData.GetFlightsCurrentMonth(spannerCode);
			IsCurrentlyInActive = _flightSpannersData.IsCurrentlyInActive(spannerCode);
			Balance = _flightSpannersData.GetSpannerBalance(spannerCode);
		}

		public string GroupName { get; set; }
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

}
