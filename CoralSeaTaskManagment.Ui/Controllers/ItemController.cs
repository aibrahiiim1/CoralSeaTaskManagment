using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using CoralSeaTaskManagment.Ui.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class ItemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ItemController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<ItemDto> ItemList = new List<ItemDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.ItemApi);
                response.EnsureSuccessStatusCode();
                ItemList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            ViewBag.data = ItemList;
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
            
            var responseeclass = await client.GetAsync(ApiRequests.EclassApi);
            responseeclass.EnsureSuccessStatusCode();
            var jsoneclass = await responseeclass.Content.ReadAsStringAsync();
            var eclass = JsonConvert.DeserializeObject<List<EclassDto>>(jsoneclass);
            var modeleclass = new
            {
                items = eclass
            };
            ViewBag.eclass = eclass;            
            
            var responseefamily = await client.GetAsync(ApiRequests.EfamilyApi);
            responseefamily.EnsureSuccessStatusCode();
            var jsonefamily = await responseefamily.Content.ReadAsStringAsync();
            var efamily = JsonConvert.DeserializeObject<List<EfamilyDto>>(jsonefamily);
            var modelefamily = new
            {
                items = efamily
            };
            ViewBag.efamily = efamily;            
            
            var responseecategory = await client.GetAsync(ApiRequests.EcategoryApi);
            responseecategory.EnsureSuccessStatusCode();
            var jsonecategory = await responseecategory.Content.ReadAsStringAsync();
            var ecategory = JsonConvert.DeserializeObject<List<EcategoryDto>>(jsonecategory);
            var modelecategory = new
            {
                items = ecategory
            };
            ViewBag.ecategory = ecategory;            
            
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
        public async Task<IActionResult> Create(ItemAddDto itemAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.ItemCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(itemAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var itemadded = await httpResponseMessage.Content.ReadFromJsonAsync<ItemDto>();
            if (itemadded != null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ItemDto>(ApiRequests.ItemApi + $"/{id.ToString()}");

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

                var responseeclass = await client.GetAsync(ApiRequests.EclassApi);
                responseeclass.EnsureSuccessStatusCode();
                var jsoneclass = await responseeclass.Content.ReadAsStringAsync();
                var eclass = JsonConvert.DeserializeObject<List<EclassDto>>(jsoneclass);
                var modeleclass = new
                {
                    items = eclass
                };
                ViewBag.eclass = eclass;

                var responseefamily = await client.GetAsync(ApiRequests.EfamilyApi);
                responseefamily.EnsureSuccessStatusCode();
                var jsonefamily = await responseefamily.Content.ReadAsStringAsync();
                var efamily = JsonConvert.DeserializeObject<List<EfamilyDto>>(jsonefamily);
                var modelefamily = new
                {
                    items = efamily
                };
                ViewBag.efamily = efamily;

                var responseecategory = await client.GetAsync(ApiRequests.EcategoryApi);
                responseecategory.EnsureSuccessStatusCode();
                var jsonecategory = await responseecategory.Content.ReadAsStringAsync();
                var ecategory = JsonConvert.DeserializeObject<List<EcategoryDto>>(jsonecategory);
                var modelecategory = new
                {
                    items = ecategory
                };
                ViewBag.ecategory = ecategory;

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
        public async Task<IActionResult> Edit(ItemDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.ItemUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<ItemDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View();
        }
        public async Task<IActionResult> Delete(ItemDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync(ApiRequests.ItemDelete + $"/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Item");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
        public async Task<IActionResult> GetItemByLoc(int id)
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
        [HttpGet]
        public async Task<IActionResult> EditForm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ItemDto>(ApiRequests.ItemApi + $"/{id.ToString()}");
            var vm = new ItemVM() { Id=response.Id,Name=response.Name};
            // Return the _EditForm partial view with the model
            return PartialView("_EditForm",vm);
        }        
        [HttpPost]
        public async Task<IActionResult> EditFormPost(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ItemDto>(ApiRequests.ItemApi + $"/{id.ToString()}");
            var vm = new ItemVM() { Id=response.Id,Name=response.Name};
            // Return the _EditForm partial view with the model
            return PartialView("_EditForm",vm);
        }
    }
}
