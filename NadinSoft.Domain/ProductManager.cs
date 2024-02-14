namespace NadinSoft.Domain;

public class ProductManager
{
    private readonly IProductsRepository _productsRepository;

    public ProductManager(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<Product> CreateProduct(string createdBy, string name, DateOnly produceDate, string manufacturePhone, string manufactureEmail, bool isAvailable)
    {
        bool alreadyExists = await _productsRepository.AnyAsync(x => x.ProduceDate == produceDate || x.ManufactureEmail == manufactureEmail);

        if (alreadyExists)
        {
            throw new NadinSoftBusinessException("A product with either the same email or produce date already exists.");
        }

        return new Product(createdBy: createdBy, name: name, produceDate: produceDate, manufacturePhone: manufacturePhone,
            manufactureEmail: manufactureEmail, isAvailable: isAvailable);
    }
}