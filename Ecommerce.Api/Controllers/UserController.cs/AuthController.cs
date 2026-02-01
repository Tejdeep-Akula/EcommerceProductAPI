using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly AuthService _service;

    public AuthController(AuthService service, ILogger<AuthController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // POST: Register a new user
    [HttpPost("register")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering a new user.");
        await _service.Register(dto, cancellationToken);
        _logger.LogInformation("User registered successfully.");
        return Ok("User registered successfully.");
    }

    // POST: Login
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User login attempt.");
        var token = await _service.Login(dto, cancellationToken);
        _logger.LogInformation("User logged in successfully.");
        return Ok(token);
    }
}