
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
	public class InActivePeriodDetailViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public InActivePeriodDetailViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize InActivePeriodViewModel Properties
		public void SetInActivePeriodDetailViewModelProperties(string spannerCode)
		{
			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			GroupName = currentGroup;


			var inActiveDaysList = GetInActivePeriodListBySpannerCode(spannerCode);
		}

		public List<DaysOfInActive> InActivePeriodList { get; set; }

		public string GroupName { get; set; }

		public class DaysOfInActive
		{
			public string SpannerCode { get; set; }
			public string SpannerLicenseNo { get; set; }
			public string SpannerName { get; set; }
			public int InActivePeriodId { get; set; }
			public DateTime InActiveFromDate { get; set; }
			public DateTime InActiveToDate { get; set; }
		}

		public int InActiveDaysTotal(string spannerCode)
		{
			int ret = 0;
			foreach (var inActive in InActivePeriodList)
			{
				if (spannerCode.Equals(inActive.SpannerCode))
				{
					ret += (int)inActive.InActiveToDate.Subtract(inActive.InActiveFromDate).TotalDays;
				}
			}

			return ret;
		}

		public bool IsInActiveNow(string spannerCode)
		{
			foreach (var inActive in InActivePeriodList)
			{
				if (spannerCode.Equals(inActive.SpannerCode) &&
						DateTime.Compare(inActive.InActiveToDate, DateTime.Now) > 0
					//DateTime.Compare(t1, t2) => returns > zero then t1 is later than t2.
					//Befor comparing DateTime objects, ensure that the objects represent times in the same time zone.
					)
				{
					return true;
				}
			}
			return false;
		}


		private List<DaysOfInActive> GetInActivePeriodListBySpannerCode(string spannerCode)
		{
			InActivePeriodList = new List<DaysOfInActive>();
			var inActiveDaysQuery = _flightSpannersData.GetInActivePeriodBySpannerCode(spannerCode);
			Spanner spanner = _flightSpannersData.GetSpannerByCode(spannerCode);

			foreach (var inActiveDays in inActiveDaysQuery)
			{
				InActivePeriodList.Add(new DaysOfInActive
				{
					SpannerCode = spanner.SpannerCode,
					SpannerLicenseNo = spanner.SpannerLicenseNo,
					SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name,
					InActivePeriodId = inActiveDays.InActivePeriodId,
					InActiveFromDate = inActiveDays.InActiveFromDate,
					InActiveToDate = inActiveDays.InActiveToDate
				});
			}
			return InActivePeriodList;
		}

	}

}
