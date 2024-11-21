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
    public class AssignationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AssignationController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<AssignationDto> AssignationList = new List<AssignationDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(ApiRequests.AssignationApi);
                response.EnsureSuccessStatusCode();
                AssignationList.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<AssignationDto>>());
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
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(ApiRequests.AssignApi);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var assigns = JsonConvert.DeserializeObject<List<AssignDto>>(json);
            ViewBag.assignlist = assigns;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssignationAddDto assignationAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.AssignationCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignationAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var assignationAdd = await httpResponseMessage.Content.ReadFromJsonAsync<AssignationDto>();

            // Order Switch to Assigned
            var request = new OrderDto();
            request.Id = assignationAddDto.OrderId;
            var httpRequestMessageAssign = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.OrderUpdateAss + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessageAssign = await client.SendAsync(httpRequestMessageAssign);
            httpResponseMessageAssign.EnsureSuccessStatusCode();
           

            //---------------------------------
            if (assignationAdd != null)
            {
                //return RedirectToAction("Index", "Assignation");
                return Ok();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<AssignationDto>(ApiRequests.AssignationApi + $"/{id.ToString()}");
            if (response is not null)
            {
                var responseassign = await client.GetAsync(ApiRequests.AssignApi);
                responseassign.EnsureSuccessStatusCode();
                var json = await responseassign.Content.ReadAsStringAsync();
                var assignlist = JsonConvert.DeserializeObject<List<AssignDto>>(json);
                ViewBag.assignlist = assignlist;
                return View(response);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AssignationDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.AssignationUpdate + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<AssignationDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Assignation");
            }
            return View();
        }
        public async Task<IActionResult> Delete(AssignationDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync(ApiRequests.AssignationDelete + $"/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
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
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.AssignationCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignationAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var assignationAdd = await httpResponseMessage.Content.ReadFromJsonAsync<AssignationDto>();

            // Order Switch to Assigned
            var request = new OrderDto();
            request.Id = assignationAddDto.OrderId;
            var httpRequestMessageAssign = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.OrderUpdateAss + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessageAssign = await client.SendAsync(httpRequestMessageAssign);
            httpResponseMessageAssign.EnsureSuccessStatusCode();


            //---------------------------------
            if (assignationAdd != null)
            {
                return RedirectToAction("Index", "Assignation"); 
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateModal([FromBody] AssignationAddDto assignationAddDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(ApiRequests.AssignationCreate),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignationAddDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var assignationAdd = await httpResponseMessage.Content.ReadFromJsonAsync<AssignationDto>();

            // Order Switch to Assigned
            var request = new OrderDto();
            request.Id = assignationAddDto.OrderId;
            var httpRequestMessageAssign = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(ApiRequests.OrderUpdateAss + $"/{request.Id}"),
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessageAssign = await client.SendAsync(httpRequestMessageAssign);
            httpResponseMessageAssign.EnsureSuccessStatusCode();


            //---------------------------------
            if (assignationAdd != null)
            {
                //return RedirectToAction("Index", "Assignation");
                return Ok();
            }
            return View();
        }
    }
}
