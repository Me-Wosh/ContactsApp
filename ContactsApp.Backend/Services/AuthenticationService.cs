using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ContactsApp.Backend.Data;
using ContactsApp.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ContactsApp.Backend.Services;

public partial class AuthenticationService : IAuthenticationService
{
    private readonly ContactsDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(
        ContactsDbContext dbContext,
        IPasswordHasher passwordHasher, 
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IResult> Login(LoginUserDto userDto)
    {
        User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email);

        if (user == null)
        {
            return Results.BadRequest("Wrong e-mail or password.");
        }

        if (!_passwordHasher.Verify(user.PasswordHash, userDto.Password))
        {
            return Results.BadRequest("Wrong e-mail or password.");
        }

        await CreateAndSaveRefreshToken(user);
        
        return Results.Ok(CreateJwt(user));
    }
    
    public async Task<IResult> Register(RegisterUserDto userDto)
    {
        string? email = _dbContext
            .Users
            .Select(u => u.Email)
            .SingleOrDefault(e => e == userDto.Email); 
        
        // if e-mail already exists
        if (email != null)
        {
            return Results.BadRequest($"Account with e-mail '{email}' already exists.");
        }

        userDto.Password = userDto.Password.Trim();

        if (!PasswordMeetsRequirements(userDto.Password))
        {
            return Results.BadRequest("Password didn't meet the requirements.");
        }

        var user = new User
        {
            Email = userDto.Email,
            PasswordHash = _passwordHasher.Hash(userDto.Password)
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return Results.Ok("Account successfully created.");
    }

    public async Task<IResult> RefreshTokens()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return Results.BadRequest("Couldn't process the request.");
        }

        // read refresh token and user id directly from http request
        string refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]!;
        var id = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpirationDate <= DateTime.UtcNow)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Invalid refresh token. Check the refresh token or login again."
            );
        }

        await CreateAndSaveRefreshToken(user);
        
        return Results.Ok(CreateJwt(user));
    }

    private string CreateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("Secret").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetSection("JWT").GetSection("Issuer").Value,
            audience: _configuration.GetSection("JWT").GetSection("Audience").Value,
            expires: DateTime.UtcNow.AddMinutes(15),
            claims: claims,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private async Task CreateAndSaveRefreshToken(User user)
    {
        byte[] randomBytes = RandomNumberGenerator.GetBytes(256 / 8);
        string refreshToken = Convert.ToHexString(randomBytes);
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(7);
        await _dbContext.SaveChangesAsync();

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = user.RefreshTokenExpirationDate
        };
        
        // add refresh token as httponly cookie
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    private bool PasswordMeetsRequirements(string password)
    {
        // allowed characters:
        // letters: a-z, A-Z
        // digits: 0-9
        // special characters: <<space>>!"#$%&'()*+,-./:;<=>?@[\]^_`{|}~
        
        // rules:
        // length: 12-255
        // at least one lowercase letter
        // at least one uppercase letter
        // at least one digit
        // at least one special character
        // characters only from the allowed characters list

        return PasswordRegex().IsMatch(password);
    }

    // generate regex at project compilation
    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[ !\"#$%&'()*+,\\-./:;<=>?@\\[\\\\\\]^_`{|}~])[a-zA-Z\\d !\"#$%&'()*+,\\-./:;<=>?@\\[\\\\\\]^_`{|}~]{12,255}$")]
    private static partial Regex PasswordRegex();
}