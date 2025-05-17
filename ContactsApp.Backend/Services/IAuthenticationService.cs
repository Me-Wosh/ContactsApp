using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public interface IAuthenticationService
{
    Task<IResult> Login(UserDto userDto);
}