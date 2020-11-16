using BSFoodFramework.DataTransfer;
using System;
using BSFoodFramework.Models;

namespace BSFoodFramework.BusinessLogic.Interfaces
{
    public interface IRelatorios
    {
        #region Cupons

        Retorno RetornaCupomEntrega(tbPedido objPedido, int? intPedCodigo = null);

        Retorno RetornaCaixaFechamento(tbCaixa objCaixa);

        Retorno RetornaTicketConferencia(tbPedido objPedido, int? intPedCodigo = null);

        Retorno RetornaCupomComanda(tbPedido objPedido);

        #endregion



        #region Pedidos

        Retorno RetornaPedidoPorEntregador(DateTime dtDataInicial, DateTime dtDataFinal, int? intFunCodigo, int intCaiCodigo, string strEmpresa);

        #endregion Pedidos
    }
}
