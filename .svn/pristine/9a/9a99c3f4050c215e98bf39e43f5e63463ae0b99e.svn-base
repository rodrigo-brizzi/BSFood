using BSFood.Apoio;
using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
{
    public interface ICidades
    {
        Retorno RetornaCidade(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaCidade(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno RetornaListaEstado();

        Retorno SalvarCidade(tbCidade objCidade, int intFunCodigo);

        Retorno ExcluirCidade(int intCodigo);
    }
}
