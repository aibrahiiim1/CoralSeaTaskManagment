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
    public class LocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LocationController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<LocationDto> locationList = new List<LocationDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7097/api/Location");
                response.EnsureSuccessStatusCode();
                locationList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<LocationDto>>());
                //ViewBag.Hotels = hotelsBody;
                // Islam C#
                //var client = _httpClientFactory.CreateClient();
                //var response = await client.GetAsync(ApiRequests.DepartmentApi);
                //response.EnsureSuccessStatusCode();
                //var json = await response.Content.ReadAsStringAsync();
                //var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(json);
                //departmentList.AddRange(departments);
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(locationList);
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
        public async Task<IActionResult> Create(LocationAddDto locationAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.LocationCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(locationAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var locationadded = await httpResponseMessage.Content.ReadFromJsonAsync<LocationDto>();
            if (locationadded != null)
            {
                return RedirectToAction("Index", "Location");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<LocationDto>(ApiRequests.LocationApi + $"/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LocationDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.LocationUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<LocationDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Location");
            }

            return View();
        }
        public async Task<IActionResult> Delete(LocationDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.LocationDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Location");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
