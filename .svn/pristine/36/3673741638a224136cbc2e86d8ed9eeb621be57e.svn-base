using BSFood.DataAccess;
using BSFood.DataTransfer;
using BSFood.Apoio;
using BSFood.BusinessLogic.Interfaces;
using BSFood.Models;
using System;
using System.Linq;

namespace BSFood.BusinessLogic
{
    public class ClienteGrupos : IClienteGrupos, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public ClienteGrupos()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public ClienteGrupos(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaClienteGrupo(int intCodigo, enNavegacao? enDirecao)
        {
            var objRetorno = new Retorno();
            try
            {
                tbClienteGrupo objClienteGrupo = null;
                if (enDirecao == null)
                    objClienteGrupo = _objCtx.tbClienteGrupo
                                                    .AsNoTracking()
                                                    .FirstOrDefault(cgr => cgr.cgr_codigo == intCodigo);
                if (enDirecao == enNavegacao.Proximo)
                    objClienteGrupo = _objCtx.tbClienteGrupo.AsNoTracking()
                                                    .Where(cgr => cgr.cgr_codigo > intCodigo)
                                                    .OrderBy(cgr => cgr.cgr_codigo).FirstOrDefault();
                if (enDirecao == enNavegacao.Anterior)
                    objClienteGrupo = _objCtx.tbClienteGrupo.AsNoTracking()
                                                    .Where(cgr => cgr.cgr_codigo < intCodigo)
                                                    .OrderByDescending(cgr => cgr.cgr_codigo).FirstOrDefault();
                if (objClienteGrupo != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objClienteGrupo;
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

        public Retorno RetornaListaClienteGrupo(string strCodigo, string strNome, int intSkip, int intTake)
        {
            var objRetorno = new Retorno();
            try
            {
                var arrClienteGrupo = _objCtx.tbClienteGrupo.AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(strCodigo))
                {
                    int intCodigo = Convert.ToInt32(strCodigo);
                    arrClienteGrupo = arrClienteGrupo.Where(cgr => cgr.cgr_codigo == intCodigo).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(strNome))
                    arrClienteGrupo = arrClienteGrupo.Where(cgr => cgr.cgr_nome.Contains(strNome)).AsQueryable();
                objRetorno.intCodigoErro = 0;
                if (intSkip == 0)
                    objRetorno.intQtdeRegistro = arrClienteGrupo.Count();
                objRetorno.objRetorno = arrClienteGrupo.OrderBy(cgr => cgr.cgr_codigo).Skip(intSkip).Take(intTake).ToList();
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

        public Retorno SalvarClienteGrupo(tbClienteGrupo objClienteGrupo, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaClienteGrupo(objClienteGrupo);
            try
            {
                if (strValidacao == string.Empty)
                {
                    enOperacao enTipoOperacao;
                    if (objClienteGrupo.cgr_codigo == 0)
                    {
                        enTipoOperacao = enOperacao.Inclusao;
                        _objCtx.tbClienteGrupo.Add(objClienteGrupo);
                    }
                    else
                    {
                        enTipoOperacao = enOperacao.Alteracao;
                        var objClienteGrupoContexto = _objCtx.tbClienteGrupo.FirstOrDefault(cgr => cgr.cgr_codigo == objClienteGrupo.cgr_codigo);
                        _objCtx.Entry(objClienteGrupoContexto).CurrentValues.SetValues(objClienteGrupo);
                    }
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objClienteGrupo.cgr_codigo, enTipoOperacao, objClienteGrupo, intFunCodigo);
                    objRetorno.intCodigoErro = 0;
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

        public Retorno ExcluirClienteGrupo(int intCodigo)
        {
            var objRetorno = new Retorno();
            try
            {
                //Cria um contexto isolado para a trasacao de exclusao
                using (var objContexto = new EFContexto())
                {
                    //Inicia uma transacao no contexto isolado
                    using (var transacao = objContexto.Database.BeginTransaction())
                    {
                        try
                        {
                            var objClienteGrupo = objContexto.tbClienteGrupo.FirstOrDefault(cgr => cgr.cgr_codigo == intCodigo);
                            if (objClienteGrupo != null)
                            {
                                //Tenta excluir o perfil no contexto isolado
                                objContexto.tbClienteGrupo.Remove(objClienteGrupo);
                                objContexto.SaveChanges();
                                transacao.Commit();

                                objRetorno.intCodigoErro = 0;
                                objRetorno.objRetorno = true;
                            }
                            else
                            {
                                objRetorno.intCodigoErro = 48;
                                objRetorno.strMsgErro = "Cliente Grupo não encontrada para exclusão";
                            }
                        }
                        catch (Exception)
                        {
                            //Se deu erro é porque o perfil tem  registros relacionado
                            transacao.Rollback();
                            objRetorno.intCodigoErro = 48;
                            objRetorno.strMsgErro = "Cliente Grupo não pode ser excluido pois há registros relacionados ao mesmo.";
                        }
                    }
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

        private string ValidaClienteGrupo(tbClienteGrupo objClienteGrupo)
        {
            if (string.IsNullOrEmpty(objClienteGrupo.cgr_nome) || string.IsNullOrWhiteSpace(objClienteGrupo.cgr_nome))
                return "O nome deve ser informado.";

            return _objCtx.tbClienteGrupo.AsNoTracking().Any(cgr => (cgr.cgr_nome.Equals(objClienteGrupo.cgr_nome)) && cgr.cgr_codigo != objClienteGrupo.cgr_codigo) ? "Já existe Cliente Grupo com esse nome." : string.Empty;
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}
