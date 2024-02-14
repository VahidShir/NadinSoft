using MediatR;

namespace NadinSoft.Application.Commands;

public class DeleteProductCommand : IRequest
{
    public long Id { get; set; }
    public string UserName { get; set; }

    public DeleteProductCommand(string userName, long id)
    {
        Id = id;
        UserName = userName;
    }
}