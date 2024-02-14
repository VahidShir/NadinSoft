using AutoMapper;

using MediatR;

using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

namespace NadinSoft.Application.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
        private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(request.Id);

        if (product is not null)
        {
            return _mapper.Map<ProductDto>(product);
        }

        return null;
    }
}