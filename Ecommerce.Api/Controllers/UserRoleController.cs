using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;
    private readonly ILogger<UserRoleController> _logger;

    public UserRoleController(IUserRoleService userRoleService, ILogger<UserRoleController> logger)
    {
        _userRoleService = userRoleService;
        _logger = logger;
    }

    // POST: Create a new user role
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUserRoles([FromBody] UserRoleDto userRoleDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new user role.");
        await _userRoleService.CreateUserRole(userRoleDto, cancellationToken);
        _logger.LogInformation("User role created successfully.");
        return Ok("User role created successfully.");
    }

    // GET: Get all user roles
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserRoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserRoles(
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize
    )
    {
        _logger.LogInformation("Retrieving all user roles.");
        var userRoleDtos = await _userRoleService.GetUserRoles(pageNumber, pageSize, cancellationToken);
        _logger.LogInformation("User roles retrieved successfully.");
        return Ok(userRoleDtos);
    }

    // DELETE: Disable a user role
    [HttpDelete("{userId}/{roleId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> DisableUserRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Disabling user role.");
        await _userRoleService.DisableUserRoleById(userId, roleId, cancellationToken);
        _logger.LogInformation("User role disabled successfully.");
        return Ok("User role disabled successfully.");
    }
}
