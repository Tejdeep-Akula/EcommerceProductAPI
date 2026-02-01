using Microsoft.Extensions.Logging;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<RoleService> _logger; 
    public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }
    //Create a role
    public async Task CreateRole(RoleDto roleDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting role creation process.");
        if(roleDto == null)
        {
            throw new ArgumentNullException(nameof(roleDto));
        }
        Role role = new Role
        {
            RoleId = Guid.NewGuid(),
            Name = roleDto.Name,
            IsActive = roleDto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            UserCreatedId = roleDto.UserCreatedId
        };
        await _roleRepository.CreateRole(role, cancellationToken);
        _logger.LogInformation($"Role created successfully with ID: {role.RoleId}");
    }
    //Update a role
    public async Task UpdateRole(RoleDto roleDto, Guid roleId, CancellationToken cancellationToken)
    {
         _logger.LogInformation("Starting role update process.");
        if(roleDto == null)
        {
            throw new ArgumentNullException(nameof(roleDto));
        }

        await _roleRepository.UpdateRole(roleDto, roleId, cancellationToken);
            _logger.LogInformation($"Role updated successfully: {roleDto.Name}");
            // Logic to update a role
    }
    //Read List of Roles
    public async Task<List<RoleDto>> GetRoles(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting role retrieval process.");
        var roles = await _roleRepository.GetRoles(pageNumber, pageSize, cancellationToken);
        var roleDtos = roles.Select(role => new RoleDto
        {
            RoleId = role.RoleId,
            Name = role.Name,
            IsActive = role.IsActive,
            UserCreatedId = role.UserCreatedId
        }).ToList();
        _logger.LogInformation($"Retrieved {roleDtos.Count} roles.");
        return roleDtos;
    }

    //Read Role By RoleId
    public async Task<RoleDto> GetRoleById(Guid roleId, int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting role retrieval process.");
        if(roleId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(roleId));
        }
        var role = await _roleRepository.GetRoleById(roleId, pageSize, pageNumber, cancellationToken);
            if(role == null)
            {
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }
            var roleDto = new RoleDto
            {
                RoleId = role.RoleId,
                Name = role.Name,
                IsActive = role.IsActive,
                UserCreatedId = role.UserCreatedId
            };
            _logger.LogInformation($"Role retrieved successfully: {role.Name}");
            return roleDto;
    }
    //Disable a role By RoleId
    public async Task DisableRoleById(Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting role disable process.");
        if(roleId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(roleId));
        }
        await _roleRepository.DisableRoleById(roleId, cancellationToken);
        _logger.LogInformation("Role disabled successfully.");
        // Logic to disable a role by role ID
    }
}