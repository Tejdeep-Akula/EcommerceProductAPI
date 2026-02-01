public interface IProductRepository
{
    // Define methods for product-related operations
    //Create a product
    public Task CreateProduct(Product product, CancellationToken cancellationToken);
    //Update a product
    public Task UpdateProduct(ProductDto productDto, Guid productId, CancellationToken cancellationToken);
    //Read List of Products By CategoryId
    public Task<List<Product>> GetProductsByCategoryId(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    //Read Product By ProductId
    public Task<Product> GetProductById(Guid productId, CancellationToken cancellationToken);
    //Disable a product By ProductId
    public Task DisableProductById(Guid productId, CancellationToken cancellationToken);
}