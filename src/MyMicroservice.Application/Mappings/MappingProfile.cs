using AutoMapper;
using MyMicroservice.Application.Features.Products.Queries.GetProductById;
using MyMicroservice.Domain.Entities;

namespace MyMicroservice.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency));
    }
}