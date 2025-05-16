using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public interface IAuthenticationService
{
    Task Login(UserDto userDto);
}