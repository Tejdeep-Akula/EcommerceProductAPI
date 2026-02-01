public interface IRoleService
{
    // Define methods for Role-related operations

    //Create a role
    public Task CreateRole(RoleDto roleDto, CancellationToken cancellationToken);
    //Update a role
    public Task UpdateRole(RoleDto roleDto, Guid roleId, CancellationToken cancellationToken);
    //Read List of Roles
    public Task<List<RoleDto>> GetRoles(int pageNumber, int pageSize, CancellationToken cancellationToken);
    //Read Role By RoleId
    public Task<RoleDto> GetRoleById(Guid roleId, int pageSize, int pageNumber, CancellationToken cancellationToken);
    //Disable a role By RoleId
    public Task DisableRoleById(Guid roleId, CancellationToken cancellationToken);
}