using System.Security.Cryptography;
using ContactsApp.Backend.Data;
using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ContactsDbContext _dbContext;

    public AuthenticationService(ContactsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Login(UserDto userDto)
    {
        
    }
}