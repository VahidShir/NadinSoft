using MediatR;

using NadinSoft.Application.Contracts;

namespace NadinSoft.Application.Queries;

public class GetProductsListQuery : IRequest<IEnumerable<ProductDto>>
{
    public string UserName { get; set; }

    public GetProductsListQuery(string userName)
    {
        UserName = userName;
    }
}