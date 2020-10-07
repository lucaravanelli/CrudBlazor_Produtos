using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor_MathGrid.Data
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetProdutos();
    }
}
