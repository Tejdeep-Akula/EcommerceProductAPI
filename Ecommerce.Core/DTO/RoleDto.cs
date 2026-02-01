public class RoleDto
{
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Guid UserCreatedId { get; set; }
}