using MediatR;

using NadinSoft.Application.Contracts;

namespace NadinSoft.Application.Queries;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public long Id { get; set; }
}