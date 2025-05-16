using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public interface IRegistrationService
{
    Task Register(UserDto userDto);
}