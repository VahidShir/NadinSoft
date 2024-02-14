namespace NadinSoft.Application.Contracts;

public interface IProductService
{
    Task<ProductDto> CreateAsync(Guid createdBy, ProductCreateDto product);
    Task<ProductDto> GetAsync(long id);
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task DeleteAsync(long id);
}