public interface ICategoryRepository
{
    // Repository methods for Category entity
    public Task CreateCategory(Category category, CancellationToken cancellationToken);
    //Update a category
    public Task UpdateCategory(CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken);
    //Read List of Categories
    public Task<List<Category>> GetCategories(int pageNumber, int pageSize, CancellationToken cancellationToken);
    //Read category by CategoryId
    public Task<Category> GetCategoryById(Guid CategorId, int pageSize, int pageNumber, CancellationToken cancellationToken);
    //Disable a category By CategoryId
    public Task DisableCategoryById(Guid categoryId, CancellationToken cancellationToken);
}