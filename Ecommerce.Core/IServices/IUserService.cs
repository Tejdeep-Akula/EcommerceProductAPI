public interface IUserService
{
    public Task<UserDto> GetMyProfile(Guid userId, CancellationToken cancellationToken);
    public Task UpdateMyProfile(Guid userId, UpdateUserDto dto, CancellationToken cancellationToken);
}