using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UserRepository : IUserRepository
{
    private readonly EcommerceDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(EcommerceDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving user by email: {email} from the database.");
        var user =  _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);
        _logger.LogInformation($"User with email: {email} retrieved successfully.");
        return user;
        
    }

    public Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Retrieving user by ID: {id} from the database.");
        var user =  _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsActive, cancellationToken);
        _logger.LogInformation($"User with ID: {id} retrieved successfully.");
        return user;
    }

    public Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving all users from the database.");
        var users =  _dbContext.Users.Where(u => u.IsActive).ToListAsync(cancellationToken);
        _logger.LogInformation($"Retrieved {users.Result.Count} users.");
        return users;
    }

    public async Task Create(User user, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new user to the database.");
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("User added successfully.");
    }

    public async Task Update(User user, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating user with ID: {user.UserId} in the database.");
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"User with ID: {user.UserId} updated successfully.");
    }
}
