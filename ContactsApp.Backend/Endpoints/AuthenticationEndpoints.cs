using ContactsApp.Backend.Services;
using ContactsApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Backend.Endpoints;

// /auth endpoints for registering, logging in user and refreshing tokens
public static class AuthenticationEndpoints
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this RouteGroupBuilder routes)
    {
        routes.MapPost("/login", (IAuthenticationService authenticationService, [FromBody]LoginUserDto userDto) =>
        {
            return authenticationService.Login(userDto);
        });
        
        routes.MapPost("/register", (IAuthenticationService authenticationService, [FromBody]RegisterUserDto userDto) =>
        {
            return authenticationService.Register(userDto);
        });

        routes.MapPost("/refresh-tokens", (IAuthenticationService authenticationService) =>
        {
            return authenticationService.RefreshTokens();
        }).RequireAuthorization();

        return routes;
    }
}