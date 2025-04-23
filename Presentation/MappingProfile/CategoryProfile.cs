using Application.DTOS.CategoriesDto;
using Presentation.ViewModels.CategoriesViewModel;
using AutoMapper;

namespace Presentation.MappingProfile
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<GetAllCategroiesViewModel, GetAllCategoriesDto>().ReverseMap();
        }
    }
}
