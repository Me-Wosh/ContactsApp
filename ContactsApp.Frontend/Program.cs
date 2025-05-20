using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ContactsApp.Frontend;
using ContactsApp.Frontend.StateManagement;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var address = builder.Configuration.GetSection("Backend").GetSection("Address").Value!;

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(address)
});

builder.Services.AddSingleton<JwtState>();

await builder.Build().RunAsync();