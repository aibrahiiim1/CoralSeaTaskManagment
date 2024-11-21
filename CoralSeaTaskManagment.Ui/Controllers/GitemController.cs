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
    public class GitemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GitemController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<GitemDto> List = new List<GitemDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.GitemApi);
                response.EnsureSuccessStatusCode();
                List.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<GitemDto>>());

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
            ViewBag.hotels = hotels;

            var responsedepartments = await client.GetAsync(ApiRequests.DepartmentApi);
            responsedepartments.EnsureSuccessStatusCode();
            var jsondepartmens = await responsedepartments.Content.ReadAsStringAsync();
            var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsondepartmens);
            var modeldepartments = new
            {
                items = departments
            };
            ViewBag.departments = departments;

            var responseglocation = await client.GetAsync(ApiRequests.GlocationApi);
            responseglocation.EnsureSuccessStatusCode();
            var jsonglocation = await responseglocation.Content.ReadAsStringAsync();
            var glocations = JsonConvert.DeserializeObject<List<GlocationDto>>(jsonglocation);
            var modelglocations = new
            {
                items = glocations
            };
            ViewBag.glocations = glocations;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GitemAddDto gitemAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.GitemCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(gitemAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var gitemadded = await httpResponseMessage.Content.ReadFromJsonAsync<GitemDto>();
            if (gitemadded != null)
            {
                return RedirectToAction("Index", "Gitem");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GitemDto>(ApiRequests.GitemApi + $"/{id.ToString()}");

            if (response is not null)
            {
                var responsehotels = await client.GetAsync(ApiRequests.HotelApi);
                responsehotels.EnsureSuccessStatusCode();
                var jsonhotels = await responsehotels.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<HotelDto>>(jsonhotels);
                var modelhotels = new
                {
                    items = hotels
                };
                ViewBag.hotels = hotels;

                var responsedepartments = await client.GetAsync(ApiRequests.DepartmentApi);
                responsedepartments.EnsureSuccessStatusCode();
                var jsondepartmens = await responsedepartments.Content.ReadAsStringAsync();
                var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsondepartmens);
                var modeldepartments = new
                {
                    items = departments
                };
                ViewBag.departments = departments;

                var responseglocation = await client.GetAsync(ApiRequests.GlocationApi);
                responseglocation.EnsureSuccessStatusCode();
                var jsonglocation = await responseglocation.Content.ReadAsStringAsync();
                var glocations = JsonConvert.DeserializeObject<List<GlocationDto>>(jsonglocation);
                var modelglocations = new
                {
                    items = glocations
                };
                ViewBag.glocations = glocations;
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GitemDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.GitemUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<GitemDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Gitem");
            }

            return View();
        }
        public async Task<IActionResult> Delete(GitemDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.GitemDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Gitem");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
        public async Task<IActionResult> Getloc(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responselocation = await client.GetAsync(ApiRequests.GitemByLoc + $"/{id.ToString()}");
            responselocation.EnsureSuccessStatusCode();
            var jsonlocation = await responselocation.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<List<LocationDto>>(jsonlocation);
            var modellocation = new
            {
                items = location
            };
            ViewBag.location = location;
            return Ok(location);
        }
    }
}
