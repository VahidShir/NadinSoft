using MediatR;

using NadinSoft.Application.Contracts;

namespace NadinSoft.Application.Commands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string CreatedBy { get; set; }

    public ProductCreateDto Product { get; set; }

    public CreateProductCommand(string createdBy, ProductCreateDto product)
    {
        Product = product;
        CreatedBy = createdBy;
    }
}