namespace NadinSoft.Domain;

public class ProductManager
{
    private readonly IProductsRepository _productsRepository;

    public ProductManager(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<Product> CreateProduct(string name, DateOnly produceDate, string manufacturePhone, string manufactureEmail, bool isAvailable)
    {
        bool alreadyExists = await _productsRepository.AnyAsync(x => x.Name == name || x.ManufactureEmail == manufactureEmail);

        if (alreadyExists)
        {
            throw new NadinSoftBusinessException("A product with either the same email or produce date already exists.");
        }

        return new Product(name: name, produceDate: produceDate, manufacturePhone: manufacturePhone,
            manufactureEmail: manufactureEmail, isAvailable: isAvailable);
    }
}