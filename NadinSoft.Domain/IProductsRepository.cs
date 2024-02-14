using System.Linq.Expressions;

namespace NadinSoft.Domain;

public interface IProductsRepository
{
    Task<Product> InsertAsync(Product product, bool autoSave = false);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetListAsync(Expression<Func<Product, bool>> filter);
    Task<Product> GetAsync(long id);
    Task<bool> AnyAsync(Expression<Func<Product, bool>> filter);
    Task<Product> UpdateAsync(Product product, bool autoSave = false);
    Task DeleteAsync(long id, bool autoSave = false);
    Task SaveAsync();
}