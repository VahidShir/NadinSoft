using Microsoft.EntityFrameworkCore;

using NadinSoft.Domain;

using System.Linq.Expressions;

namespace NadinSoft.EntityFrameworkCore.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly NadinSoftDbContext _dbContext;

    public ProductsRepository(NadinSoftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteAsync(long id, bool autoSave = false)
    {
        var product = await GetAsync(id);

        if (product is null)
            return;

        _dbContext.Products.Remove(product);

        if (autoSave)
        {
            await SaveAsync();
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product> GetAsync(long id)
    {
        return await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetListAsync(Expression<Func<Product, bool>> filter)
    {
        return await _dbContext.Products.Where(filter).ToListAsync();
    }

    public async Task<Product> InsertAsync(Product product, bool autoSave = false)
    {
        bool esists = await _dbContext.Products.AnyAsync(x => x.Id == product.Id);

        if (esists)
            return product;

        var insertedProduct = await _dbContext.Products.AddAsync(product);

        if (autoSave)
        {
            await SaveAsync();
        }

        return insertedProduct.Entity;
    }

    public async Task<Product> UpdateAsync(Product product, bool autoSave = false)
    {
        var productDb = await GetAsync(product.Id);

        if (productDb is null)
            return product;

        var updatedProduct = _dbContext.Products.Update(product);

        return updatedProduct.Entity;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
