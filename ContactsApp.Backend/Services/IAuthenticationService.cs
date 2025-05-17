using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public interface IAuthenticationService
{
    Task<IResult> Login(UserDto userDto);
    Task<IResult> Register(UserDto userDto);
    Task<IResult> RefreshTokens();
}