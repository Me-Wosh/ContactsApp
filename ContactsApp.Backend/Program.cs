using ContactsApp.Backend.Configuration;
using ContactsApp.Backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapGroup("/contacts").MapContactsEndpoints();
app.MapGroup("/auth").MapAuthenticationEndpoints();

app.Run();