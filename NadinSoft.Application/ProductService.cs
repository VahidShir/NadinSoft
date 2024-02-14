using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

namespace NadinSoft.Application;

public class ProductService : IProductService
{
    private readonly ProductManager _productManager;
    private readonly IProductsRepository _productsRepository;

    public ProductService(ProductManager productManager, IProductsRepository productsRepository)
    {
        _productManager = productManager;
        _productsRepository = productsRepository;
    }

    public Task<ProductDto> CreateAsync(ProductCreateDto product)
    {
        throw new NotImplementedException();
    }
}