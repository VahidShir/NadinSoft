using MediatR;

using NadinSoft.Application.Contracts;

namespace NadinSoft.Application.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public Guid CreatedBy { get; set; }

    public ProductCreateDto Product { get; set; }

    public CreateProductCommand(Guid CreatedBy, ProductCreateDto product)
    {
        Product = product;
    }
}