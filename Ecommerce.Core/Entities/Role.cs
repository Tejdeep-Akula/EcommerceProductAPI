public class Role
{
    public Guid RoleId { get; set; }

    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public Guid UserCreatedId { get; set; }
    public DateTime? UpdatedAt { get; set; }
}