using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class GdashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
