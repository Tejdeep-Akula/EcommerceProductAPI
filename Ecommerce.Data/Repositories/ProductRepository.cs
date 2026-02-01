using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ProductRepository : IProductRepository
{
    private readonly EcommerceDbContext _dbContext;
    private readonly ILogger<ProductRepository> _logger;
    public ProductRepository(EcommerceDbContext dbContext, ILogger<ProductRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task CreateProduct(Product product, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new product to the database.");
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Product added successfully.");

    }
    //Update a product
    public async Task UpdateProduct(ProductDto productDto, Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product in the database.");
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning($"Product with ID {productId} not found.");
            throw new KeyNotFoundException($"Product with ID {productId} not found.");
        }
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;
        product.IsActive = productDto.IsActive;
        product.StockQuantity = productDto.StockQuantity;
        product.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Product updated successfully.");
    }
    //Read List of Products By CategoryId
    public async Task<List<Product>> GetProductsByCategoryId(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving products by category from the database with categoryId: {categoryId}.");
            var products = await _dbContext.Products.AsNoTracking()
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .OrderBy(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            _logger.LogInformation($"Retrieved {products.Count} products for categoryId: {categoryId}.");
            
            return products;
            // Logic to get products by category ID
    }
    
    //Read Product By ProductId
    public async Task<Product> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving product by ID from the database with productId: {productId}.");
        var product = await _dbContext.Products.AsNoTracking()
            .Where(p => p.ProductId == productId && p.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
            _logger.LogInformation($"Product retrieved successfully: {product?.Name}."); 
        return product;  
    }
    //Disable a product By ProductId
    public async Task DisableProductById(Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Disabling product in the database with productId: {productId}.");
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId, cancellationToken);
            if(product == null)
            {
                _logger.LogWarning($"Product with ID {productId} not found.");
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            product.IsActive = false;
            await  _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Product with ID {productId} has been disabled.");
            // Logic to disable a product by product ID
    }
}