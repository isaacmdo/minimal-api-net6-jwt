using Microsoft.EntityFrameworkCore;
using MinimalApiNET6EFCORE.Context;
using MinimalApiNET6EFCORE.Models;

//Minimal apis usando os metodos .MapGet, .MapPost, .MapPut, .MapDelete da classe WebApplication, assim podemos criar endpoints em seus parametros 


namespace MinimalApiNET6EFCORE.ApiEndpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {

            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            }).WithTags("Categorias").RequireAuthorization();

            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).WithTags("Categorias").RequireAuthorization();

            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id) is Categoria categoria ? Results.Ok(categoria) : Results.NotFound();
            }).WithTags("Categorias");

            app.MapPut("/categoria/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest();
                }

                var categoriaDB = await db.Categorias.FindAsync(id);

                if (categoriaDB is null) return Results.NotFound();

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(categoriaDB);
            }).WithTags("Categorias");

            app.MapDelete("/categoria/{id:int}", async (int id, AppDbContext db) =>
            {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null) return Results.NotFound();

                db.Categorias.Remove(categoria);
                db.SaveChangesAsync();

                return Results.NoContent();
            }).WithTags("Categorias");
        }
    }
}
