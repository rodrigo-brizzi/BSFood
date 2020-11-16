using BSFood.DataAccess;
using BSFood.DataTransfer;
using BSFood.Apoio;
using BSFood.BusinessLogic.Interfaces;
using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BSFood.BusinessLogic
{
    public class Mesa : IMesa, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;
        
        public Mesa()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Mesa(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaMesa(int intCodigo)
        {
            var objRetorno = new Retorno();
            try
            {
                var objMesa = _objCtx.tbMesa.AsNoTracking().Include(ped => ped.tbPedido.tbPedidoProduto.Select(pro => pro.tbProduto))
                                                       .Include(fpg => fpg.tbPedido.tbFormaPagamento)
                                                       .FirstOrDefault(mes => mes.mes_codigo == intCodigo);
                if (objMesa != null)
                {
                    if (objMesa.tbPedido == null)
                    {
                        objMesa.tbPedido = new tbPedido
                        {
                            ped_data = DateTime.Now,
                            cli_codigo = 1,
                            ped_numeroMesa = objMesa.mes_numero,
                            tbPedidoProduto = new List<tbPedidoProduto>()
                        };

                        var objPedidoProduto = new tbPedidoProduto {tbProduto = new tbProduto()};
                        objMesa.tbPedido.tbPedidoProduto.Add(objPedidoProduto);
                    }
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objMesa;
                }
                else
                {
                    objRetorno.intCodigoErro = 48;
                    objRetorno.strMsgErro = "Registro não encontrado";
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        public Retorno RetornaListaMesa(string strParametro, bool blnLivre = true, bool blnOcupado = true)
        {
            var objRetorno = new Retorno();
            try
            {
                var query = _objCtx.tbMesa.AsNoTracking().OrderBy(mes => mes.mes_codigo).AsQueryable();
                if (!blnLivre)
                    query = query.Where(mes => mes.mes_status != enStatusMesa.L.ToString()).AsQueryable();
                if (!blnOcupado)
                    query = query.Where(ped => ped.mes_status != enStatusMesa.O.ToString()).AsQueryable();
                objRetorno.intCodigoErro = 0;
                objRetorno.objRetorno = query.OrderBy(mes => mes.mes_codigo).ToList();
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        public Retorno SalvarMesa(tbMesa objMesa, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaMesa(objMesa);
            try
            {
                if (strValidacao == string.Empty)
                {
                    var intPedCodigo = objMesa.tbPedido.ped_codigo;
                    using (var objBllPedido = new Pedidos())
                    {
                        objRetorno = objBllPedido.SalvarPedido(objMesa.tbPedido, enOrigemPedido.Comanda, intFunCodigo);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        var objPedidoAux = objRetorno.objRetorno as tbPedido;
                        if (intPedCodigo == 0)
                        {
                            objMesa.tbPedido = null;
                            objMesa.mes_status = enStatusMesa.O.ToString();//O
                            if (objPedidoAux != null) objMesa.ped_codigo = objPedidoAux.ped_codigo;
                            var objMesaContexto = _objCtx.tbMesa.FirstOrDefault(mes => mes.mes_codigo == objMesa.mes_codigo);
                            _objCtx.Entry(objMesaContexto).CurrentValues.SetValues(objMesa);
                            _objCtx.SaveChanges();
                            using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                                objBll.SalvarAuditoria(objMesa.mes_codigo, enOperacao.Outro, objMesa, intFunCodigo);
                        }
                        objMesa.tbPedido = objPedidoAux;
                        objRetorno.objRetorno = objMesa;
                    }
                }
                else
                {
                    objRetorno.intCodigoErro = 48;
                    objRetorno.strMsgErro = strValidacao;
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        public Retorno FecharMesa(tbMesa objMesa, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaFechamentoMesa(objMesa);
            try
            {
                if (strValidacao == string.Empty)
                {
                    if (objMesa.tbPedido.ped_codigo > 0)
                    {
                        objMesa.tbPedido.ped_status = enStatusPedido.F.ToString();
                        using (var objBllPedido = new Pedidos())
                        {
                            objRetorno = objBllPedido.SalvarPedido(objMesa.tbPedido, enOrigemPedido.Comanda, intFunCodigo);
                        }
                        if (objRetorno.intCodigoErro == 0)
                        {
                            objMesa.tbPedido = null;
                            objMesa.mes_status = enStatusMesa.L.ToString();//"L";
                            objMesa.ped_codigo = null;
                            var objMesaContexto = _objCtx.tbMesa.FirstOrDefault(mes => mes.mes_codigo == objMesa.mes_codigo);
                            _objCtx.Entry(objMesaContexto).CurrentValues.SetValues(objMesa);
                            _objCtx.SaveChanges();
                            using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                                objBll.SalvarAuditoria(objMesa.mes_codigo, enOperacao.Outro, objMesa, intFunCodigo);

                            objRetorno = RetornaMesa(objMesa.mes_codigo);
                        }
                    }
                    else
                    {
                        objRetorno.intCodigoErro = 48;
                        objRetorno.strMsgErro = "Pedido não encontrado!";
                    }
                }
                else
                {
                    objRetorno.intCodigoErro = 48;
                    objRetorno.strMsgErro = strValidacao;
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        public Retorno ExcluirMesa(tbMesa objMesa, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            try
            {
                if (objMesa.tbPedido.ped_codigo > 0)
                {
                    objMesa.tbPedido.ped_status = enStatusPedido.X.ToString();//"X";
                    using (var objBllPedido = new Pedidos())
                    {
                        objRetorno = objBllPedido.SalvarPedido(objMesa.tbPedido, enOrigemPedido.Comanda, intFunCodigo);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objMesa.tbPedido = null;
                        objMesa.mes_status = enStatusMesa.L.ToString();//"L";
                        objMesa.ped_codigo = null;
                        var objMesaContexto = _objCtx.tbMesa.FirstOrDefault(mes => mes.mes_codigo == objMesa.mes_codigo);
                        _objCtx.Entry(objMesaContexto).CurrentValues.SetValues(objMesa);
                        _objCtx.SaveChanges();
                        using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                            objBll.SalvarAuditoria(objMesa.mes_codigo, enOperacao.Outro, objMesa, intFunCodigo);

                        objRetorno = RetornaMesa(objMesa.mes_codigo);
                    }
                }
                else
                {
                    objRetorno.intCodigoErro = 48;
                    objRetorno.strMsgErro = "Pedido não encontrado!";
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        private string ValidaMesa(tbMesa objMesa)
        {
            return objMesa.tbPedido == null ? "Nenhum pedido para a mesa informada." : string.Empty;
        }

        private string ValidaFechamentoMesa(tbMesa objMesa)
        {
            if (objMesa.tbPedido == null)
                return "Nenhum pedido para a mesa informada.";
            if (objMesa.tbPedido.tbPedidoProduto.Count == 0)
                return "Nenhum produto informado.";
            if (objMesa.tbPedido.fpg_codigo == null || objMesa.tbPedido.fpg_codigo == 0)
                return "Forma de pagamento não informada.";
            return objMesa.tbPedido.ped_valorRecebido == 0 ? "Valor recebido não informado." : string.Empty;
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}