using AutoMapper;
using ContactsApp.Backend.Data;
using ContactsApp.Backend.Models;

namespace ContactsApp.Backend.AutoMapper;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory != null ? src.SubCategory.Name : null ));
    }
}