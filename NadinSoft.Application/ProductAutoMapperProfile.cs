using AutoMapper;

using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

namespace NadinSoft.Application;

public class ProductAutoMapperProfile : Profile
{
    public ProductAutoMapperProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}