using BSFoodFramework.Apoio;
using BSFoodFramework.BusinessLogic.Interfaces;
using BSFoodFramework.DataAccess;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Models;
using System;
using System.Linq;

namespace BSFoodFramework.BusinessLogic
{
    public class Configuracao : IConfiguracao, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public Configuracao()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Configuracao(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaConfiguracao()
        {
            var objRetorno = new Retorno();
            try
            {
                var objConfiguracao = _objCtx.tbConfiguracao.AsNoTracking().FirstOrDefault();
                if (objConfiguracao != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objConfiguracao;
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

        public Retorno SalvarConfiguracao(tbConfiguracao objConfiguracao, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaConfiguracao(objConfiguracao);
            try
            {
                if (strValidacao == string.Empty)
                {
                    var objConfiguracaoContexto = _objCtx.tbConfiguracao.FirstOrDefault();
                    _objCtx.Entry(objConfiguracaoContexto).CurrentValues.SetValues(objConfiguracao);
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objConfiguracao.cfg_codigo, enOperacao.Alteracao, objConfiguracao, intFunCodigo);
                    objRetorno = RetornaConfiguracao();
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

        private string ValidaConfiguracao(tbConfiguracao objConfiguracao)
        {
            if (string.IsNullOrEmpty(objConfiguracao.cfg_cnpjSoftwareHouse) || string.IsNullOrWhiteSpace(objConfiguracao.cfg_cnpjSoftwareHouse))
                return "O Cnpj da SoftwareHouse deve ser informado.";
            if (string.IsNullOrEmpty(objConfiguracao.cfg_impressoraEntrega) || string.IsNullOrWhiteSpace(objConfiguracao.cfg_impressoraEntrega))
                return "A impressora de entrega deve ser informada.";
            if (string.IsNullOrEmpty(objConfiguracao.cfg_impressoraComanda) || string.IsNullOrWhiteSpace(objConfiguracao.cfg_impressoraComanda))
                return "A impressora de comanda deve ser informada.";
            if (string.IsNullOrEmpty(objConfiguracao.cfg_impressoraBebida) || string.IsNullOrWhiteSpace(objConfiguracao.cfg_impressoraBebida))
                return "A impressora das bebidas deve ser informada.";
            if (string.IsNullOrEmpty(objConfiguracao.cfg_impressoraBalcao) || string.IsNullOrWhiteSpace(objConfiguracao.cfg_impressoraBalcao))
                return "A impressora do balcão deve ser informada.";
            return string.Empty;
        }        

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}
