using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator
{
    private readonly IConfiguration _config;
    private readonly ILogger<JwtTokenGenerator> _logger;

    public JwtTokenGenerator(IConfiguration config, ILogger<JwtTokenGenerator> logger)
    {
        _config = config;
        _logger = logger;
    }

    public string Generate(User user, List<string> roles)
    {
        _logger.LogInformation("Generating JWT token for user ID: {UserId}", user.UserId);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        _logger.LogInformation("JWT token generated successfully for user ID: {UserId}", user.UserId);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
