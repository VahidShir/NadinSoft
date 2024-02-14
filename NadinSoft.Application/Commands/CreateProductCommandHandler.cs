using AutoMapper;

using MediatR;

using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

namespace NadinSoft.Application.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly ProductManager _productManager;
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(ProductManager productManager, IProductsRepository productsRepository, IMapper mapper)
    {
        _productManager = productManager;
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productManager.CreateProduct(
            createdBy: request.CreatedBy,
            name: request.Product.Name,
            produceDate: request.Product.ProduceDate,
            manufacturePhone: request.Product.ManufacturePhone,
            manufactureEmail: request.Product.ManufactureEmail,
            isAvailable: request.Product.IsAvailable);

        await _productsRepository.InsertAsync(product, autoSave: true);

        return _mapper.Map<ProductDto>(product);
    }
}