using MediatR;

namespace NadinSoft.Application.Commands;

public class DeleteProductCommand : IRequest
{
    public long Id { get; set; }

    public DeleteProductCommand(long id)
    {
        Id = id;
    }
}