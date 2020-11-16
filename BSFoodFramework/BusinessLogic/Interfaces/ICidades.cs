using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
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
