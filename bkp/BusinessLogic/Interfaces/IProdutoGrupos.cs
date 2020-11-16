using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IProdutoGrupos
    {
        Retorno RetornaProdutoGrupo(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaProdutoGrupo();

        Retorno RetornaListaProdutoGrupo(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarProdutoGrupo(tbProdutoGrupo objProdutoGrupo, int intFunCodigo);

        Retorno ExcluirProdutoGrupo(int intCodigo);
    }
}
