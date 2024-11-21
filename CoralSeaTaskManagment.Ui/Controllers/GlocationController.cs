using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class GlocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GlocationController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<GlocationDto> List = new List<GlocationDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.GlocationApi);
                response.EnsureSuccessStatusCode();
                List.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<GlocationDto>>());

            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(List);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();
            var responsehotels = await client.GetAsync(ApiRequests.HotelApi);
            responsehotels.EnsureSuccessStatusCode();
            var jsonhotels = await responsehotels.Content.ReadAsStringAsync();
            var hotels = JsonConvert.DeserializeObject<List<HotelDto>>(jsonhotels);
            var modelhotels = new
            {
                items = hotels
            };
            var responsedepartments = await client.GetAsync(ApiRequests.DepartmentApi);
            responsedepartments.EnsureSuccessStatusCode();
            var jsondepartmens = await responsedepartments.Content.ReadAsStringAsync();
            var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsondepartmens);
            var modeldepartments = new
            {
                items = departments
            };
            ViewBag.hotels = hotels;
            ViewBag.departments = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GlocationAddDto glocationAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.GlocationCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(glocationAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var glocationadded = await httpResponseMessage.Content.ReadFromJsonAsync<GlocationDto>();
            if (glocationadded != null)
            {
                return RedirectToAction("Index", "Glocation");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GlocationDto>(ApiRequests.GlocationApi + $"/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GlocationDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.GlocationUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<GlocationDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Glocation");
            }

            return View();
        }
        public async Task<IActionResult> Delete(GlocationDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.GlocationDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Glocation");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
