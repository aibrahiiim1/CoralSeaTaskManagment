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
    public class DepartmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DepartmentController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async void HotelList()
        {
            List<HotelDto> hotelsList = new List<HotelDto>();

            // Get All Hotels
 
                var client = _httpClientFactory.CreateClient();
                var hotels = await client.GetAsync("https://localhost:7097/api/hotel");
                hotels.EnsureSuccessStatusCode();
                hotelsList.AddRange(await hotels.Content.ReadFromJsonAsync<IEnumerable<HotelDto>>());
                ViewBag.hotels = hotelsList;
            
        }
        public async Task<IActionResult> Index()
        {
            List<DepartmentDto> departmentList = new List<DepartmentDto>();
            List<HotelDto> hotelList = new List<HotelDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var departments = await client.GetAsync("https://localhost:7097/api/department");
                departments.EnsureSuccessStatusCode();
                departmentList.AddRange(await departments.Content.ReadFromJsonAsync<IEnumerable<DepartmentDto>>());
                // Islam C#
                //var client = _httpClientFactory.CreateClient();
                //var response = await client.GetAsync(ApiRequests.DepartmentApi);
                //response.EnsureSuccessStatusCode();
                //var json = await response.Content.ReadAsStringAsync();
                //var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(json);
                //departmentList.AddRange(departments);

                var responsehotel = await client.GetAsync(ApiRequests.HotelApi);
                responsehotel.EnsureSuccessStatusCode();

                hotelList.AddRange(await responsehotel.Content.ReadFromJsonAsync<IEnumerable<HotelDto>>());
                ViewBag.hotellist = hotelList;

            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(departmentList);
        }
        [HttpGet]
        public async Task<IActionResult> Create( )
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ApiRequests.HotelApi);
            response.EnsureSuccessStatusCode();
            var json=await response.Content.ReadAsStringAsync();
            var hotels=JsonConvert.DeserializeObject<List<HotelDto>>(json);
            var model = new
            {
                items = hotels
            };
            ViewBag.hotels = hotels;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentAddDto departmentAddDto) 
        {
            var client=_httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.DepartmentCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(departmentAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var departmentadded = await httpResponseMessage.Content.ReadFromJsonAsync<DepartmentDto>();
            if (departmentadded != null) 
            { 
            return RedirectToAction("Index","Department");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<DepartmentDto>(ApiRequests.DepartmentApi + $"/{id.ToString()}");

            if (response is not null)
            {
                var responsehotel = await client.GetAsync(ApiRequests.HotelApi);
                responsehotel.EnsureSuccessStatusCode();
                var json = await responsehotel.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<HotelDto>>(json);
                var model = new
                {
                    items = hotels
                };
                ViewBag.hotels = hotels;
                return View(response);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.DepartmentUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<DepartmentDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Department");
            }

            return View();
        }
        public async Task<IActionResult> Delete(DepartmentDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.DepartmentDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Department");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
