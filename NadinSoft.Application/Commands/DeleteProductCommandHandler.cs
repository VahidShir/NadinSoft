using MediatR;

using NadinSoft.Domain;

namespace NadinSoft.Application.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductsRepository _productsRepository;

    public DeleteProductCommandHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        bool exists = await _productsRepository.AnyAsync(x => x.Id == request.Id);

        if (!exists)
        {
            throw new NadinSoftBusinessException("THe product you want to delete doesn't exists.");
        }

        await _productsRepository.DeleteAsync(request.Id, autoSave: true);
    }
}