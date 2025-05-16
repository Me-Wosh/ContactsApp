using AutoMapper;
using ContactsApp.Backend.Data;
using ContactsApp.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Backend.Services;

public class ContactsService : IContactsService
{
    private readonly ContactsDbContext _dbContext;
    private readonly IMapper _mapper;

    public ContactsService(ContactsDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IResult> GetAllContacts()
    {
        List<Contact> contacts = await _dbContext
            .Contacts
            .Include(c => c.Category)
            .Include(c => c.SubCategory)
            .ToListAsync();
        
        var contactDtos = _mapper.Map<List<Contact>, List<ContactDto>>(contacts);
        
        return Results.Ok(contactDtos);
    }
    
    public async Task<IResult> GetContact(int id)
    {
        Contact? contact = await _dbContext
            .Contacts
            .Include(c => c.Category)
            .Include(c => c.SubCategory)
            .SingleOrDefaultAsync(c => c.Id == id);
        
        if (contact == null)
        {
            return Results.BadRequest($"Contact with given id {id} does not exist");
        }
        
        var contactDto = _mapper.Map<Contact, ContactDto>(contact);
        
        return Results.Ok(contactDto);
    }
}