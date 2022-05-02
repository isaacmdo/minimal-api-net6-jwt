﻿using System.Text.Json.Serialization;

namespace MinimalApiNET6EFCORE.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; } 

        public string? Nome { get; set; }

        public string? Descricao {  get; set; }

        public decimal Preco { get; set; }

        public string? Imagem { get; set; }

        public DateTime DataCompra { get; set; }

        public int Estoque { get; set; }

        

        //Estas propriedades estão reforçando o relacionamento de Categoria e Produto, elas são propriedades de navegação
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

    }
}
