using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using CoralSeaTaskManagment.Ui.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class GorderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GorderController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<GorderDto> orderList = new List<GorderDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.GorderApi);
                response.EnsureSuccessStatusCode();
                orderList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<GorderDto>>());
                var orderVM=orderList.ToList().Select(x => new GorderVM
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    AssignFlag=x.AssignFlag.ToString(),
                    Comment = x.Comment,
                    CreatedTime = x.CreatedTime,
                    GdepartmentFId = x.GdepartmentFrom.Name,
                    DepartmentId = x.Departments.Name,
                    HotelId = x.Hotels.Name,
                    GitemId = x.Gitems.Name,
                    GroomsId = x.Grooms.Name,
                    GlocationId = x.Glocations.Name,
                    OstatusId = x.OstatusId.ToString(),
                    OtypeId= x.Otypes.Name,
                }).ToList();
                ViewBag.data = orderVM;

                var responsehotels = await client.GetAsync(ApiRequests.HotelApi);
                responsehotels.EnsureSuccessStatusCode();
                var jsonhotels = await responsehotels.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<HotelDto>>(jsonhotels);
                ViewBag.hotellist = hotels;
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            
            return View();
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
            ViewBag.hotels = hotels;

            var responsedepartments = await client.GetAsync(ApiRequests.DepartmentApi);
            responsedepartments.EnsureSuccessStatusCode();
            var jsondepartmens = await responsedepartments.Content.ReadAsStringAsync();
            var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsondepartmens);
            var modelitems = new
            {
                items = departments
            };
            ViewBag.departments = departments;

            var responseotype = await client.GetAsync(ApiRequests.OtypeApi);
            responseotype.EnsureSuccessStatusCode();
            var jsonotype = await responseotype.Content.ReadAsStringAsync();
            var otype = JsonConvert.DeserializeObject<List<OtypeDto>>(jsonotype);
            var modelotype = new
            {
                items = otype
            };
            ViewBag.otype = otype;

            var responseitem = await client.GetAsync(ApiRequests.GitemApi);
            responseitem.EnsureSuccessStatusCode();
            var jsonitem = await responseitem.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<List<GitemDto>>(jsonitem);
            var modelitem = new
            {
                items = item
            };
            ViewBag.item = item;

            var responselocation = await client.GetAsync(ApiRequests.GlocationApi);
            responselocation.EnsureSuccessStatusCode();
            var jsonlocation = await responselocation.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<List<GlocationDto>>(jsonlocation);
            var modellocation = new
            {
                items = location
            };
            ViewBag.location = location;           
            
            var responseGroom = await client.GetAsync(ApiRequests.GroomsApi);
            responseGroom.EnsureSuccessStatusCode();
            var jsonGroom = await responseGroom.Content.ReadAsStringAsync();
            var Groom = JsonConvert.DeserializeObject<List<GroomDto>>(jsonGroom);
            var modelGroom = new
            {
                items = location
            };
            ViewBag.groom = Groom;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GorderAddDto orderAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.GorderCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(orderAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var orderadded = await httpResponseMessage.Content.ReadFromJsonAsync<GorderDto>();
            if (orderadded != null)
            {
                return RedirectToAction("Index", "Gorder");
            }
            return View();
        }
    }
}
