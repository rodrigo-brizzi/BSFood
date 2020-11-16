using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IClienteGrupos
    {
        Retorno RetornaClienteGrupo(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaClienteGrupo(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarClienteGrupo(tbClienteGrupo objClienteGrupo, int intFunCodigo);

        Retorno ExcluirClienteGrupo(int intCodigo);
    }
}
