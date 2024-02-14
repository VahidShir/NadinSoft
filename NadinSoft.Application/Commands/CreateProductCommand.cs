using MediatR;

using NadinSoft.Application.Contracts;

namespace NadinSoft.Application.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public ProductCreateDto Product { get; set; }

    public CreateProductCommand(ProductCreateDto product)
    {
        Product = product;
    }
}