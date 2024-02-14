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
        var product = await _productsRepository.GetAsync(request.Id);

        if(product.CreatedBy != request.UserName)
        {
            throw new NadinSoftForbiddenException("Operation is forbidden.");
        }

        if (product is null)
        {
            throw new NadinSoftBusinessException("THe product you want to delete doesn't exists.");
        }

        await _productsRepository.DeleteAsync(request.Id, autoSave: true);
    }
}