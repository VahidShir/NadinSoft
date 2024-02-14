using AutoMapper;

using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

namespace NadinSoft.Application;

public class ProductService : IProductService
{
    private readonly ProductManager _productManager;
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public ProductService(ProductManager productManager, IProductsRepository productsRepository, IMapper mapper)
    {
        _productManager = productManager;
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateAsync(Guid createdBy, ProductCreateDto input)
    {
        Product product = await _productManager.CreateProduct(
            createdBy: createdBy,
            name: input.Name,
            produceDate: input.ProduceDate,
            manufacturePhone: input.ManufacturePhone,
            manufactureEmail: input.ManufactureEmail,
            isAvailable: input.IsAvailable);

        await _productsRepository.InsertAsync(product, autoSave: true);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task DeleteAsync(long id)
    {
        bool exists = await _productsRepository.AnyAsync(x => x.Id == id);

        if (!exists)
        {
            throw new NadinSoftBusinessException("THe product you want to delete doesn't exists.");
        }

        await _productsRepository.DeleteAsync(id, autoSave: true);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productsRepository.GetAllAsync();

        if (products?.Count() > 0)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        return Enumerable.Empty<ProductDto>();
    }

    public async Task<ProductDto> GetAsync(long id)
    {
        var product = await _productsRepository.GetAsync(id);

        if (product is not null)
        {
            return _mapper.Map<ProductDto>(product);
        }

        return null;
    }
}