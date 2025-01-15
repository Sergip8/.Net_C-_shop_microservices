
using microStore.Services.CommentApi.Models.DTO;

namespace microStore.Services.CommentApi.Service.IService
{
    public interface ICommentService
    {
        Task<ResponseDTO> GetCommentsByProductId(int productId, int page, int size);
        Task<ResponseDTO> StoreComment(CommentHeaderWriteDTO commentDTO);
        Task<object> GetCommentsByCommentHeaderId(int commentHeaderId, int page, int size);
    }
}
