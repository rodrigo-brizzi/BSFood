using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IPerfilAcesso
    {
        Retorno RetornaPerfilAcesso(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaPerfilAcesso();

        Retorno RetornaListaPerfilAcesso(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno RetornaListaMenu();

        Retorno SalvarPerfilAcesso(tbPerfilAcesso objPerfilAcesso, int intFunCodigo);

        Retorno ExcluirPerfilAcesso(int intCodigo);
    }
}
