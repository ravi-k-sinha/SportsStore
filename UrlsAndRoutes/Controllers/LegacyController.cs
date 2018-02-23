using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlsAndRoutes.Controllers
{
    public class LegacyController : Controller
    {
        public ViewResult GetLegacyUrl(string legacyUrl) =>
            View((object)legacyUrl); // cast to object so that the argument is treated as model instead of a name of a view
    }
}
