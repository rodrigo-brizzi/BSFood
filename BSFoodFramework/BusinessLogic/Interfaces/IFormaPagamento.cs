using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IFormaPagamento
    {
        Retorno RetornaFormaPagamento(int intCodigo, enNavegacao? enDirecao);

        Retorno RetornaListaFormaPagamento();

        Retorno RetornaListaFormaPagamento(string strCodigo, string strNome, int intSkip, int intTake);

        Retorno RetornaListaFormaPagamentoTipo();

        Retorno SalvarFormaPagamento(tbFormaPagamento objFormaPagamento, int intFunCodigo);

        Retorno ExcluirFormaPagamento(int intCodigo);
    }
}
