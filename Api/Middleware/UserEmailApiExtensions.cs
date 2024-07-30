using ApiGateway.Dtos;
using ApiGateway.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ApiGateway.Middleware
{
    public static class UserEmailApiExtensions
    {
        public static IEndpointRouteBuilder MapUserEmailApi(this IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (IUserEmailService userEmailService, RegisterUserDto request) =>
            {
                var response = await userEmailService.RegisterUserAndSendEmailAsync(request);
                return Results.Ok(response);
            }).WithName("RegisterUser");

            return app;
        }
    }
}
