using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Blazor_MathGrid.Data
{
    public class ProdutoService : IProdutoService
    {

        private readonly SqlConnectionConfiguration _configuration;
        public ProdutoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<List<Produto>> GetProdutos()
        {
            List<Produto> produtos = new List<Produto>();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.ConnectionString))
                {
                    const string query = "Select * from Produtos";
                    SqlCommand cmd = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text
                    };
                    con.Open();
                    SqlDataReader rd = await cmd.ExecuteReaderAsync();
                    while (rd.Read())
                    {
                        Produto produto = new Produto
                        {
                            ProdutoId = Convert.ToInt32(rd["ProdutoId"]),
                            Nome = rd["Nome"].ToString(),
                            Descricao = rd["Descricao"].ToString(),
                            Preco = Convert.ToDecimal(rd["Preco"]),
                            Ativo = Convert.ToBoolean(rd["Ativo"]),
                            ImagemUrl = rd["ImagemUrl"].ToString(),
                        };
                        produtos.Add(produto);
                    }
                    cmd.Dispose();
                }
                return produtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
