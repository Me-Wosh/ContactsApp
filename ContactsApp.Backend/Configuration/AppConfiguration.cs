using System.Text;
using ContactsApp.Backend.Data;
using ContactsApp.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ContactsApp.Backend.Configuration;

// Services used across the app
public static class AppConfiguration
{
    public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // built-in services
        builder.Services.AddOpenApi();
        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("https://localhost:7045", "http://localhost:5218")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        
        // data services
        builder.Services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsDb")));
     
        // external services
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration.GetSection("JWT").GetSection("Issuer").Value,
                ValidateAudience = true,
                ValidAudience = builder.Configuration.GetSection("JWT").GetSection("Audience").Value,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetSection("Secret").Value!)),
            };
        });
        
        // user-defined services
        builder.Services.AddScoped<IContactsService, ContactsService>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}