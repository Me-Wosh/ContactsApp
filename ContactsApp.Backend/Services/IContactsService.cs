using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public interface IContactsService
{
    Task<IResult> GetAllContacts();
    Task<IResult> GetContact(int id);
}