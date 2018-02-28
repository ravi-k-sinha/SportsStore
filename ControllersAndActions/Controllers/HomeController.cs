using ControllersAndActions.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        [HttpPost]
        public RedirectToActionResult ReceiveForm(string name, string city) =>
            RedirectToAction(nameof(Data));

        public ViewResult DefaultView() =>
            View();

        public ViewResult Data() => View("Result");
    }
}
