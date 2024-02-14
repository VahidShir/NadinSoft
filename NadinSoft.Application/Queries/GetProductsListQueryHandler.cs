using AutoMapper;

using MediatR;

using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

namespace NadinSoft.Application.Queries;

public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IEnumerable<ProductDto>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductsListQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetAllAsync();

        if (products?.Count() > 0)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        return Enumerable.Empty<ProductDto>();
    }
}