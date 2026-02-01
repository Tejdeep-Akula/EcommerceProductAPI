public interface IUserRepository
{
    public Task<User> GetByEmail(string email,CancellationToken cancellationToken);
    public Task<User?> GetById(Guid id,CancellationToken cancellationToken);
    public Task<List<User>> GetAll(CancellationToken cancellationToken);
    public Task Create(User user,CancellationToken cancellationToken);
    public Task Update(User user,CancellationToken cancellationToken);
}
