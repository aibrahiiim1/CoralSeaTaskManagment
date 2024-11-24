using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("verify")]
        public ActionResult Verify()
        {
            return Ok("You're Authorized.");
        }
    }
}
