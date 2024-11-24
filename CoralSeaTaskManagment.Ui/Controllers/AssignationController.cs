using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Services;
using CoralSeaTaskManagment.Ui.Models;
using CoralSeaTaskManagment.Ui.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoralSeaTaskManagment.Ui.Controllers
{
    public class AssignationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly APIService _aPIService;

        public AssignationController(IHttpClientFactory httpClientFactory, APIService aPIService)
        {
            this._httpClientFactory = httpClientFactory;
            _aPIService = aPIService;
        }
        public async Task<IActionResult> Index()
        {
            List<AssignationDto> AssignationList = new List<AssignationDto>();
            try
            {
                var Requset = await _aPIService.GetAsync(ApiRequests.AssignationApi);
                if (Requset.IsSuccessStatusCode)
                {
                    AssignationList.AddRange(await Requset.Content.ReadFromJsonAsync<IEnumerable<AssignationDto>>());
                }
            }
            catch (Exception ex)
            {
                // Log the exception
            }
            return View(AssignationList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Requset = await _aPIService.GetAsync(ApiRequests.AssignApi);
            if (Requset.IsSuccessStatusCode)
            {
                var json = await Requset.Content.ReadAsStringAsync();
                var assigns = JsonConvert.DeserializeObject<List<AssignDto>>(json);
                ViewBag.assignlist = assigns;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssignationAddDto assignationAddDto)
        {
            try
            {
                var Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignationAddDto),
                        Encoding.UTF8, "application/json");
                var Requset = await _aPIService.PostDataAsync(ApiRequests.AssignationCreate, assignationAddDto);
                if (Requset.IsSuccessStatusCode)
                {
                    var assignationAdd = await Requset.Content.ReadFromJsonAsync<AssignationDto>();

                    var orderRequset = await _aPIService.PutDataAsync(ApiRequests.OrderUpdateAss +
                        $"/{assignationAddDto.OrderId}", null);
                    if (orderRequset.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                  
                   
                }
                return BadRequest("Error");
            }
            catch (Exception)
            {

                return BadRequest("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Requset = await _aPIService.GetAsync(ApiRequests.AssignationApi + $"/{id.ToString()}");
            if (Requset.IsSuccessStatusCode)
            {
                var json = await Requset.Content.ReadAsStringAsync();
                var assignlist = JsonConvert.DeserializeObject<List<AssignDto>>(json);
                ViewBag.assignlist = assignlist;
                return View(assignlist);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AssignationDto request)
        {
            var Requset = await _aPIService.PutDataAsync(ApiRequests.AssignationUpdate + $"/{request.Id}", request);
            if (Requset.IsSuccessStatusCode)
            {
                var respose = await Requset.Content.ReadFromJsonAsync<AssignationDto>();
                if (respose is not null)
                {
                    return RedirectToAction("Index", "Assignation");
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(AssignationDto request)
        {
            try
            {
                var Requset = await _aPIService.DeleteDataAsync(ApiRequests.AssignationDelete + $"/{request.Id}");
                if (Requset.IsSuccessStatusCode)
                return RedirectToAction("Index", "Assignation");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> CreateForm(AssignationAddDto assignationAddDto)
        {
            try
            {
                var Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignationAddDto),
                        Encoding.UTF8, "application/json");
                var Requset = await _aPIService.PostDataAsync(ApiRequests.AssignationCreate, assignationAddDto);
                if (Requset.IsSuccessStatusCode)
                {
                    var assignationAdd = await Requset.Content.ReadFromJsonAsync<AssignationDto>();

                    var orderRequset = await _aPIService.PutDataAsync(ApiRequests.OrderUpdateAss +
                        $"/{assignationAddDto.OrderId}", null);
                    if (orderRequset.IsSuccessStatusCode)
                    {
                        return Ok();
                    }


                }
                return BadRequest("Error");
            }
            catch (Exception)
            {

                return BadRequest("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateModal([FromBody] AssignationAddDto assignationAddDto)
        {
            try
            {
                var Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignationAddDto),
                        Encoding.UTF8, "application/json");
                var Requset = await _aPIService.PostDataAsync(ApiRequests.AssignationCreate, assignationAddDto);
                if (Requset.IsSuccessStatusCode)
                {
                    var assignationAdd = await Requset.Content.ReadFromJsonAsync<AssignationDto>();

                    var orderRequset = await _aPIService.PutDataAsync(ApiRequests.OrderUpdateAss +
                        $"/{assignationAddDto.OrderId}", null);
                    if (orderRequset.IsSuccessStatusCode)
                    {
                        return Ok();
                    }


                }
                return BadRequest("Error");
            }
            catch (Exception)
            {

                return BadRequest("Error");
            }
        }
    }
}
