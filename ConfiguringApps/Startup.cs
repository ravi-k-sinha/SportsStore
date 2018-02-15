using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfiguringApps
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseBrowserLink();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc().AddMvcOptions(options => {
                options.RespectBrowserAcceptHeader = true;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if ((Configuration.GetSection("ShortCircuitMiddleware")?.GetValue<bool>("EnableBrowserShortCircuit")).Value)
            {
                app.UseMiddleware<BrowserTypeMiddleware>();
                app.UseMiddleware<ShortCircuitMiddleware>();
            }
            
            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
