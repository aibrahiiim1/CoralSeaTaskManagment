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
    public class EcategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EcategoryController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<EcategoryDto> EcategoryList = new List<EcategoryDto>();

            // Get All Ecategorys
            try
            {
                var client = _httpClientFactory.CreateClient();
                var ecategory = await client.GetAsync(ApiRequests.EcategoryApi);
                ecategory.EnsureSuccessStatusCode();
                EcategoryList.AddRange(await ecategory.Content.ReadFromJsonAsync<IEnumerable<EcategoryDto>>());
                //ViewBag.Hotels = hotelsBody;
                // Islam C#
                //var client = _httpClientFactory.CreateClient();
                //var response = await client.GetAsync(ApiRequests.EcategoryApi);
                //response.EnsureSuccessStatusCode();
                //var json = await response.Content.ReadAsStringAsync();
                //var ecategory = JsonConvert.DeserializeObject<List<EcategoryDto>>(json);
                //EcategoryList.AddRange(ecategory);
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(EcategoryList);
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
        public async Task<IActionResult> Create(EcategoryAddDto AddDto) 
        {
            var client=_httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.EcategoryCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(AddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var ecategoryaddDto = await httpResponseMessage.Content.ReadFromJsonAsync<EcategoryDto>();
            if (ecategoryaddDto != null) 
            { 
            return RedirectToAction("Index", "Ecategory");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<EcategoryDto>(ApiRequests.EcategoryApi + $"/{id.ToString()}");

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
        public async Task<IActionResult> Edit(EcategoryDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.EcategoryUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<EcategoryDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Ecategory");
            }

            return View();
        }
        public async Task<IActionResult> Delete(EcategoryDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.EcategoryDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Ecategory");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
