using BSFood.Apoio;
using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
{
    public interface IFornecedores
    {
        Retorno RetornaFornecedor(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaFornecedor(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarFornecedor(tbFornecedor objFornecedor, int intFunCodigo);

        Retorno ExcluirFornecedor(int intCodigo);
    }
}
