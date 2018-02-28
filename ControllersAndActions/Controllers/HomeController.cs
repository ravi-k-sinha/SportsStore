using ControllersAndActions.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        public ViewResult ReceiveForm(string name, string city) =>
            View("Result", $"{name} lives is {city}");
    }
}
