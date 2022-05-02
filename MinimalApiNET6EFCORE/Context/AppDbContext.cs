using Microsoft.EntityFrameworkCore;
using MinimalApiNET6EFCORE.Models;

namespace MinimalApiNET6EFCORE.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        /**
         * A classe DbSet é a herança de DbContext, e ela vai ser a responsavel para transformar metodos LINQ em consultas no banco de dados.
         */
        public DbSet<Produto>? Produtos { get; set; } //Aqui fazendo uma referencia ao EFCORE para criação da tabela Produtos que referencia o tipo complexo Produto, criado na pasta models

        public DbSet<Categoria>? Categorias { get; set; }//Aqui fazendo uma referencia ao EFCORE para criação da tabela Categorias que referencia o tipo complexo Categoria, criado na pasta models


        protected override void OnModelCreating(ModelBuilder mb) //Este método será chamado quando o contexto ser criado pela primeira vez para construir o modelo e seus mapeamentos na memoria
        {
            //Aqui configuramos as propriedades das nossas entidades e definimos o relacionamento entre elas de forma explicita usando a fluent api, sobreescrevendo as convenções do EFCORE
            //Fazemos isso pois quando o EFCORE criar a tabela no banco pela primeira vez, irá aproximar os tipos de variaveis C# com os tipos de dados SQL, sempre com o tamanho maximo permitido no banco
            //O que não é uma boa pratica quando falamos de armazenamento de dados, então na fluent api, conseguimos setar tudo como será criado no banco explicitamente
            //Fluent api e um padrão de design, e a classe ModelBuilder do EFCORE atua como uma Fluent API

            //CATEGORIA
            mb.Entity<Categoria>().HasKey(c => c.CategoriaId);

            mb.Entity<Categoria>().Property(c => c.Nome)
                                  .HasMaxLength(100)
                                  .IsRequired();

            mb.Entity<Categoria>().Property(c => c.Descricao)
                                  .HasMaxLength(100)
                                  .IsRequired();



            //PRODUTO
            mb.Entity<Produto>().HasKey(c => c.ProdutoId);

            mb.Entity<Produto>().Property(c => c.Nome)
                                .HasMaxLength(100)
                                .IsRequired();

            mb.Entity<Produto>().Property(c => c.Descricao)
                                .HasMaxLength(150)
                                .IsRequired();

            mb.Entity<Produto>().Property(c => c.Imagem).HasMaxLength(100);

            mb.Entity<Produto>().Property(c => c.Preco).HasPrecision(14, 2);


            //RELACIONAMENTO
            mb.Entity<Produto>()
              .HasOne<Categoria>(c => c.Categoria)
              .WithMany(p => p.Produtos)
              .HasForeignKey(c => c.CategoriaId);


        }
    }
}
