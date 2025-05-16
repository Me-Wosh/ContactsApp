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

app.MapContactsEndpoints();

app.Run();