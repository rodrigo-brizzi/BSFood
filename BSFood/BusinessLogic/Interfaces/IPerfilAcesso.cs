using BSFood.Apoio;
using BSFood.DataTransfer;
using BSFood.Models;

namespace BSFood.BusinessLogic.Interfaces
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
