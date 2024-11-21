using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class GroomsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GroomsController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<GroomDto> locationList = new List<GroomDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.GroomsApi);
                response.EnsureSuccessStatusCode();
                locationList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<GroomDto>>());

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
        public async Task<IActionResult> Create(GroomAddDto groomAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.GroomsCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(groomAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var groomsadded = await httpResponseMessage.Content.ReadFromJsonAsync<GroomDto>();
            if (groomsadded != null)
            {
                return RedirectToAction("Index", "Grooms");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //var client = _httpClientFactory.CreateClient();
            //var response = await client.GetFromJsonAsync<GroomDto>("https://localhost:7097/api/Grooms" + $"/{id.ToString()}");

            //if (response is not null)
            //{
            //    return View(response);
            //}
            //return View(null);
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GroomDto>($"https://localhost:7097/api/Grooms/{id}");

            if (response is not null)
            {
                return View(response);
            }

            // Handle the case where the groom with the provided ID is not found
            return NotFound(); // Or return a custom error view
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GroomDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.GroomsUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<GroomDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Grooms");
            }

            return View();
        }
        public async Task<IActionResult> Delete(GroomDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.GroomsDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Grooms");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
