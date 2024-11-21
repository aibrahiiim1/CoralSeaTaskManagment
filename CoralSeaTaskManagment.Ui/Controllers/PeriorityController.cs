using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class PeriorityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PeriorityController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<PeriorityDto> List=new List<PeriorityDto>();

            // Get All Perioritys
            try
            {
                var client = _httpClientFactory.CreateClient();
                var hotels = await client.GetAsync(ApiRequests.PeriorityApi);
                hotels.EnsureSuccessStatusCode();
                List.AddRange( await hotels.Content.ReadFromJsonAsync<IEnumerable<PeriorityDto>>());
                //ViewBag.Perioritys = hotelsBody;
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            
            return View(List);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PeriorityAddDto model)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.PeriorityCreate),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<PeriorityDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Periority");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<PeriorityDto>(ApiRequests.PeriorityApi+$"/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PeriorityDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.PeriorityUpdate+$"/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<PeriorityDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Periority");
            }

            return View();
        }                
        public async Task<IActionResult> Delete(PeriorityDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.PeriorityDelete+$"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Periority");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
