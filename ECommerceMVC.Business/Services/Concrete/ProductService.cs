using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<List<ProductResponseModel>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProduct();
        return products.Select(p => new ProductResponseModel
        {
            ProductID = p.ProductID,
            ProductName = p.ProductName,
            CategoryName = p.CategoryName,
            UnitPrice = p.UnitPrice
        }).ToList();
    }
    public async Task<List<ProductResponseModel>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
        return products.Select(p => new ProductResponseModel
        {
            ProductID = p.ProductID,
            ProductName = p.ProductName,
            CategoryName = p.CategoryName,
            UnitPrice = p.UnitPrice
        }).ToList();
    }
    public async Task<List<ProductResponseModel>> GetProductsByCategoryNameAsync(string categoryName)
    {
        var products = await _productRepository.GetProductsByCategoryNameAsync(categoryName);
        return products.Select(p => new ProductResponseModel
        {
            ProductID = p.ProductID,
            ProductName = p.ProductName,
            CategoryName = p.CategoryName,
            UnitPrice = p.UnitPrice
        }).ToList();
    }
    public async Task<List<ProductResponseModel>> GetTop5BestSellingProductsAsync()
    {
        var products = await _productRepository.GetTop5BestSellingProductsAsync();
        return products.Select(p => new ProductResponseModel
        {
            ProductID = p.ProductID,
            ProductName = p.ProductName,
            CategoryName = p.CategoryName,
            UnitPrice = p.UnitPrice
        }).ToList();
    }
    public bool BulkInsertProducts(List<Product> products)
        {
        return _productRepository.BulkInsertProducts(products);
    }
}
