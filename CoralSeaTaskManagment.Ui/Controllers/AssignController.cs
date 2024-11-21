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
    public class AssignController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AssignController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<AssignDto> Dtolist = new List<AssignDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.AssignApi);
                response.EnsureSuccessStatusCode();
                Dtolist.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<AssignDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            return View(Dtolist);
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
        public async Task<IActionResult> Create(AssignAddDto addDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.AssignCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(addDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var locationadd = await httpResponseMessage.Content.ReadFromJsonAsync<AssignDto>();
            if (locationadd != null)
            {
                return RedirectToAction("Index", "Assign");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<AssignDto>(ApiRequests.AssignApi + $"/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AssignDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.AssignUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<AssignDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Assign");
            }

            return View();
        }
        public async Task<IActionResult> Delete(AssignDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.AssignDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Assign");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
