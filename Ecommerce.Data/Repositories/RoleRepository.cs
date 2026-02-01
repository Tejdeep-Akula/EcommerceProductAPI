using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class RoleRepository : IRoleRepository
{
    private readonly EcommerceDbContext _dbContext;
    private readonly ILogger<RoleRepository> _logger;
    public RoleRepository(EcommerceDbContext dbContext, ILogger<RoleRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task CreateRole(Role role, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new role to the database.");
        _dbContext.Roles.Add(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Role added successfully.");

    }
    //Update a role
    public async Task UpdateRole(RoleDto roleDto, Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating role in the database.");
        var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId, cancellationToken);
        if (role == null)
        {
            _logger.LogWarning($"Role with ID {roleId} not found.");
            throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        }
        role.Name = roleDto.Name;
        role.IsActive = roleDto.IsActive;
        role.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Role updated successfully.");
    }
    //Read List of Roles
    public async Task<List<Role>> GetRoles(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving roles from the database.");
            var roles = await _dbContext.Roles.AsNoTracking()
                .Where(r => r.IsActive)
                .OrderBy(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        _logger.LogInformation($"Retrieved {roles.Count} roles.");
        return roles;
    }

    //Read Role By RoleId
    public async Task<Role> GetRoleById(Guid roleId, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving role by ID from the database with roleId: {roleId}.");
        var role = await _dbContext.Roles.AsNoTracking()
            .Where(r => r.RoleId == roleId && r.IsActive)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync(cancellationToken);
            _logger.LogInformation($"Role retrieved successfully: {role?.Name}."); 
        return role;  
    }
    //Disable a Role By RoleId
    public async Task DisableRoleById(Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Disabling role in the database with roleId: {roleId}.");
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId, cancellationToken);
            if(role == null)
            {
                _logger.LogWarning($"Role with ID {roleId} not found.");
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }
            role.IsActive = false;
            await  _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Role with ID {roleId} has been disabled.");
            // Logic to disable a role by role ID
    }
}