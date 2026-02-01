using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _Logger;
    private readonly IUserService _userService;
    private readonly IAdminUserService _adminService;

    public UsersController(IUserService u, IAdminUserService a, ILogger<UsersController> logger)
    {
        _userService = u;
        _adminService = a;
        _Logger = logger;
    }

    [HttpGet("me")]
     [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        _Logger.LogInformation("Fetching profile for the authenticated user.");
        var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var profile = await _userService.GetMyProfile(id, cancellationToken);
        _Logger.LogInformation("Profile fetched successfully for the authenticated user.");
        return Ok(profile);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        foreach (var claim in User.Claims)
        {
            _Logger.LogInformation("Claim: {Type} = {Value}", claim.Type, claim.Value);
        }

        _Logger.LogInformation("Fetching all users.");
        var users = await _adminService.GetAllUsers(cancellationToken);
        _Logger.LogInformation("All users fetched successfully.");
        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}/disable")]
    public async Task<IActionResult> Disable(Guid id, CancellationToken cancellationToken)
    {
        _Logger.LogInformation("Disabling user with ID: {UserId}", id);
        await _adminService.DisableUser(id, cancellationToken);
        _Logger.LogInformation("User with ID: {UserId} disabled successfully", id);
        return NoContent();
    }
}
