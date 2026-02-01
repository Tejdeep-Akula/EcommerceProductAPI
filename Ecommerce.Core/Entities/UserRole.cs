public class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserCreatedId { get; set; }
}