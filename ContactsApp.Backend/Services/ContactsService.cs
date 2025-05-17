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
            return Results.BadRequest($"Contact with given id '{id}' does not exist");
        }
        
        var contactDto = _mapper.Map<Contact, ContactDto>(contact);
        
        return Results.Ok(contactDto);
    }

    public async Task<IResult> CreateContact(CreateContactDto contactDto)
    {
        string? email = await _dbContext
            .Contacts
            .Select(c => c.Email)
            .SingleOrDefaultAsync(e => e == contactDto.Email);

        // e-mail is not unique 
        if (email != null)
        {
            return Results.BadRequest($"Contact with given e-mail '{email}' already exists");
        }
        
        // check if subcategory exists, otherwise create one
        int? subCategoryId = await GetContactSubCategoryId(contactDto.SubCategoryName);
        
        Contact contact = new Contact
        {
            FirstName = contactDto.FirstName,
            LastName = contactDto.LastName,
            Email = contactDto.Email,
            PhoneNumber = contactDto.PhoneNumber,
            DateOfBirth = contactDto.DateOfBirth,
            CategoryId = contactDto.CategoryId,
            SubCategoryId = subCategoryId
        };

        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync();

        // return path to the new contact
        return Results.Created($"contacts/{contact.Id}", "Contact created successfully.");
    }

    public async Task<IResult> UpdateContact(UpdateContactDto contactDto)
    {
        Contact? contact = await _dbContext
            .Contacts
            .SingleOrDefaultAsync(c => c.Id == contactDto.Id);

        if (contact == null)
        {
            return Results.BadRequest($"Contact with given id '{contactDto.Id}' does not exist.");
        }

        if (contactDto.Email != contact.Email)
        {
            string? email = await _dbContext
                .Contacts
                .Select(c => c.Email)
                .SingleOrDefaultAsync(e => e == contactDto.Email);

            // e-mail is not unique 
            if (email != null)
            {
                return Results.BadRequest($"Contact with given e-mail '{email}' already exists.");
            }    
        }
        
        // check if subcategory exists, otherwise create one
        int? subCategoryId = await GetContactSubCategoryId(contactDto.SubCategoryName);

        contact.FirstName = contactDto.FirstName;
        contact.LastName = contactDto.LastName;
        contact.Email = contactDto.Email;
        contact.PhoneNumber = contactDto.PhoneNumber;
        contact.DateOfBirth = contactDto.DateOfBirth;
        contact.CategoryId = contactDto.CategoryId;
        contact.SubCategoryId = subCategoryId;
        
        await _dbContext.SaveChangesAsync();

        return Results.Ok("Contact updated successfully.");
    }

    public async Task<IResult> DeleteContact(int id)
    {
        Contact? contact = await _dbContext.Contacts.SingleOrDefaultAsync(c => c.Id == id);

        if (contact == null)
        {
            return Results.BadRequest($"Contact with given id '{id}' does not exist.");
        }
        
        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync();

        return Results.Ok("Contact was successfully deleted.");
    }

    private async Task<int?> GetContactSubCategoryId(string? name)
    {
        if (name == null)
        {
            return null;
        }
        
        ContactSubCategory? subCategory = await _dbContext
                .ContactSubCategories
                .SingleOrDefaultAsync(s => s.Name == name);
        
        // subcategory exists
        if (subCategory != null)
        {
            return subCategory.Id;
        }

        // subcategory doesn't exist - create one
        var dbSubCategory = new ContactSubCategory
        {
            Name = name
        };
        
        await _dbContext.ContactSubCategories.AddAsync(dbSubCategory);
        await _dbContext.SaveChangesAsync();
        
        return dbSubCategory.Id;
    }
}