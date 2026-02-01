using Microsoft.Extensions.Logging;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger; 
    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    //Create a product
    public async Task CreateProduct(ProductDto productDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting product creation process.");
        if(productDto == null)
        {
            throw new ArgumentNullException(nameof(productDto));
        }
        if(productDto.Price < 0)
            {
                throw new ArgumentException("Product price cannot be negative");
            }
            if(productDto.StockQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative");
            }
            Product product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                IsActive = productDto.IsActive,
                StockQuantity = productDto.StockQuantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                UserCreatedId = productDto.UserCreatedId
            };
            await _productRepository.CreateProduct(product, cancellationToken);
            _logger.LogInformation($"Product created successfully with ID: {product.ProductId}");
    }
    //Update a product
    public async Task UpdateProduct(ProductDto productDto, Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting product update process.");
        if(productDto == null)
        {
            throw new ArgumentNullException(nameof(productDto));
        }
        if(productDto.Price < 0)
            {
                throw new ArgumentException("Product price cannot be negative");
            }
            if(productDto.StockQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative");
            }
            await _productRepository.UpdateProduct(productDto, productId, cancellationToken);
            _logger.LogInformation($"Product updated successfully: {productDto.Name}");
            // Logic to update a product
    }
    //Read List of Products By CategoryId
    public async Task<List<ProductDto>> GetProductsByCategoryId(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting product retrieval by category process.");
        if(categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }
        _logger.LogInformation($"Retrieving products for Category ID: {categoryId}");
        var products = await _productRepository.GetProductsByCategoryId(categoryId, pageNumber, pageSize, cancellationToken);
            if(products == null || !products.Any())
            {
                _logger.LogWarning($"No products found for Category ID: {categoryId}");
                throw new KeyNotFoundException($"No products found for Category ID: {categoryId}");
            }
            var productDtos = products.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive,
                StockQuantity = product.StockQuantity,
                UserCreatedId = product.UserCreatedId // Not mapping UserCreatedId here
            }).ToList();
            _logger.LogInformation($"Retrieved {productDtos.Count} products for Category ID: {categoryId}");
            return productDtos;
            // Logic to get products by category ID
    }
    
    //Read Product By ProductId
    public async Task<ProductDto> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting product retrieval process.");
        if(productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }
        var product = await _productRepository.GetProductById(productId, cancellationToken);
            if(product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive,
                StockQuantity = product.StockQuantity,
                UserCreatedId = product.UserCreatedId // Not mapping UserCreatedId here
            };
            _logger.LogInformation($"Product retrieved successfully: {product.Name}");
            return productDto;
            // Logic to get product by product ID
    }
    //Disable a product By ProductId
    public async Task DisableProductById(Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting product disable process.");
        if(productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }
        await _productRepository.DisableProductById(productId, cancellationToken);
        _logger.LogInformation("Product disabled successfully.");
        // Logic to disable a product by product ID
    }
}