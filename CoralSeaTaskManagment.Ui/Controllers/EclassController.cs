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
    public class EclassController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EclassController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<EclassDto> eclassList = new List<EclassDto>();

            // Get All Eclasss
            try
            {
                var client = _httpClientFactory.CreateClient();
                var eclasss = await client.GetAsync(ApiRequests.EclassApi);
                eclasss.EnsureSuccessStatusCode();
                eclassList.AddRange(await eclasss.Content.ReadFromJsonAsync<IEnumerable<EclassDto>>());
                //ViewBag.Hotels = hotelsBody;
                // Islam C#
                //var client = _httpClientFactory.CreateClient();
                //var response = await client.GetAsync(ApiRequests.EclassApi);
                //response.EnsureSuccessStatusCode();
                //var json = await response.Content.ReadAsStringAsync();
                //var eclasss = JsonConvert.DeserializeObject<List<EclassDto>>(json);
                //eclassList.AddRange(eclasss);
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(eclassList);
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
        public async Task<IActionResult> Create(EclassAddDto AddDto) 
        {
            var client=_httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.EclassCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(AddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var eclassadded = await httpResponseMessage.Content.ReadFromJsonAsync<EclassDto>();
            if (eclassadded != null) 
            {

                return RedirectToAction("Index","Eclass");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<EclassDto>(ApiRequests.EclassApi + $"/{id.ToString()}");

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
        public async Task<IActionResult> Edit(EclassDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.EclassUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<EclassDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Eclass");
            }

            return View();
        }
        public async Task<IActionResult> Delete(EclassDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.EclassDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Eclass");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
