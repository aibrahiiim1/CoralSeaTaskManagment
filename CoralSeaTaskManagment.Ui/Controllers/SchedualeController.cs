using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class SchedualeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SchedualeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SchedualeDto> DtoList = new List<SchedualeDto>();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ApiRequests.SchedualeApi);
            response.EnsureSuccessStatusCode();
            DtoList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<SchedualeDto>>());
            return View(DtoList);
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
        public async Task<IActionResult> Create(SchedualeAddDto AddToDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.SchedualeCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(AddToDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var outgoingAdd = await httpResponseMessage.Content.ReadFromJsonAsync<SchedualeDto>();
            if(outgoingAdd != null)
            {
                return RedirectToAction("Index", "Scheduale");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<SchedualeDto>(ApiRequests.SchedualeApi + $"/{id.ToString()}");

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
        public async Task<IActionResult> Edit(SchedualeDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.SchedualeUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<SchedualeDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Scheduale");
            }
            return View();
        }
        public async Task<IActionResult> Delete(SchedualeDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync(ApiRequests.SchedualeDelete + $"/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Scheduale");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> CreateModal([FromBody] SchedualeAddDto AddToDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.SchedualeCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(AddToDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var outgoingAdd = await httpResponseMessage.Content.ReadFromJsonAsync<SchedualeDto>();
            if (outgoingAdd != null)
            {
                return RedirectToAction("Index", "Scheduale");
            }

            return View();
        }
    }
}
