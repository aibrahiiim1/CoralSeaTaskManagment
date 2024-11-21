using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HotelController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<HotelDto> hotelsList=new List<HotelDto>();

            // Get All Hotels
            try
            {
                var client = _httpClientFactory.CreateClient();
                var hotels = await client.GetAsync("https://localhost:7097/api/hotel");
                hotels.EnsureSuccessStatusCode();
                hotelsList.AddRange( await hotels.Content.ReadFromJsonAsync<IEnumerable<HotelDto>>());
                //ViewBag.Hotels = hotelsBody;
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            
            return View(hotelsList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HotelAddDto model)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.HotelCreate),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<HotelDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Hotel");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<HotelDto>(ApiRequests.HotelApi+$"/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HotelDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.HotelUpdate+$"/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<HotelDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Hotel");
            }

            return View();
        }                
        public async Task<IActionResult> Delete(HotelDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.HotelDelete+$"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Hotel");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }

        public async Task<IActionResult> IndexModal()
        {
            List<HotelDto> hotelsList = new List<HotelDto>();

            // Get All Hotels
            try
            {
                var client = _httpClientFactory.CreateClient();
                var hotels = await client.GetAsync("https://localhost:7097/api/hotel");
                hotels.EnsureSuccessStatusCode();
                hotelsList.AddRange(await hotels.Content.ReadFromJsonAsync<IEnumerable<HotelDto>>());
                //ViewBag.Hotels = hotelsBody;
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(hotelsList);
        }
        [HttpPost]
        public async Task<IActionResult> EditModal(HotelDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.HotelUpdate + $"/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<HotelDto>();

            if (respose is not null)
            {
                return RedirectToAction("IndexModal", "Hotel");
                //return Ok();
            }

            return View();
        }
    }
}
