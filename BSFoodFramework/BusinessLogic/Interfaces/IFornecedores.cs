using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IFornecedores
    {
        Retorno RetornaFornecedor(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaFornecedor(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarFornecedor(tbFornecedor objFornecedor, int intFunCodigo);

        Retorno ExcluirFornecedor(int intCodigo);
    }
}
