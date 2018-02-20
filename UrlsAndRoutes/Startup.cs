using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

namespace UrlsAndRoutes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                /*
                routes.MapRoute(
                    name: "ShopSchema2",
                    template: "Shop/OldAction",
                    defaults: new { controller = "Home", action = "Index" }
                );

                routes.MapRoute(
                    name: "ShopSchema",
                    template: "Shop/{action}",
                    defaults: new { controller = "Home" }
                );

                routes.MapRoute("", "X{controller}/{action}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                );

                routes.MapRoute(
                    name: "",
                    template: "Public/{controller=Home}/{action=Index}");
                */

                routes.MapRoute(
                    name: "MyRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index"},
                    constraints: new
                    {
                        id = new CompositeRouteConstraint(
                            new IRouteConstraint[] {
                                new AlphaRouteConstraint(),
                                new MinLengthRouteConstraint(6)
                            }
                        )
                    }
                );
            });
        }
    }
}
