

using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Service.IService
{
    public interface ICommentService
    {
        Task<CommentHeaderDTO> GetCommentsAsync(int productId, int page, int size);
    }
}
