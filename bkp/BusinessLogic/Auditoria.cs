using BSFoodFramework.DataAccess;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Apoio;
using BSFoodFramework.BusinessLogic.Interfaces;
using BSFoodFramework.Models;
using System;
using System.Linq;
using System.Data.Entity;

namespace BSFoodFramework.BusinessLogic
{
    public class Auditoria : IAuditoria, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public Auditoria()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Auditoria(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaListaAuditoria(int intCodRegistro, object objTabela)
        {
            var objRetorno = new Retorno();
            try
            {
                var strNomeTabela = objTabela.GetType().Name.Split('_')[0];
                var arrAuditoria = _objCtx.tbAuditoria
                                                .Include(audo => audo.tbAuditoriaOperacao)
                                                .Include(fun => fun.tbFuncionario).AsNoTracking()
                                                .Where(aud => aud.aud_nomeTabela == strNomeTabela && aud.aud_codigoRegistro == intCodRegistro)
                                                .OrderBy(aud => aud.aud_codigo)
                                                .ToList();
                objRetorno.intCodigoErro = 0;
                objRetorno.objRetorno = arrAuditoria;
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

        public void SalvarAuditoria(int intCodRegistro, enOperacao enAcao, object objTabela, int intFunCodigo)
        {
            var objAuditoria = new tbAuditoria()
            {
                aud_codigoRegistro = intCodRegistro,
                aud_data = DateTime.Now,
                aud_nomeTabela = objTabela.GetType().Name.Split('_')[0],
                aud_login = _objCtx.tbFuncionario.AsNoTracking().First(fun => fun.fun_codigo == intFunCodigo).fun_login,
                fun_codigo = intFunCodigo,
                audo_codigo = (int)enAcao
            };
            _objCtx.tbAuditoria.Add(objAuditoria);
            _objCtx.SaveChanges();
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}
