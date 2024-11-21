using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class OutgoingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OutgoingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OutgoingDto> outgoingList = new List<OutgoingDto>();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ApiRequests.OutgoingApi);
            response.EnsureSuccessStatusCode();
            outgoingList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<OutgoingDto>>());
            return View(outgoingList);
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
        public async Task<IActionResult> Create(OutgoingAddDto outgoingAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.OutgoingCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(outgoingAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var outgoingAdd = await httpResponseMessage.Content.ReadFromJsonAsync<OutgoingDto>();
            if(outgoingAdd != null)
            {
                return RedirectToAction("Index", "Outgoing");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<OutgoingDto>(ApiRequests.OutgoingApi + $"/{id.ToString()}");

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
        public async Task<IActionResult> Edit(OutgoingDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.OutgoingUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<OutgoingDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Outgoing");
            }
            return View();
        }
        public async Task<IActionResult> Delete(OutgoingDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync(ApiRequests.OutgoingDelete + $"/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Outgoing");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> CreateModal([FromBody]OutgoingAddDto outgoingAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.OutgoingCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(outgoingAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var outgoingAdd = await httpResponseMessage.Content.ReadFromJsonAsync<OutgoingDto>();
            if (outgoingAdd != null)
            {
                return RedirectToAction("Index", "Outgoing");
            }

            return View();
        }
    }
}
