using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IConfiguracao
    {
        Retorno RetornaConfiguracao();

        Retorno SalvarConfiguracao(tbConfiguracao objConfiguracao, int intFunCodigo);
    }
}
