using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlightSpanners.Areas.CommonArea.Data;
//using FlightSpanners.Areas.CommonArea.Services;
using FlightSpanners.Areas.CommonArea.Services;
//using FlightSpanners.Areas.OrganizerArea.Services;
//using FlightSpanners.Areas.SpannerArea.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using FlightSpanners.Areas.OrganizerArea.ViewModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FlightSpanners.Areas.SpannerArea.ViewModels;

namespace FlightSpanners
{
  public class Startup
  {
    private IConfiguration _configuration { get; }

    public Startup(IConfiguration configuration)
    {
			_configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });
      services.AddDbContext<FlightSpannersContext>( options => options.UseSqlServer(_configuration.GetConnectionString("FlightSpanners")));

			//Singleton: This implies only a single instance will be created and shared by all consumers.
			//services.AddSingleton<IAppSetting, AppSetting>();

			//Scoped: This implies that one instance per scope (i.e., one instance per request to the application) will be created.
			services.AddScoped<IFlightSpannersData, SqlServerFlightSpannersData>();

			//Singleton: This implies only a single instance will be created and shared by all consumers.
			//Registere IHttpContextAccessor in services
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//Transient: services are created each time they're requested. 
			//					 This lifetime works best for lightweight, stateless services.

			//Register FlightRecordDetailViewModel object in services
			services.AddTransient<FlightRecordDetailViewModel>();

			//Register FlightRecordViewModel object in services
			services.AddTransient<FlightRecordViewModel>();

			//Register FlightSummaryDetailViewModel object in services
			services.AddTransient<FlightSummaryDetailViewModel>();

			//Register FlightSummaryViewModel object in services
			services.AddTransient<FlightSummaryViewModel>();

			//Register OrganizerDataViewModel object in services
			services.AddTransient<OrganizerDataViewModel>();

			//Register SpannerDataViewModel object in services
			services.AddTransient<SpannerDataViewModel>();

			//Register InActivePeriodViewModel object in services
			services.AddTransient<InActivePeriodViewModel>();

			//Register InActivePeriodViewModel object in services
			services.AddTransient<InActivePeriodDetailViewModel>();

			//Register SpannersDataViewModel object in services
			services.AddTransient<SpannersDataViewModel>();

			//Register SpannersDataDetailViewModel object in services
			services.AddTransient<SpannersDataDetailViewModel>();

			//Register ApprovalsDataViewModel object in services
			services.AddTransient<ApprovalsDataViewModel>();

			//Register ApprovalsDataDetailViewModel object in services
			services.AddTransient<ApprovalsDataDetailViewModel>();

			//Register GroupsDataViewModel object in services
			services.AddTransient<GroupsDataViewModel>();

			//Use cookie authentication without ASP.NET Core Identity
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie( options =>
				{
					// The CookieAuthenticationOptions class is used to configure the authentication provider options.
					options.LoginPath = "/";//"/OrganizerArea/Home/Signin";
					//options.LogoutPath = "/Home/Signout";
					options.Cookie.Name = "FlightSpannersCookie";
				}
				);

			//Add MVC Service
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      //

      /*services.Configure<RazorViewEngineOptions>(o =>
      {
				// Gets the locations where RazorViewEngine will search for views.
				// {2} is area, {1} is controller,{0} is the action    
				o.ViewLocationFormats.Clear();
				o.ViewLocationFormats.Add("/Views/SpannerFiles/{0}" + RazorViewEngine.ViewExtension);
				o.ViewLocationFormats.Add("/Views/OrganizerFiles/{0}" + RazorViewEngine.ViewExtension);
				o.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
				o.ViewLocationFormats.Add("/Controllers/Shared/Views/{0}" + RazorViewEngine.ViewExtension);

				// Gets the locations where RazorViewEngine will search for views within an area.
				o.AreaViewLocationFormats.Clear();
				o.AreaViewLocationFormats.Add("/Areas/{2}/Controllers/{1}/Views/{0}" + RazorViewEngine.ViewExtension);
				o.AreaViewLocationFormats.Add("/Areas/{2}/Controllers/Shared/Views/{0}" + RazorViewEngine.ViewExtension);
				o.AreaViewLocationFormats.Add("/Areas/Shared/Views/{0}" + RazorViewEngine.ViewExtension);
      });*/
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app,
                            IHostingEnvironment env
                            )
    {
      if (env.IsDevelopment())
      {
				app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

			//app.UseCookiePolicy();

			// Invoke the Authentication Middleware that sets the HttpContext.User property
			// Must go before UseMvc
			app.UseAuthentication();

			app.UseMvc(routes =>
      {
				// Areas support
				routes.MapRoute(
					name: "areaRoute",
					template: "{area}/{controller}/{action}/{code?}", ///{isSigninAsSpanner:bool?}
					defaults: new { area="CommonArea", controller="Home", action="Index" });
				//Note use {area=OrganizerArea} instead of { area: exists}
				routes.MapRoute(
          name: "defaultRoute",
          //template: "SpannerFiles/{controller=Home}/{action=Index}/{code?}");
          template: "{controller=Home}/{action=Index}/{code?}");
      });

    }
  }
}
