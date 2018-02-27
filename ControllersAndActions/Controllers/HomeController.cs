using ControllersAndActions.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        public IActionResult ReceiveForm(string name, string city) =>
            new CustomHttpResult
            {
                Content = $"{name} lives in {city}"
            };
    }
}
