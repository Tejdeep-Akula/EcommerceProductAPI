public interface IRoleRepository
{
    // Define methods for Role-related operations
    //Create a role
    public Task CreateRole(Role role, CancellationToken cancellationToken);
    //Update a role
    public Task UpdateRole(RoleDto roleDto, Guid roleId, CancellationToken cancellationToken);
    //Read List of Roles
    public Task<List<Role>> GetRoles(int pageNumber, int pageSize, CancellationToken cancellationToken);
    //Read Role By RoleId
    public Task<Role> GetRoleById(Guid roleId, int pageSize, int pageNumber, CancellationToken cancellationToken);
    //Disable a role By RoleId
    public Task DisableRoleById(Guid roleId, CancellationToken cancellationToken);
}