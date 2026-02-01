public interface IUserRoleService
{
    // Define methods for UserRole-related operations

    //Create a UserRole
    public Task CreateUserRole(UserRoleDto userRoleDto, CancellationToken cancellationToken);
    //Read List of UserRoles
    public Task<List<UserRoleDto>> GetUserRoles(int pageNumber, int pageSize, CancellationToken cancellationToken);
    //Disable a UserRole By UserRoleId
    public Task DisableUserRoleById(Guid userId, Guid roleId, CancellationToken cancellationToken);
}