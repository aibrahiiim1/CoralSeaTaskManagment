using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
