using ContactsApp.Backend.Models;
using ContactsApp.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Backend.Endpoints;

// /contacts endpoint for CRUD operations on Contacts
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
        
        app.MapPost("/contacts", async (IContactsService contactsService, [FromBody]CreateContactDto contactDto) =>
        {
            return await contactsService.CreateContact(contactDto);
        });
        
        app.MapPatch("/contacts", async (IContactsService contactsService, [FromBody]UpdateContactDto contactDto) =>
        {
            return await contactsService.UpdateContact(contactDto);
        });

        app.MapDelete("/contacts/{id:int}", async (IContactsService contactsService, int id) =>
        {
            return await contactsService.DeleteContact(id);
        });
    }
}