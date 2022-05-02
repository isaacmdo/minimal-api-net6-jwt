using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApiNET6EFCORE;
using MinimalApiNET6EFCORE.ApiEndpoints;
using MinimalApiNET6EFCORE.AppServicesExtentions;
using MinimalApiNET6EFCORE.Context;
using MinimalApiNET6EFCORE.Models;
using MinimalApiNET6EFCORE.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args); //Inicialização da api

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();


var app = builder.Build(); //Build da aplicacao


app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();

var enviroment = app.Environment;
app.UseExceptionHandling(enviroment).UseSwaggerMiddleware()
                                    .UseCors();



app.UseAuthentication();
app.UseAuthorization();
app.Run();