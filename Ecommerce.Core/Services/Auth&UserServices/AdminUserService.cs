using Microsoft.Extensions.Logging;

public class AdminUserService : IAdminUserService
{
    private readonly IUserRepository _repo;
    private readonly ILogger<AdminUserService> _logger;
    public AdminUserService(IUserRepository repo, ILogger<AdminUserService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving all users.");
        var users = await _repo.GetAll(cancellationToken);
        var userDtos = users.Select(user => new UserDto
        {
            UserId = user.UserId,
            Email = user.Email,
            FullName = user.FullName
        }).ToList();
        _logger.LogInformation($"Retrieved {userDtos.Count} users.");
        return userDtos;
    }

    public async Task DisableUser(Guid userId, CancellationToken cancellationToken)
    {
       _logger.LogInformation($"Disabling user with ID: {userId}");
       var user = await _repo.GetById(userId, cancellationToken)
            ?? throw new KeyNotFoundException($"User with ID: {userId} not found.");
        user.IsActive = false;
        await _repo.Update(user, cancellationToken);
        _logger.LogInformation($"User with ID: {userId} disabled successfully.");
    }
}
