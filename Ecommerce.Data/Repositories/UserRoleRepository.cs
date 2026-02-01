using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly EcommerceDbContext _dbContext;
    private readonly ILogger<UserRoleRepository> _logger;
    public UserRoleRepository(EcommerceDbContext dbContext, ILogger<UserRoleRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task CreateUserRole(UserRole userRole, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new user role to the database.");
        _dbContext.UserRoles.Add(userRole);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("User role added successfully.");

    }
    //Read List of UserRoles
    public async Task<List<UserRole>> GetUserRoles(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving user roles from the database.");
            var userRoles = await _dbContext.UserRoles.AsNoTracking().Where(u => u.IsActive)
                .OrderBy(ur => ur.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            _logger.LogInformation($"Retrieved {userRoles.Count} user roles.");
            
            return userRoles;
    }
    //Read UserRole By UserId
    public async Task<List<string>> GetUserRolesByUserId(Guid userId, CancellationToken cancellationToken)
{
    _logger.LogInformation("Retrieving active roles for active user with UserId: {UserId}",userId);

    var roles = await (
        from u in _dbContext.Users.AsNoTracking()
        join ur in _dbContext.UserRoles.AsNoTracking()
            on u.UserId equals ur.UserId
        join r in _dbContext.Roles.AsNoTracking()
            on ur.RoleId equals r.RoleId
        where u.UserId == userId
              && u.IsActive
              && ur.IsActive
              && r.IsActive
        select r.Name
    ).ToListAsync(cancellationToken);

    _logger.LogInformation("Retrieved {RoleCount} roles for UserId: {UserId}",roles.Count,userId);
    return roles;
}

    //Disable a product By ProductId
    public async Task DisableUserRoleById(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Disabling user role in the database with userId: {userId} and roleId: {roleId}.");
            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
            if(userRole == null)
            {
                _logger.LogWarning($"User role with userId: {userId} and roleId: {roleId} not found.");
                throw new KeyNotFoundException($"User role with userId: {userId} and roleId: {roleId} not found.");
            }
            userRole.IsActive = false;
            await  _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"User role with userId: {userId} and roleId: {roleId} has been disabled.");
            // Logic to disable a user role by user ID and role ID
    }
}