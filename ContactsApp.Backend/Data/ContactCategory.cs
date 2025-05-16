namespace ContactsApp.Backend.Data;

// ContactCategory entity
public class ContactCategory
{
    // Primary Key
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Foreign Key (one-to-many)
    public List<Contact> Contacts { get; set; }
}