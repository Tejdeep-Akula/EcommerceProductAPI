using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Logging;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;
    public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }
    //Create a category
    public async Task CreateCategory(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting category creation process.");
        if(categoryDto == null)
        {
            throw new ArgumentNullException(nameof(categoryDto));
        }
        Category category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = categoryDto.Name,
            Description = categoryDto.Description,
            IsActive = categoryDto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UserCreatedId = categoryDto.UserCreatedId,
            UpdatedAt = null
        };
        await _categoryRepository.CreateCategory(category, cancellationToken);
        _logger.LogInformation($"Category created successfully with ID: {category.CategoryId}");
    }
    //Update a category
    public async Task UpdateCategory(CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken)
    {
         _logger.LogInformation("Starting category update process.");
        if(categoryDto == null)
        {
            throw new ArgumentNullException(nameof(categoryDto));
        }
        await _categoryRepository.UpdateCategory(categoryDto, categoryId, cancellationToken);
        _logger.LogInformation($"Category updated successfully: {categoryDto.Name}");
        // Logic to update a category
    }
    //Read List of Categories
    public async Task<List<CategoryDto>> GetCategories(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting category retrieval process.");
        var categories = await _categoryRepository.GetCategories(pageNumber, pageSize, cancellationToken);
            if(categories == null || !categories.Any())
            {
                _logger.LogWarning("No categories found.");
                throw new KeyNotFoundException("No categories found.");
            }
            var categoryDtos = categories.Select(category => new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            }).ToList();
            _logger.LogInformation($"Retrieved {categoryDtos.Count} categories.");
            return categoryDtos;
    }
    public async Task<CategoryDto> GetCategoryById(Guid categoryId, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting category retrieval by ID process.");
        if(categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }
        var category = await _categoryRepository.GetCategoryById(categoryId, pageSize, pageNumber, cancellationToken);
            if(category == null)
            {
                _logger.LogWarning($"Category with ID {categoryId} not found.");
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            var categoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
            _logger.LogInformation($"Retrieved category with ID {categoryId}.");
            return categoryDto;
    }

    //Disable a category By CategoryId
    public async Task DisableCategoryById(Guid categoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting category disable process.");
        if(categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }
        await _categoryRepository.DisableCategoryById(categoryId, cancellationToken);
        _logger.LogInformation("Category disabled successfully.");
        // Logic to disable a category by category ID
    }

}