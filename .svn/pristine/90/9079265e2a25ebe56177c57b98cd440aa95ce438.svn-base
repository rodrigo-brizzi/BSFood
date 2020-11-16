using BSFoodFramework.DataAccess;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Apoio;
using BSFoodFramework.BusinessLogic.Interfaces;
using BSFoodFramework.Models;
using System;
using System.Linq;

namespace BSFoodFramework.BusinessLogic
{
    public class Empresa : IEmpresa, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public Empresa()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Empresa(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaEmpresa()
        {
            var objRetorno = new Retorno();
            try
            {
                var objEmpresa = _objCtx.tbEmpresa.AsNoTracking().FirstOrDefault();
                if (objEmpresa != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objEmpresa;
                }
                else
                {
                    objRetorno.intCodigoErro = 48;
                    objRetorno.strMsgErro = "Registro não encontrado";
                }
            }
            catch (Exception ex)
            {
                FrameworkUtil.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        public Retorno SalvarEmpresa(tbEmpresa objEmpresa, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaEmpresa(objEmpresa);
            try
            {
                if (strValidacao == string.Empty)
                {
                    var objEmpresaContexto = _objCtx.tbEmpresa.FirstOrDefault();
                    _objCtx.Entry(objEmpresaContexto).CurrentValues.SetValues(objEmpresa);
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objEmpresa.emp_codigo, enOperacao.Alteracao, objEmpresa, intFunCodigo);
                    objRetorno = RetornaEmpresa();
                }
                else
                {
                    objRetorno.intCodigoErro = 48;
                    objRetorno.strMsgErro = strValidacao;
                }
            }
            catch (Exception ex)
            {
                FrameworkUtil.LogErro(ex);
                objRetorno.intCodigoErro = 16;
                objRetorno.strMsgErro = ex.Message;
                objRetorno.strExceptionToString = ex.ToString();
            }
            return objRetorno;
        }

        private static string ValidaEmpresa(tbEmpresa objEmpresa)
        {
            if (string.IsNullOrEmpty(objEmpresa.emp_nome) || string.IsNullOrWhiteSpace(objEmpresa.emp_nome))
                return "O nome deve ser informado.";
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
