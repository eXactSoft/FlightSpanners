using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlightSpanners.Areas.SpannerArea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
//using FlightSpanners.Areas.CommonArea.Controllers;
using FlightSpanners.Areas.SpannerArea.Controllers;

namespace FlightSpanners.Areas.SpannerArea.Controllers
{
	[Area("SpannerArea")] //Let the framework know which area this controller belongs to
	[Authorize(Roles ="Spanner")]
	public class PersonalDataController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private SpannerDataViewModel _spannerDataViewModel;

		public PersonalDataController(IFlightSpannersData flightSpannersData, SpannerDataViewModel spannerDataViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_spannerDataViewModel = spannerDataViewModel;
		}

		public IActionResult Index()
		{
			string code = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			if (code == null)
			{
				//return RedirectToAction( nameof(HomeController.Index), _flightSpannersData.ControllerName(nameof(HomeController)), new {area = nameof(CommonArea)} );
				return Content("Hi Man!!");
			}
			else
			{ 
				return View( nameof(PersonalDataController.Index), _spannerDataViewModel);
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
						return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
