using BSFood.Apoio;
using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
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
