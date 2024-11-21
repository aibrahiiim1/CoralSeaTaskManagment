using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class SpartController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SpartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SpartDto> Dtolist = new List<SpartDto>();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ApiRequests.SpartApi);
            response.EnsureSuccessStatusCode();
            Dtolist.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<SpartDto>>());
            return View(Dtolist);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<HotelDto> hotelList = new List<HotelDto>();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ApiRequests.HotelApi);
            response.EnsureSuccessStatusCode();
            hotelList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<HotelDto>>());
            ViewBag.hotels = hotelList;
            return View();
        }        
        [HttpPost]
        public async Task<IActionResult> Create(SpartAddDto outgoingAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.SpartCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(outgoingAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var outgoingAdd = await httpResponseMessage.Content.ReadFromJsonAsync<SpartDto>();
            if(outgoingAdd != null)
            {
                return RedirectToAction("Index", "Spart");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<SpartDto>(ApiRequests.SpartApi + $"/{id.ToString()}");

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
                return View(response);
            }
            return View(null);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(SpartDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.SpartUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<SpartDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Spart");
            }
            return View();
        }
        public async Task<IActionResult> Delete(SpartDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync(ApiRequests.SpartDelete + $"/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Spart");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
