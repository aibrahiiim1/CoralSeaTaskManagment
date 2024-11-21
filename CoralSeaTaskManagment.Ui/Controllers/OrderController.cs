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
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<OrderDto> orderList = new List<OrderDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.OrderApi);
                response.EnsureSuccessStatusCode();
                orderList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>());
                var orderVM=orderList.ToList().Select(x => new OrderVM
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    AssignFlag=x.AssignFlag.ToString(),
                    Comment = x.Comment,
                    CreatedTime = x.CreatedTime,
                    DepartmentFId = x.DepartmentFrom.Name,
                    DepartmentId = x.Departments.Name,
                    HotelId = x.Hotels.Name,
                    ItemId = x.Items.Name,
                    LocationId = x.Locations.Name,
                    OstatusId = x.OstatusId.ToString(),
                    OtypeId= x.Otypes.Name,
                    PeriorityId = x.Periorities.Name,
                    Pic= x.Pic
                }).ToList();
                ViewBag.data = orderVM;

                var responseAssign = await client.GetAsync(ApiRequests.AssignApi);
                responseAssign.EnsureSuccessStatusCode();
                var json = await responseAssign.Content.ReadAsStringAsync();
                var assigns = JsonConvert.DeserializeObject<List<AssignDto>>(json);
                ViewBag.assignlist = assigns;

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

            var responsejsoneperiority = await client.GetAsync(ApiRequests.PeriorityApi);
            responsejsoneperiority.EnsureSuccessStatusCode();
            var jsonperiority = await responsejsoneperiority.Content.ReadAsStringAsync();
            var periority = JsonConvert.DeserializeObject<List<PeriorityDto>>(jsonperiority);
            var modelperiority = new
            {
                items = periority
            };
            ViewBag.periority = periority;

            var responseotype = await client.GetAsync(ApiRequests.OtypeApi);
            responseotype.EnsureSuccessStatusCode();
            var jsonotype = await responseotype.Content.ReadAsStringAsync();
            var otype = JsonConvert.DeserializeObject<List<OtypeDto>>(jsonotype);
            var modelotype = new
            {
                items = otype
            };
            ViewBag.otype = otype;

            var responseitem = await client.GetAsync(ApiRequests.ItemApi);
            responseitem.EnsureSuccessStatusCode();
            var jsonitem = await responseitem.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<List<ItemDto>>(jsonitem);
            var modelitem = new
            {
                items = item
            };
            ViewBag.item = item;

            var responselocation = await client.GetAsync(ApiRequests.LocationApi);
            responselocation.EnsureSuccessStatusCode();
            var jsonlocation = await responselocation.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<List<LocationDto>>(jsonlocation);
            var modellocation = new
            {
                items = location
            };
            ViewBag.location = location;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderAddDto orderAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.OrderCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(orderAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var orderadded = await httpResponseMessage.Content.ReadFromJsonAsync<OrderDto>();
            if (orderadded != null)
            {
                return RedirectToAction("Index", "Order");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<OrderDto>(ApiRequests.OrderApi + $"/{id.ToString()}");

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

                var responsedepartments = await client.GetAsync(ApiRequests.DepartmentApi);
                responsedepartments.EnsureSuccessStatusCode();
                var jsondepartmens = await responsedepartments.Content.ReadAsStringAsync();
                var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsondepartmens);
                var modelitems = new
                {
                    items = departments
                };
                ViewBag.departments = departments;

                var responsejsoneperiority = await client.GetAsync(ApiRequests.PeriorityApi);
                responsejsoneperiority.EnsureSuccessStatusCode();
                var jsonperiority = await responsejsoneperiority.Content.ReadAsStringAsync();
                var periority = JsonConvert.DeserializeObject<List<PeriorityDto>>(jsonperiority);
                var modelperiority = new
                {
                    items = periority
                };
                ViewBag.periority = periority;

                var responseotype = await client.GetAsync(ApiRequests.OtypeApi);
                responseotype.EnsureSuccessStatusCode();
                var jsonotype = await responseotype.Content.ReadAsStringAsync();
                var otype = JsonConvert.DeserializeObject<List<OtypeDto>>(jsonotype);
                var modelotype = new
                {
                    items = otype
                };
                ViewBag.otype = otype;

                var responseitem = await client.GetAsync(ApiRequests.ItemApi);
                responseitem.EnsureSuccessStatusCode();
                var jsonitem = await responseitem.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<List<ItemDto>>(jsonitem);
                var modelitem = new
                {
                    items = item
                };
                ViewBag.item = item;

                var responselocation = await client.GetAsync(ApiRequests.LocationApi);
                responselocation.EnsureSuccessStatusCode();
                var jsonlocation = await responselocation.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<List<LocationDto>>(jsonlocation);
                var modellocation = new
                {
                    items = location
                };
                ViewBag.location = location;
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.OrderUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<OrderDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Order");
            }
            return View();
        }

        public async Task<IActionResult> Getloc(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responselocation = await client.GetAsync(ApiRequests.LocationByDep + $"/{id.ToString()}");
            responselocation.EnsureSuccessStatusCode();
            var jsonlocation = await responselocation.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<List<LocationDto>>(jsonlocation);
            var modellocation = new
            {
                items = location
            };
            ViewBag.location = location;
            return Ok(location);
        }

        public async Task<IActionResult> GetNew()
        {
            List<OrderDto> orderList = new List<OrderDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.OrderApi);
                response.EnsureSuccessStatusCode();
                orderList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>());
                var orderVM = orderList.ToList().Select(x => new OrderVM
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    AssignFlag = x.AssignFlag.ToString(),
                    Comment = x.Comment,
                    CreatedTime = x.CreatedTime,
                    DepartmentFId = x.DepartmentFrom.Name,
                    DepartmentId = x.Departments.Name,
                    HotelId = x.Hotels.Name,
                    ItemId = x.Items.Name,
                    LocationId = x.Locations.Name,
                    OstatusId = x.OstatusId.ToString(),
                    OtypeId = x.Otypes.Name,
                    PeriorityId = x.Periorities.Name,
                    Pic = x.Pic
                }).ToList();
                ViewBag.data = orderVM;

                var responseAssign = await client.GetAsync(ApiRequests.AssignApi);
                responseAssign.EnsureSuccessStatusCode();
                var json = await responseAssign.Content.ReadAsStringAsync();
                var assigns = JsonConvert.DeserializeObject<List<AssignDto>>(json);
                ViewBag.assignlist = assigns;

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
    }
}
