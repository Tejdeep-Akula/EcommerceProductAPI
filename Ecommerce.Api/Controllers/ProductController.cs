using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    // POST: Create a new product
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to create a new product.");
        await _productService.CreateProduct(productDto, cancellationToken);
        _logger.LogInformation("Product creation request processed.");
        return Ok("Product created successfully.");
    }

    // PUT: Update a product
    [Authorize(Roles = "Admin")]
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto, Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to update a product.");
        await _productService.UpdateProduct(productDto, productId, cancellationToken);
        _logger.LogInformation("Product update request processed.");
        return Ok("Product updated successfully.");
    }

    // GET: Get products by category
    [Authorize(Roles = "Admin,User")]
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsByCategoryId(
        Guid categoryId,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to get products by category ID.");
        var productDtos = await _productService.GetProductsByCategoryId(categoryId, pageNumber, pageSize, cancellationToken);
        _logger.LogInformation("Product retrieval by category request processed.");
        return Ok(productDtos);
    }

    // GET: Get product by ID
    [Authorize(Roles = "Admin,User")]
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById(
        Guid productId,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to get a product by ID.");
        var productDto = await _productService.GetProductById(productId, cancellationToken);
        _logger.LogInformation("Product retrieval request processed.");
        return Ok(productDto);
    }

    // DELETE: Disable product
    [Authorize(Roles = "Admin")]
    [HttpDelete("disable/{productId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> DisableProductById(Guid productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to disable a product.");
        await _productService.DisableProductById(productId, cancellationToken);
        _logger.LogInformation("Product disable request processed.");
        return Ok("Product disabled successfully.");
    }
}