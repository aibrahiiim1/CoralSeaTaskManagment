using CoralSeaTaskManagment.Security;
using CoralSeaTaskManagment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
