using ContactsApp.Backend.Services;

namespace ContactsApp.Backend.Endpoints;

public static class ContactsEndpoints
{
    public static void MapContactsEndpoints(this WebApplication app)
    {
        app.MapGet("/contacts", async (IContactsService contactsService) =>
        {
            return await contactsService.GetAllContacts();
        });

        app.MapGet("/contacts/{id:int}", async (IContactsService contactsService, int id) =>
        {
            return await contactsService.GetContact(id);
        });
    }
}