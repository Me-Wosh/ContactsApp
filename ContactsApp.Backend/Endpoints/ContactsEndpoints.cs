using ContactsApp.Backend.Models;
using ContactsApp.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Backend.Endpoints;

// /contacts endpoints for CRUD operations on Contacts
public static class ContactsEndpoints
{
    public static RouteGroupBuilder MapContactsEndpoints(this RouteGroupBuilder routes)
    {
        routes.MapGet("/", async (IContactsService contactsService) =>
        {
            return await contactsService.GetAllContacts();
        }).AllowAnonymous();

        routes.MapGet("/{id:int}", async (IContactsService contactsService, int id) =>
        {
            return await contactsService.GetContact(id);
        }).AllowAnonymous();
        
        routes.MapPost("/", async (IContactsService contactsService, [FromBody]CreateContactDto contactDto) =>
        {
            return await contactsService.CreateContact(contactDto);
        });
        
        routes.MapPatch("/", async (IContactsService contactsService, [FromBody]UpdateContactDto contactDto) =>
        {
            return await contactsService.UpdateContact(contactDto);
        });

        routes.MapDelete("/{id:int}", async (IContactsService contactsService, int id) =>
        {
            return await contactsService.DeleteContact(id);
        });

        routes.RequireAuthorization();

        return routes;
    }
}