using ContactsApp.Backend.Data;
using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public class RegistrationService : IRegistrationService
{
    private readonly ContactsDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public RegistrationService(ContactsDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }
    
    public async Task Register(UserDto userDto)
    {
        string? email = _dbContext
            .Users
            .Select(u => u.Email)
            .SingleOrDefault(e => e == userDto.Email); 
        
        // if e-mail already exists
        if (email != null)
        {
            // exception
        }

        userDto.Password = userDto.Password.Trim();

        if (!PasswordMeetsRequirements(userDto.Password))
        {
            // exception
        }

        var user = new User
        {
            Email = userDto.Email, // validation occurs at DTO level
            PasswordHash = _passwordHasher.Hash(userDto.Password)
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    private bool PasswordMeetsRequirements(string password)
    {
        if (password.Length < 12)
        {
            return false;
        }

        char[] lowercase = "abcdefghijklmnopqrstuvwxyz".ToArray();
        char[] uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        char[] special = "".ToArray();
        
        if (1==1)
        {
            return false;
        }
        
        return true;
    }
}