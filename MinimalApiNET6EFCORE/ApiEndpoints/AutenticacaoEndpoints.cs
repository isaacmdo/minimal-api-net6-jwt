using Microsoft.AspNetCore.Authorization;
using MinimalApiNET6EFCORE.Services;

//Minimal apis usando os metodos .MapGet, .MapPost, .MapPut, .MapDelete da classe WebApplication, assim podemos criar endpoints em seus parametros 


namespace MinimalApiNET6EFCORE.ApiEndpoints
{
    public static class AutenticacaoEndpoints
    {
        public static void MapAutenticacaoEndpoints(this WebApplication app)
        {

            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel == null)
                {
                    return Results.BadRequest("Login inválido");
                }
                if (userModel.UserName == "marcoratti" && userModel.Password == "numsey#123")
                {

                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel);
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login invalido");
                }

            }).Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status200OK)
              .WithName("Login")
              .WithTags("Autenticacao");
        }
    }
}
