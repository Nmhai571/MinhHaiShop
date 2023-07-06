
using AutoMapper;
using MinhHaiShop.Model.Models;
using MinhHaiShop.Web.Models;

namespace MinhHaiShop.Web.Mappings
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Post, PostViewModel>().ReverseMap().ReverseMap();
            CreateMap<PostCategory, PostCategoryViewModel>().ReverseMap().ReverseMap();
            CreateMap<PostTag, PostTagViewModel>().ReverseMap().ReverseMap();
            CreateMap<Tag, TagViewModel>().ReverseMap().ReverseMap();
            CreateMap<ApplicationUser, SignInViewModel>().ReverseMap().ReverseMap();
            CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap().ReverseMap();
        }
        
    }
}
