using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IBairros
    {
        Retorno RetornaBairro(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaBairro(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno RetornaListaBairro();

        Retorno SalvarBairro(tbBairro objBairro, int intFunCodigo);

        Retorno ExcluirBairro(int intCodigo);
    }
}
