using ContactsApp.Shared;

namespace ContactsApp.Backend.Services;

public interface IAuthenticationService
{
    Task<IResult> Login(LoginUserDto userDto);
    Task<IResult> Register(RegisterUserDto userDto);
    Task<IResult> RefreshTokens();
}