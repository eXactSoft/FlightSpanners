using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
//using FlightSpanners.Areas.OrganizerArea.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlightSpanners.Areas.OrganizerArea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FlightSpanners.Areas.CommonArea.Controllers;

namespace FlightSpanners.Areas.OrganizerArea.Controllers
{
	[Area("OrganizerArea")] //Let the framework know which area this controller belongs to
	[Authorize(Roles ="organizer")]
	public class PersonalDataController : Controller
	{
		private IFlightSpannersData _flightSpannersData;
		private OrganizerDataViewModel _organizerDataViewModel;

		public PersonalDataController(IFlightSpannersData flightSpannersData, OrganizerDataViewModel organizerDataViewModel)
		{
			_flightSpannersData = flightSpannersData;
			_organizerDataViewModel = organizerDataViewModel;
		}

		public IActionResult Index()
		{
			string code = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			if (code == null)
			{
				return RedirectToAction( nameof(HomeController.Index), _flightSpannersData.ControllerName(nameof(HomeController)), new {area = nameof(CommonArea)} );
			}
			else
			{ 
				return View( nameof(PersonalDataController.Index), _organizerDataViewModel );
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
						return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
