public class User
{
    public Guid UserId { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }
    public string PasswordHash{ get; set;}
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}