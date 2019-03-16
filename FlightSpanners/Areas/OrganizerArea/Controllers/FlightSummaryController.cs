using FlightSpanners.Areas.CommonArea.Controllers;
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
	[Authorize(Roles = "organizer")]
	public class FlightSummaryController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private FlightSummaryViewModel _flightsSummaryViewModel;

		public FlightSummaryController(IFlightSpannersData flightSpannersData, FlightSummaryViewModel flightsSummaryViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_flightsSummaryViewModel = flightsSummaryViewModel;
		}

		[HttpGet]
		public IActionResult Index()
		{
			string code = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			if (code == null)
			{
				return RedirectToAction(nameof(HomeController.Index), _flightSpannersData.ControllerName(nameof(HomeController)), new { area = nameof(CommonArea) });
			}
			else
			{
				//_flightsSummaryViewModel.InitializeSummaryViewModel(HttpContext, _flightSpannersData);
				return View(nameof(FlightSummaryController.Index), _flightsSummaryViewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]// Ensures that the form that send the request is the right one
		public IActionResult Index(FlightSummaryViewModel model)
		{
			//model.OrganizerGroups = ViewBag.OrganizerGroups;
			model.SummaryList = ViewBag.SummaryList;

			var x = model;
			var y = ViewBag.Title;
			return View(nameof(FlightSummaryController.Index), model);
		}
	}
}
