using AutoMapper;
using microStore.Services.CommentApi.Models;
using microStore.Services.CommentApi.Models.DTO;


namespace microStore.Services.CommentApi
{
    public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<CommentHeader, CommentHeaderDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
