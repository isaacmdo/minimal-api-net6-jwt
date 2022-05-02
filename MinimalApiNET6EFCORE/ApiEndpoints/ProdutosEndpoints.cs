using Microsoft.EntityFrameworkCore;
using MinimalApiNET6EFCORE.Context;
using MinimalApiNET6EFCORE.Models;

//Minimal apis usando os metodos .MapGet, .MapPost, .MapPut, .MapDelete da classe WebApplication, assim podemos criar endpoints em seus parametros 


namespace MinimalApiNET6EFCORE.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints(this WebApplication app)
        {


            app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produtos/{produto.ProdutoId}", produto);
            }).WithTags("Produtos");

            app.MapGet("/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync()).RequireAuthorization().WithTags("Produtos");

            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Produtos.FindAsync(id) is Produto categoria ? Results.Ok(categoria) : Results.NotFound();
            }).WithTags("Produtos");

            app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
            {
                if (produto.ProdutoId != id)
                {
                    return Results.BadRequest();
                }

                var produtoDB = await db.Produtos.FindAsync(id);

                if (produtoDB is null) return Results.NotFound();

                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Preco = produto.Preco;
                produtoDB.Imagem = produto.Imagem;
                produtoDB.Estoque = produto.Estoque;
                produtoDB.CategoriaId = produto.CategoriaId;


                await db.SaveChangesAsync();
                return Results.Ok(produtoDB);
            }).WithTags("Produtos");

            app.MapDelete("/produto/{id:int}", async (int id, AppDbContext db) =>
            {
                var produto = await db.Produtos.FindAsync(id);

                if (produto is null) return Results.NotFound();

                db.Produtos.Remove(produto);
                db.SaveChangesAsync();

                return Results.NoContent();
            }).WithTags("Produtos");

        }
    }
}
