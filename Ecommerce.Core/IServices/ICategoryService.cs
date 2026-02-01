public interface ICategoryService
{
    //Create a category
    public Task CreateCategory(CategoryDto categoryDto, CancellationToken cancellationToken);
    //Update a category
    public Task UpdateCategory(CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken);
    //Read List of Categories
    public Task<List<CategoryDto>> GetCategories(int pageNumber, int pageSize, CancellationToken cancellationToken);
    //Read Category By CategoryId
    public Task<CategoryDto> GetCategoryById(Guid CategorId, int pageSize, int pageNumber, CancellationToken cancellationToken);
    //Disable a category By CategoryId
    public Task DisableCategoryById(Guid categoryId, CancellationToken cancellationToken);
}