namespace NadinSoft.Application.Contracts;

public interface IProductService
{
    Task<ProductDto> CreateAsync(ProductCreateDto product);
}