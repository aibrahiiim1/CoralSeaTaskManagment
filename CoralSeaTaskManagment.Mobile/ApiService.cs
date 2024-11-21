using CoralSeaTaskManagment.Model.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoralSeaTaskManagment.Mobile
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7097/") };
        }

        public async Task<List<Hotel>> GetModelsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Hotel>>("api/Hotel");
        }

        // Add other methods for POST, PUT, DELETE as needed
    }

}
