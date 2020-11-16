using BSFood.Apoio;
using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
{
    public interface IProdutos
    {
        Retorno RetornaProduto(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaProduto(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarProduto(tbProduto objProduto, int intFunCodigo);

        Retorno ExcluirProduto(int intCodigo);
    }
}
