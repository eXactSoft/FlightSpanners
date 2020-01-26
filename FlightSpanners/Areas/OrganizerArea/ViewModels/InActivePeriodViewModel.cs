
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
	public class InActivePeriodViewModel
	{
		private IFlightSpannersData _flightSpannersData;
		private IHttpContextAccessor _httpContext;

		//Constructor injection
		public InActivePeriodViewModel(IHttpContextAccessor httpContext, IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
			_httpContext = httpContext;
		}

		//Initialize InActivePeriodViewModel Properties
		public void SetInActivePeriodViewModelProperties(int pageNo)
		{
			var currentGroup = _httpContext.HttpContext.User.FindFirst(ClaimTypes.GroupSid).Value;

			//The number of inactive days PageSize in one page
			PageSize = 10;//****This will be taken from a configuration file

			PageNo = pageNo;
			var inActiveDaysList = GetInActivePeriodList(currentGroup);
			TotalRecords = inActiveDaysList.Count();

			//Return the InActiveDaysList for the current organizer group according to the current group name ordered by spanner code
			//and for pagination to operate skip the ((PageNo - 1) * PageSize) rows starting from 1st row
			//and take from the remaining rows the first PageSize rows only
			InActivePeriodList = inActiveDaysList.OrderByDescending(x => x.SpannerCode).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();  
		}

		public int TotalRecords { get; set; }
		
		public int PageNo { get; set; }

		public int PageSize { get; set; }

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
				if ( spannerCode.Equals(inActive.SpannerCode) )
				{
					ret += (int) inActive.InActiveToDate.Subtract(inActive.InActiveFromDate).TotalDays;
				}
			}

			return ret;
		}

		public bool IsInActiveNow (string spannerCode)
		{
			foreach(var inActive in InActivePeriodList)
			{
				if( spannerCode.Equals(inActive.SpannerCode) &&
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

		private List<DaysOfInActive> GetInActivePeriodList(string groupName)
		{
			InActivePeriodList = new List<DaysOfInActive>();
			var inActiveDaysQuery = _flightSpannersData.GetInActivePeriodByGroupName(groupName);
			Spanner spanner;

			foreach (var inActiveDays in inActiveDaysQuery)
			{
				spanner = _flightSpannersData.GetSpannerByCode(inActiveDays.SpannerCode);

				InActivePeriodList.Add(new DaysOfInActive {
					SpannerCode = inActiveDays.SpannerCode,
					SpannerName = spanner.SpannerFName + " " + spanner.SpannerM1Name,
					InActivePeriodId = inActiveDays.InActivePeriodId,
					InActiveFromDate = inActiveDays.InActiveFromDate,
					InActiveToDate = inActiveDays.InActiveToDate
				});
			}
			return InActivePeriodList;
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
