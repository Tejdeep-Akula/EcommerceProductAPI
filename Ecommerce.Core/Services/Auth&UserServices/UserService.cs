using Microsoft.Extensions.Logging;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo, ILogger<UserService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<UserDto> GetMyProfile(Guid userId , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching profile for user ID: {UserId}", userId);
        var user = await _repo.GetById(userId, cancellationToken)
            ?? throw new KeyNotFoundException();

        var userDto = new UserDto
        {
            UserId = user.UserId,
            Email = user.Email,
            FullName = user.FullName
        };
        _logger.LogInformation("Profile fetched successfully for user ID: {UserId}", userId);
        return userDto;
    }

    public async Task UpdateMyProfile(Guid userId, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating profile for user ID: {UserId}", userId);
        var user = await _repo.GetById(userId, cancellationToken)
            ?? throw new KeyNotFoundException();

        user.FullName = dto.FullName;
        await _repo.Update(user, cancellationToken);
        _logger.LogInformation("Profile updated successfully for user ID: {UserId}", userId);
    }
}
