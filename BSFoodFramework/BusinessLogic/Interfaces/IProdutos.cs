using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IProdutos
    {
        Retorno RetornaProduto(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaProduto(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarProduto(tbProduto objProduto, int intFunCodigo);

        Retorno ExcluirProduto(int intCodigo);
    }
}
