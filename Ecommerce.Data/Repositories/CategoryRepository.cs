using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CategoryRepository : ICategoryRepository
{
    private readonly EcommerceDbContext _dbContext;
    private readonly ILogger<CategoryRepository> _logger;
    public CategoryRepository(EcommerceDbContext dbContext, ILogger<CategoryRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task CreateCategory(Category category, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new category to the database.");
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Category added successfully.");

    }
    //Update a category
    public async Task UpdateCategory(CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating category in the database.");
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);
        if (category == null)
        {
            _logger.LogWarning($"Category with ID {categoryId} not found.");
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }
        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        category.IsActive = categoryDto.IsActive;
        category.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Category updated successfully.");
    }
    //Read List of Categories
    public async Task<List<Category>> GetCategories(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving categories from the database.");
            var categories = await _dbContext.Categories.AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            _logger.LogInformation($"Retrieved {categories.Count} categories.");
            return categories;
            // Logic to get categories
    }
    //Read category by CategoryId
    public async Task<Category> GetCategoryById(Guid categoryId, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving category with ID {categoryId} from the database.");
        var categories = await _dbContext.Categories.AsNoTracking()
                .Where(c => c.CategoryId == categoryId && c.IsActive)
                .OrderBy(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .FirstOrDefaultAsync(cancellationToken);
            _logger.LogInformation($"Retrieved category with ID {categoryId}.");
            return categories;
    }
    //Disable a Category By CategoryId
    public async Task DisableCategoryById(Guid categoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Disabling category in the database with categoryId: {categoryId}.");
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);
            if(category == null)
            {
                _logger.LogWarning($"Category with ID {categoryId} not found.");
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            category.IsActive = false;
            await  _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Category with ID {categoryId} has been disabled.");
            // Logic to disable a category by category ID
    }
}