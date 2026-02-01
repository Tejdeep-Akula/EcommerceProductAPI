using Microsoft.Extensions.Logging;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ILogger<UserRoleService> _logger;
    public UserRoleService(IUserRoleRepository userRoleRepository, ILogger<UserRoleService> logger)
    {
        _userRoleRepository = userRoleRepository;
        _logger = logger;
    }
    //Create a user role
    public async Task CreateUserRole(UserRoleDto userRoleDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting user role creation process.");
        if(userRoleDto == null)
        {
            throw new ArgumentNullException(nameof(userRoleDto));
        }
        UserRole userRole = new UserRole
        {
            UserId = userRoleDto.UserId,
            RoleId = userRoleDto.RoleId,
            IsActive = userRoleDto.IsActive,
            UserCreatedId = userRoleDto.UserCreatedId
        };
        await _userRoleRepository.CreateUserRole(userRole, cancellationToken);
        _logger.LogInformation($"User role created successfully with ID: {userRole.UserId}");
    }
    //Read List of UserRoles
    public async Task<List<UserRoleDto>> GetUserRoles(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting user role retrieval process.");
        var userRoles = await _userRoleRepository.GetUserRoles(pageNumber, pageSize, cancellationToken);
            if(userRoles == null || !userRoles.Any())
            {
                _logger.LogWarning("No user roles found.");
                throw new KeyNotFoundException("No user roles found.");
            }
            var userRoleDtos = userRoles.Select(userRole => new UserRoleDto
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                IsActive = userRole.IsActive,
                UserCreatedId = userRole.UserCreatedId
            }).ToList();
            _logger.LogInformation($"Retrieved {userRoleDtos.Count} user roles.");
            return userRoleDtos;
    }
    //Disable a UserRole By UserId and RoleId
    public async Task DisableUserRoleById(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting user role disable process.");
        if(userId == Guid.Empty || roleId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(userId) + " or " + nameof(roleId));
        }
        await _userRoleRepository.DisableUserRoleById(userId, roleId, cancellationToken);
        _logger.LogInformation("User role disabled successfully.");
        // Logic to disable a user role by user ID and role ID
    }

}