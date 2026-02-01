using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IRoleService roleService, ILogger<RoleController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    // POST: Create a new role
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new role.");
        await _roleService.CreateRole(roleDto, cancellationToken);
        _logger.LogInformation("Role created successfully.");
        return Ok("Role created successfully.");
    }

    // PUT: Update a role
    [HttpPut("{roleId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRole([FromBody] RoleDto roleDto, Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating role.");
        await _roleService.UpdateRole(roleDto, roleId, cancellationToken);
        _logger.LogInformation("Role updated successfully.");
        return Ok("Role updated successfully.");
    }

    // GET: Get all roles
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoles(
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize
    )
    {
        _logger.LogInformation("Retrieving all roles.");
        var roleDtos = await _roleService.GetRoles(pageNumber, pageSize, cancellationToken);
        _logger.LogInformation("Roles retrieved successfully.");
        return Ok(roleDtos);
    }

    // GET: Get role by ID
    [HttpGet("{roleId}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleById(
        Guid roleId,
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize
    )
    {
        _logger.LogInformation("Retrieving role by ID.");
        var roleDto = await _roleService.GetRoleById(roleId, pageSize, pageNumber, cancellationToken);
        _logger.LogInformation("Role retrieved successfully.");
        return Ok(roleDto);
    }

    // DELETE: Disable a role
    [HttpDelete("{roleId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> DisableRole(Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Disabling role.");
        await _roleService.DisableRoleById(roleId, cancellationToken);
        _logger.LogInformation("Role disabled successfully.");
        return Ok("Role disabled successfully.");
    }
}
