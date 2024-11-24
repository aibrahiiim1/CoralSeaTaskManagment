using CoralSeaTaskManagment.Services;
using CoralSeaTaskManagment.Ui.Helper;
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
        private readonly APIService _aPIService;
        public HotelController(IHttpClientFactory httpClientFactory, APIService aPIService )
        {
            this._httpClientFactory = httpClientFactory;
            _aPIService= aPIService;
        }
        public async Task<IActionResult> Index()
        {
            List<HotelDto> hotelsList=new List<HotelDto>();

            // Get All Hotels
            try
            {
             var check=   Identity.Token;
              var Requset = await _aPIService.GetAsync(ApiRequests.HotelApi);
                if (Requset.IsSuccessStatusCode)
                {
                    hotelsList.AddRange(await Requset.Content.
                        ReadFromJsonAsync<IEnumerable<HotelDto>>());
                  //  ViewBag.Hotels = hotelsBody;
                }            
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
           
            var Requset = await _aPIService.PostDataAsync(ApiRequests.HotelCreate, model);
            if (Requset.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Hotel");
            }
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Requset = await _aPIService.GetAsync(ApiRequests.HotelApi + $"/{id.ToString()}");
            if (Requset.IsSuccessStatusCode)
            {
                var respose = await Requset.Content.ReadFromJsonAsync<HotelDto>();
                if (respose is not null)
                {
                    return View(respose);
                }
            }
            
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HotelDto request)
        {
            var Requset = await _aPIService.PutDataAsync(ApiRequests.HotelUpdate + $"/{request.Id}", request);
            if (Requset.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Hotel");
            }
            return View();
        }                
        public async Task<IActionResult> Delete(HotelDto request)
        {
            try
            {
                
                var Requset = await _aPIService.DeleteDataAsync(ApiRequests.HotelDelete + $"/{request.Id}");

                if (Requset.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Hotel");
                }
              
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
