using AutoMapper;
using NLayerArchitecture.Repositories.Categories;
using NLayerArchitecture.Services.Categories.Create;
using NLayerArchitecture.Services.Categories.Dto;
using NLayerArchitecture.Services.Categories.Update;

namespace NLayerArchitecture.Services.Categories.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryWithProductsDto>();
        CreateMap<CreateCategoryResponse, Category>();
        CreateMap<UpdateCategoryResponse, Category>();
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest=>dest.Name,opt=>
                opt.MapFrom(src=>src.Name.ToLowerInvariant()));
        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest=>dest.Name,opt=>
                opt.MapFrom(src=>src.Name.ToLowerInvariant()));
    }
}