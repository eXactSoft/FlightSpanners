using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightSpanners.Areas.CommonArea.Models;
using FlightSpanners.Areas.CommonArea.Services;
using FlightSpanners.Areas.CommonArea.ViewModels;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using FlightSpanners.Areas.OrganizerArea.Controllers;
//using FlightSpanners.Areas.SpannerArea.Controllers;

namespace FlightSpanners.Areas.CommonArea.Controllers
{
	[Area("CommonArea")] //Let the framework know which area this controller belongs to
	public class HomeController : Controller
	{
		private IFlightSpannersData _flightSpannersData;

		public HomeController(IFlightSpannersData flightSpannersData)
		{
			_flightSpannersData = flightSpannersData;
		}

		public IActionResult Index(string returnUrl)//To pass ReturnUrl value from view to controller.
		{
			//To pass ReturnUrl value from controller to view, ViewData's life only lasts during current http request.
			ViewData["ReturnUrl"] = returnUrl;

			if (User.Identity.IsAuthenticated)
			{
				if( (returnUrl != null) && Url.IsLocalUrl(returnUrl)) // To protecting against open redirect attacks
				{
					return Redirect(returnUrl);
				}
				//if the Role is spanner
				else if (this.User.FindFirst(ClaimTypes.Role).Value.ToLower().Equals("spanner"))
				{
					return RedirectToAction( nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new { area = nameof(SpannerArea)});
					//return Content("Spanner Personal Data");
				}
				//if the Role is organizer
				else if (this.User.FindFirst(ClaimTypes.Role).Value.ToLower().Equals("organizer"))
				{
					return RedirectToAction( nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new {area = nameof(OrganizerArea)} );
				}
			}

			// To set the error that gives that the user authintication has been end required to be sign in
			// , the error only displayed if the returnUrl is not null and not from SignOut get request
			if ((returnUrl != null) && !returnUrl.ToLower().EndsWith("signout") && Url.IsLocalUrl(returnUrl)) // To protecting against open redirect attacks
			{
				//Transfer the error message of user authentication end requires sign in
				//To pass error value from controller to view, ViewBag's life only lasts during current http request.
				ViewBag.error = "Your authentication ends, It is required to Sign in again!";
			}

			//Returns to Index view of home controller.
			return View( nameof(HomeController.Index), new SigninViewModel() );
		}

		[HttpPost]
		[ValidateAntiForgeryToken]// Ensures that the form that send the request is the right one
		public async Task<IActionResult> Signin(SigninViewModel model, string returnUrl)
		{
			//To pass ReturnUrl value from controller to view, ViewData's life only lasts during current http request.
			ViewData["ReturnUrl"] = returnUrl;

			//The user role initializes to null.
			string role=null;

			if (ModelState.IsValid && !User.Identity.IsAuthenticated)
			{
				bool isCodeOk = false, isPasswordOrganizerOk = false, isPasswordSpannerOk = false;

				//Check the existance of the entered code
				isCodeOk = _flightSpannersData.ValidateCode(model.Code);

				if (isCodeOk)//Check the password only if code exists.
				{
					//Validate the password with the code as organizer and/or spanner
					isPasswordOrganizerOk = _flightSpannersData.ValidatePasswordOrganizer(model.Code, model.Password);
					isPasswordSpannerOk = _flightSpannersData.ValidatePasswordSpanner(model.Code, model.Password);
				}

				role = (model.IsSigninAsSpanner && isPasswordSpannerOk) ?
											"spanner" : 
											(isPasswordOrganizerOk)? "organizer" : "spanner";

				string actor = (isPasswordOrganizerOk && isPasswordSpannerOk) ? "double" : "single";

				if (isPasswordOrganizerOk || isPasswordSpannerOk)
				{
					//Call the local AuthenticationCookieBasedAsync() method async to authinticate unauthenticated user.
					await AuthenticationCookieBasedAsync(model.Code, role, actor);
				}
				else 
				{
					//Transfer the error message of wrong password or code to the view
					//To pass error value from controller to view, ViewBag's life only lasts during current http request.
					ViewBag.error = isCodeOk ? ((isPasswordOrganizerOk || isPasswordSpannerOk) ? null : "The Password is wrong, Please try again!") : "This Code is not registered";
					return View( nameof(HomeController.Index), model);
				}
			}
			else if (!ModelState.IsValid)
			{
				return View( nameof(HomeController.Index), model );
			}

			role = role.Equals(null) ? this.User.FindFirst(ClaimTypes.Role).Value : role;

			//The redirect sequence first to returnUrl, then to index of organizer PersonalData controller
			// ,  then to index of spanner PersonalData controller
			//&& !returnUrl.ToLower().EndsWith("signout") 
			if ( (returnUrl != null) && Url.IsLocalUrl(returnUrl)) // To protecting against open redirect attacks
			{
				return LocalRedirect(returnUrl);
			}
			else if (role.Equals("organizer"))
			{
				return RedirectToAction(nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new {area = nameof(OrganizerArea)} );
				//Equivelant to RedirectToAction("Index", "/OrganizerArea/PersonalData");
				// , code = model.OrganizerCode
			}
			else if (role.Equals("spanner"))
			{
				return RedirectToAction( nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new {area = nameof(SpannerArea)} );
				//return RedirectToAction( "Index", "PersonalData", new { area = "SpannerArea" } );
				//return Content("Spanner Personal Data");
			}

			// If all the above redirects to the views not work then redirects to the index view of home controller.
			return View( nameof(HomeController.Index), model );
		}

		[HttpGet]
		//This httpGet action method to ensure if the form is passed by get request by reload button of the browser
		public IActionResult Signin(string returnUrl)
		{
			//To pass ReturnUrl value from controller to view, ViewData's life only lasts during current http request.
			ViewData["ReturnUrl"] = returnUrl;

			//Redirects to the index action method of the home controller.
			return RedirectToAction( nameof(HomeController.Index) );
		}

		[HttpGet]
		public async Task<IActionResult> SigninAsSpanner(string returnUrl)
		{
			//To pass ReturnUrl value from controller to view, ViewData's life only lasts during current http request.
			ViewData["ReturnUrl"] = returnUrl;

			if (User.Identity.IsAuthenticated)
			{
				if (User.FindFirst(ClaimTypes.Role).Value.ToLower().Equals("spanner"))
				{
					return RedirectToAction( nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new {area = nameof(SpannerArea)} );
					//return Content("Spanner Personal Data");
				}
				else //If the user role is organizer then signout
				{
					await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				}
			}

			//Returns to the index view of the home controller with the IsSigninAsSpanner set to true of the SigninViewModel
			return View( nameof(HomeController.Index), new SigninViewModel() {IsSigninAsSpanner = true} );
		}

		[Authorize]
		public async Task<IActionResult> Signout()
		{
			// To sign out the current user and delete their cookie
			await HttpContext.SignOutAsync( CookieAuthenticationDefaults.AuthenticationScheme );

			//var authCookies = HttpContext.Request.Cookies["FlightSpannersCookie"];

			//redirect to site root
			return Redirect("/");
		}

		public IActionResult ForgotUnregistered()
		{
			//returns to ForgotUnregistered view of the home controller
			return View( nameof(HomeController.ForgotUnregistered) );
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public IActionResult ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string currentCode = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				//HttpContext.User.Identity.Name; //Will be got from session 

				if (!model.PasswordNew.Equals(model.PasswordConfirm))
				{
					//To pass error value from controller to view, ViewBag's life only lasts during current http request.
					ViewBag.error = "The New Password and Confirm Password do not match";
					return View( nameof(HomeController.ChangePassword), model );
				}
				else if (model.PasswordOld.Equals(model.PasswordNew))
				{
					//To pass error value from controller to view, ViewBag's life only lasts during current http request.
					ViewBag.error = "The New Password Equals to the Old Password";
					return View( nameof(HomeController.ChangePassword), model );
				}

				bool isCodeOk = false, isPasswordOrganizerOk = false, isPasswordSpannerOk = false;

				//Check the existance of the entered code
				isCodeOk = _flightSpannersData.ValidateCode(currentCode);

				if (isCodeOk)//Check the password only if code exists.
				{
					isPasswordOrganizerOk = _flightSpannersData.ValidatePasswordOrganizer(currentCode, model.PasswordOld);
					isPasswordSpannerOk = _flightSpannersData.ValidatePasswordSpanner(currentCode, model.PasswordOld);
				}

				if ( isCodeOk && (isPasswordOrganizerOk || isPasswordSpannerOk) )
				{
					//Update the passowrd of the user at Spanner & Organizer tables
					_flightSpannersData.UpdatePassword(currentCode, model.PasswordNew);

					if ( this.User.FindFirst(ClaimTypes.Role).Value.ToLower().Equals("organizer") )
					{
						return RedirectToAction( nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new {area = nameof(OrganizerArea)} );
					}
					else
					{
						return RedirectToAction( nameof(PersonalDataController.Index), _flightSpannersData.ControllerName(nameof(PersonalDataController)), new {area = nameof(SpannerArea)} );
						//return Content("Spanner Personal Data");
					}
				}
				else
				{
					//Transfer the error message to view
					ViewBag.error = (isPasswordOrganizerOk || isPasswordSpannerOk) ? null : "The Old Password is wrong, Please try again!";
					return View( nameof(HomeController.ChangePassword), model );
				}
			}
			else
			{
				return View( nameof(HomeController.ChangePassword), model );
			}
		}

		[HttpGet]
		[Authorize]
		//This httpGet action method to ensure if the form is passed by get request by reload button of the browser
		public IActionResult ChangePassword()
		{
			return View( nameof(HomeController.ChangePassword), new ChangePasswordViewModel() );
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		
		//Use the user code, role, & actor to authenticate the user based on cookie
		private async Task AuthenticationCookieBasedAsync(string code, string role, string actor)
		{
			/*
			 * A claim is a statement about a subject by an issuer. Claims represent 
			 * attributes of the subject that are useful in the context of authentication 
			 * and authorization operations. Subjects and issuers are both entities that 
			 * are part of an identity scenario. Some typical examples of a subject are: a user, 
			 * an application or service, a device, or a computer.
			 * 
			 * NameIdentifier: is the code of the user.
			 * Role: is the user sign in as organizer or spanner.
			 * Actor: is the user has a single account (organizer or spanner) or double account of both.
			 * 
			 */
			var claims = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, code),
						new Claim(ClaimTypes.Role, role), //"organizer" or "spanner"
						new Claim(ClaimTypes.Actor, actor)
					};

			/*
				* The claims contained in a ClaimsIdentity describe the entity that the 
				* corresponding identity represents, and can be used to make authorization 
				* and authentication decisions.
				*/
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			// AuthenticationProperties class is a dictionary used to store state 
			// values about the authentication session.
			var authProperties = new AuthenticationProperties
			{
				// Refreshing the authentication session allowed or not
				AllowRefresh = true,

				// The time in minutes at which the authentication ticket expires. A 
				// value set here overrides the ExpireTimeSpan option of 
				// CookieAuthenticationOptions set with AddCookie.
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(3),

				// Whether the authentication session is persisted across 
				// multiple requests. Required when setting the 
				// ExpireTimeSpan option of CookieAuthenticationOptions 
				// set with AddCookie. Also required when setting 
				// ExpiresUtc.
				IsPersistent = true,

				// The time at which the authentication ticket was issued.
				//IssuedUtc = <DateTimeOffset>,

				// The full path or absolute URI to be used as an http 
				// redirect response value.
				RedirectUri = "/" /*OrganizerArea/PersonalData/Index*/
			};

			// Creates an encrypted cookie and adds it to the current response.
			// If you don't specify an AuthenticationScheme, the default scheme is used.
			 await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}

	}
}
