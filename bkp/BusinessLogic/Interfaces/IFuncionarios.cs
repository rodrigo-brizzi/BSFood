using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IFuncionarios
    {
        Retorno RetornaFuncionario(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaFuncionario(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno SalvarFuncionario(tbFuncionario objFuncionario, int intFunCodigo);

        Retorno ExcluirFuncionario(int intCodigo);

        Retorno AutenticaFuncionario(string strLogin, string strSenha);
    }
}
