using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ThesisProject.Models;

namespace ThesisProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ThesisProjectDBContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Localization; Andrew
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            ////***********************************
            ////Localisation: #2 START
            ////***********************************
            //services.AddLocalization();
            ////Ev TODO lägga till AddMvc() här
            //services.Configure<RequestLocalizationOptions>(
            //    opts =>
            //    {
            //        var supportedCultures = new List<CultureInfo>
            //        {
            //            new CultureInfo("et"),
            //            new CultureInfo("en"),
            //            new CultureInfo("ru"),
            //        };
            //        opts.DefaultRequestCulture = new RequestCulture("et");
            //        opts.SupportedCultures = supportedCultures;
            //        opts.SupportedUICultures = supportedCultures;

            //        //TODO: Kolla om rätt namespace på routeData
            //        var provider = new RouteDataRequestCultureProvider();
            //        provider.RouteDataStringKey = "lang";
            //        provider.UIRouteDataStringKey = "lang";
            //        provider.Options = opts;
            //        opts.RequestCultureProviders = new[] { provider };
            //    }
            //);
            ////TODO: RouteOptions rätt namespace(?)
            //services.Configure<RouteOptions>(
            //    options =>
            //    {
            //        options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
            //    }
            //);

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ////***********************************
            ////Localisation: #2 END
            ////***********************************

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            //Localization: Andrew
            .AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix,
                opts => { opts.ResourcesPath = "Resources"; })
            .AddDataAnnotationsLocalization();

            //Localization: Andrew
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-GB"),
                        new CultureInfo("en-US"),
                        new CultureInfo("en"),
                        new CultureInfo("fr-FR"),
                        new CultureInfo("fr"),
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-GB");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Localization: Andrew
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            ////TODO: Ta bort om inte funkar
            ////****************************
            ////Localization #2
            ////****************************
            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            //app.UseRequestLocalization(options.Value);
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "LocalizedDefault",
            //        template: "{lang:lang}/{controller=Home}/{action=Index}/{id?}"
            //    );

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{*catchall}",
            //        defaults: new { controller = "Home", action = "RedirectToDefaultLanguage" });
            //});
        }
    }
}
