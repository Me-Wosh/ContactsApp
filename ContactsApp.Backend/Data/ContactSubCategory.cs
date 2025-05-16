namespace ContactsApp.Backend.Data;

// ContactSubCategory entity
public class ContactSubCategory
{
    // Primary Key
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Foreign Key (one-to-many) (optional)
    public List<Contact> Contacts { get; set; }
}