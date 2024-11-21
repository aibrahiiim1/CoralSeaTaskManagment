using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using CoralSeaTaskManagment.Ui.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class OtypeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OtypeController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<OtypeDto> list = new List<OtypeDto>();

            // Get All Departments
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.OtypeApi);
                response.EnsureSuccessStatusCode();
                list.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<OtypeDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            return View(list);
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
            var apiResponse = await client.GetStringAsync(ApiRequests.OtypeEnum);
            var enumList = JsonConvert.DeserializeObject<List<OtypeEnumVM>>(apiResponse);
            ViewBag.otype = Enum.GetValues(typeof(FronthousEnum))
                .Cast<FronthousEnum>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.ToString()
                }).ToList();
            ViewBag.hotels = hotels;
            //ViewBag.otype = viewModel;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OtypeAddDto addDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.OtypeCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(addDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var locationadd = await httpResponseMessage.Content.ReadFromJsonAsync<AssignDto>();
            if (locationadd != null)
            {
                return RedirectToAction("Index", "Otype");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<OtypeDto>(ApiRequests.OtypeApi + $"/{id.ToString()}");

            if (response is not null)
            {
                // for fill hotels and otype enum
                var responsehotels = await client.GetAsync(ApiRequests.HotelApi);
                responsehotels.EnsureSuccessStatusCode();
                var jsonhotels = await responsehotels.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<HotelDto>>(jsonhotels);
                var modelhotels = new
                {
                    items = hotels
                };
                var apiResponse = await client.GetStringAsync(ApiRequests.OtypeEnum);
                var enumList = JsonConvert.DeserializeObject<List<OtypeEnumVM>>(apiResponse);
                ViewBag.otype = Enum.GetValues(typeof(FronthousEnum))
                    .Cast<FronthousEnum>()
                    .Select(s => new SelectListItem
                    {
                        Value = ((int)s).ToString(),
                        Text = s.ToString()
                    }).ToList();
                ViewBag.hotels = hotels;

                return View(response);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OtypeDto request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.OtypeUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<OtypeDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Otype");
            }

            return View();
        }
        public async Task<IActionResult> Delete(OtypeDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync(ApiRequests.OtypeDelete + $"/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Otype");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
