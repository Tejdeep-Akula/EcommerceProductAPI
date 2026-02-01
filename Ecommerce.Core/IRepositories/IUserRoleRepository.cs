public interface IUserRoleRepository
{
     // Define methods for User Role-related operations
    //Create a UserRole
    public Task CreateUserRole(UserRole userRole, CancellationToken cancellationToken);
    //Read List of UserRoles
    public Task<List<UserRole>> GetUserRoles(int pageNumber, int pageSize, CancellationToken cancellationToken);
   //Read UserRole By UserId
    public Task<List<String>> GetUserRolesByUserId(Guid userId, CancellationToken cancellationToken);
    //Disable a UserRole By UserId and RoleId
    public Task DisableUserRoleById(Guid userId, Guid roleId, CancellationToken cancellationToken);
}