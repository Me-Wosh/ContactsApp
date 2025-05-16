using ContactsApp.Backend.Data;
using ContactsApp.Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Backend.Configuration;

public static class AppConfiguration
{
    public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsDb")));
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddScoped<IContactsService, ContactsService>();
    }
}