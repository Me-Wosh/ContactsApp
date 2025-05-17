using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.Services;

public interface IContactsService
{
    Task<IResult> GetAllContacts();
    Task<IResult> GetContact(int id);
    Task<IResult> CreateContact(CreateContactDto contactDto);
    Task<IResult> UpdateContact(UpdateContactDto contactDto);
    Task<IResult> DeleteContact(int id);
}