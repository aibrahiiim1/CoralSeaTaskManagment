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
    public class EfamilyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
    public EfamilyController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<EfamilyDto> efamilyList = new List<EfamilyDto>();

            // Get All Efamilys
            try
            {
                var client = _httpClientFactory.CreateClient();
                var efamilys = await client.GetAsync(ApiRequests.EfamilyApi);
                efamilys.EnsureSuccessStatusCode();
                efamilyList.AddRange(await efamilys.Content.ReadFromJsonAsync<IEnumerable<EfamilyDto>>());
                //ViewBag.Hotels = hotelsBody;
                // Islam C#
                //var client = _httpClientFactory.CreateClient();
                //var response = await client.GetAsync(ApiRequests.EfamilyApi);
                //response.EnsureSuccessStatusCode();
                //var json = await response.Content.ReadAsStringAsync();
                //var efamilys = JsonConvert.DeserializeObject<List<EfamilyDto>>(json);
                //efamilyList.AddRange(efamilys);
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(efamilyList);
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
        public async Task<IActionResult> Create(EfamilyAddDto AddDto) 
        {
            var client=_httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.EfamilyCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(AddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var efamilyadded = await httpResponseMessage.Content.ReadFromJsonAsync<EfamilyDto>();
            if (efamilyadded != null) 
            { 
            return RedirectToAction("Index","Efamily");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<EfamilyDto>(ApiRequests.EfamilyApi + $"/{id.ToString()}");

            if (response is not null)
            {
                var hotelresponse = await client.GetAsync(ApiRequests.HotelApi);
                hotelresponse.EnsureSuccessStatusCode();
                var json = await hotelresponse.Content.ReadAsStringAsync();
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
        public async Task<IActionResult> Edit(EfamilyDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.EfamilyUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<EfamilyDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Efamily");
            }

            return View();
        }
        public async Task<IActionResult> Delete(EfamilyDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.EfamilyDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Efamily");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
