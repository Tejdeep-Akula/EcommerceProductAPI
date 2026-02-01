public interface IAdminUserService
{
    public Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    public Task DisableUser(Guid userId, CancellationToken cancellationToken);
}
