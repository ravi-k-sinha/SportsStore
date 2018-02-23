using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : IRouter
    {
        private string[] urls;
        private IRouter mvcRoute;

        public LegacyRoute(IServiceProvider services, params string[] targetUrls)
        {
            this.urls = targetUrls;
            mvcRoute = services.GetRequiredService<MvcRouteHandler>();
        }

        public async Task RouteAsync(RouteContext context)
        {
            string requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');

            if (urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                context.RouteData.Values["controller"] = "Legacy";
                context.RouteData.Values["action"] = "GetLegacyUrl";
                context.RouteData.Values["legacyUrl"] = requestedUrl;
                await mvcRoute.RouteAsync(context);
            }
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
