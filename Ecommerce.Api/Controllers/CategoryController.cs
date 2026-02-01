using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    // POST: Create a new category
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new category.");
        await _categoryService.CreateCategory(categoryDto, cancellationToken);
        _logger.LogInformation("Category created successfully.");
        return Ok("Category created successfully.");
    }

    // PUT: Update a category
    [Authorize(Roles = "Admin")]
    [HttpPut("{categoryId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating category.");
        await _categoryService.UpdateCategory(categoryDto, categoryId, cancellationToken);
        _logger.LogInformation("Category updated successfully.");
        return Ok("Category updated successfully.");
    }

    // GET: Get all categories
    [Authorize(Roles = "Admin,User")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize
    )
    {
        _logger.LogInformation("Retrieving all categories.");
        var categoryDtos = await _categoryService.GetCategories(pageNumber, pageSize, cancellationToken);
        _logger.LogInformation("Categories retrieved successfully.");
        return Ok(categoryDtos);
    }

    // GET: Get category by ID
    [Authorize(Roles = "Admin,User")]
    [HttpGet("{categoryId}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById(
        Guid categoryId,
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize
    )
    {
        _logger.LogInformation("Retrieving category by ID.");
        var categoryDto = await _categoryService.GetCategoryById(categoryId, pageSize, pageNumber, cancellationToken);
        _logger.LogInformation("Category retrieved successfully.");
        return Ok(categoryDto);
    }

    // DELETE: Disable a category
    [Authorize(Roles = "Admin")]
    [HttpDelete("{categoryId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> DisableCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Disabling category.");
        await _categoryService.DisableCategoryById(categoryId, cancellationToken);
        _logger.LogInformation("Category disabled successfully.");
        return Ok("Category disabled successfully.");
    }
}