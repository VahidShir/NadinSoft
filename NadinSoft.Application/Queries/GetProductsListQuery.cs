using MediatR;

using NadinSoft.Application.Contracts;

namespace NadinSoft.Application.Queries;

public class GetProductsListQuery : IRequest<IEnumerable<ProductDto>>
{
}