using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View();
        }

        public ViewResult Result() => View((object)"Hello World");

        public RedirectToActionResult Redirect() => 
            RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));

        public JsonResult JsonData() => Json(new[] { "Alice", "Bob", "Joe"});

        public ContentResult ContentData() =>
            Content("[\"Alice\",\"Bob\",\"Joe\"]", "application/json");

        public ObjectResult ObjectData() =>
            Ok(new string[] { "Alice", "Bob", "Joe"});

        public VirtualFileResult CssFile()
            => File("/lib/bootstrap/dist/css/bootstrap.css", "text/css");

        public StatusCodeResult Unknown()
            => NotFound();
    }
}
