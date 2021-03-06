﻿using FlightSpanners.Areas.CommonArea.Controllers;
using FlightSpanners.Areas.CommonArea.Services;
using FlightSpanners.Areas.OrganizerArea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.OrganizerArea.Controllers
{
	[Area("OrganizerArea")] //Let the framework know which area this controller belongs to
	[Authorize(Roles = "Organizer")]
	public class FlightSummaryController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private FlightSummaryViewModel _flightsSummaryViewModel;
		private FlightSummaryDetailViewModel _flightsSummaryDetailViewModel;

		//Constructor injection
		public FlightSummaryController(IFlightSpannersData flightSpannersData, FlightSummaryViewModel flightsSummaryViewModel, FlightSummaryDetailViewModel flightsSummaryDetailViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_flightsSummaryViewModel = flightsSummaryViewModel;
			_flightsSummaryDetailViewModel = flightsSummaryDetailViewModel;
			//HttpContext.Request.Query["page"].ToString(); This will raise a null ref exception as the HttpContext object is not constructed yet
		}

		public IActionResult Index(int? page)
		{
			string code = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			if (code == null)
			{
				return RedirectToAction(nameof(HomeController.Index), _flightSpannersData.ControllerName(nameof(HomeController)), new { area = nameof(CommonArea) });
			}
			else
			{
				//page ?? 1 {null coalescing operator} ==> return the valur of page if it has a value
				//,or return 1 if page is null
				_flightsSummaryViewModel.SetFlightSummaryViewModelProperties(page ?? 1);
				return View(nameof(FlightSummaryController.Index), _flightsSummaryViewModel);
			}
		}

		public IActionResult Detail(string spannerCode)
		{
			if ( String.IsNullOrEmpty(spannerCode) )
			{
				return RedirectToAction(nameof(HomeController.Index));
			}
			else
			{
				_flightsSummaryDetailViewModel.SetFlightSummaryDetailViewModelProperties(spannerCode);
				return View(nameof(FlightSummaryController.Detail), _flightsSummaryDetailViewModel);
			}
		}

	}
}
