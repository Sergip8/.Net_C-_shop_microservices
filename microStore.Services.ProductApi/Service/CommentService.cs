using microStore.Services.ProductApi.Models.DTO;
using microStore.Services.ProductApi.Service.IService;

using Newtonsoft.Json;
using System.Text;


namespace microStore.Services.ProductApi.Service
{
    public class CommentService : ICommentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CommentHeaderDTO> GetCommentsAsync(int productId, int page, int size)
        {
            var client = _httpClientFactory.CreateClient("Comment");

            var res = await client.GetAsync($"api/Comment/{productId}/{page}/{size}");
            var apiContent = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            if ((bool)response.Success)
            {
                return JsonConvert.DeserializeObject<CommentHeaderDTO>(Convert.ToString(response.Data));
            }
            return new CommentHeaderDTO();
        }

    }
}
