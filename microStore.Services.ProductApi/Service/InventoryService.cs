﻿using microStore.Services.ProductApi.Service.IService;
using microStore.Services.ProductApi.Models.DTO;

using System.Text;
using Newtonsoft.Json;

namespace microStore.Services.ProductApi.Service
{
    public class InventoryServices : IInventoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InventoryServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<InventoryDTO> GetInventory(int productId)
        {
            var client = _httpClientFactory.CreateClient("Inventory");

            var res = await client.GetAsync($"api/Inventory/{productId}");
            var apiContent = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            if (response != null)
            {
                var res1 = JsonConvert.DeserializeObject<InventoryDTO>(Convert.ToString(response.Data));
                Console.WriteLine(res1);
                return res1;
            }
            return new InventoryDTO();
        }
    }
}
