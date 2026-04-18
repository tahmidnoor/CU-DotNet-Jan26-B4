using AutoMapper;
using NorthwindCatalog.Services.Models;
using NorthwindCatalog.Services.DTOs;

namespace NorthwindCatalog.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src =>
                        "/images/" + src.CategoryName
                            .Replace("/", " ") + ".jpg"));

            CreateMap<Product, ProductDto>();
        }
    }
}