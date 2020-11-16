using BSFoodFramework.Apoio;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IPedidos
    {
        #region Entrega

        Retorno RetornaPedido(int intCodigo, enNavegacao? enDirecao, enOrigemPedido enOrigem);

        Retorno RetornaListaPedidoEntrega(bool blnProducao, bool blnEntrega, bool blnFinalizado, bool blnExcluido, int intFunCodigo, int intCaiCodigo, int intSkip, int intTake);

        Retorno SalvarPedidoEntrega(tbPedido objPedido, int intFunCodigo);

        Retorno SalvarPedido(tbPedido objPedido, enOrigemPedido enOrigem, int intFunCodigo);

        Retorno ExcluirPedido(int intCodigo, string strMotivoCancelamento, int intFunCodigo);

        Retorno SalvarEntregador(tbPedido objPedido, int intFunCodigo);

        #endregion Entrega


        #region Comanda

        Retorno RetornaPedidoComanda(int intNumero);

        Retorno SalvarPedidoComanda(tbPedido objPedido, int intFunCodigo);

        #endregion Comanda


        #region Mesa

        Retorno RetornaListaMesa(int? intNumero, bool blnLivre = true, bool blnOcupado = true, string strTerminal = "");

        Retorno RetornaPedidoMesa(int intNumero, string strTerminal);

        Retorno FecharPedidoMesa(tbPedido objPedido);

        #endregion Mesa
    }
}