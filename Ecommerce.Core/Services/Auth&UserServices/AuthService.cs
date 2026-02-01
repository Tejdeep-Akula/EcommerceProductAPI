using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class AuthService
{
    private readonly ILogger<AuthService>_logger;
    private readonly IUserRepository _userRepo;
    private readonly IUserRoleRepository _roleRepo;
    private readonly JwtTokenGenerator _jwt;
    private readonly PasswordHasher<User> _hasher = new();

    public AuthService(IUserRepository u, IUserRoleRepository r, JwtTokenGenerator j, ILogger<AuthService> logger)
    {
        _userRepo = u;
        _roleRepo = r;
        _jwt = j;
        _logger = logger;
    }

    public async Task Register(RegisterDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user with email: {Email}", dto.Email);
        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _hasher.HashPassword(user, dto.Password);
        await _userRepo.Create(user, cancellationToken);
        _logger.LogInformation("User registered successfully with email: {Email}", dto.Email);
    }

    public async Task<AuthResponseDto> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting login for email: {Email}", dto.Email);
        var user = await _userRepo.GetByEmail(dto.Email, cancellationToken)
            ?? throw new UnauthorizedAccessException();

        var result = _hasher.VerifyHashedPassword(
            user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException();

        var roles = await _roleRepo.GetUserRolesByUserId(user.UserId, cancellationToken);
        var token = _jwt.Generate(user, roles);
        _logger.LogInformation("User logged in successfully with email: {Email}", dto.Email);

        return new AuthResponseDto { Token = token };
    }
}
