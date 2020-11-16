using BSFoodFramework.DataAccess;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Apoio;
using BSFoodFramework.BusinessLogic.Interfaces;
using BSFoodFramework.Models;
using System;
using System.Linq;

namespace BSFoodFramework.BusinessLogic
{
    public class Bairros : IBairros, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public Bairros()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Bairros(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaBairro(int intCodigo, enNavegacao? enDirecao)
        {
            var objRetorno = new Retorno();
            try
            {
                tbBairro objBairro = null;
                if (enDirecao == null)
                    objBairro = _objCtx.tbBairro.AsNoTracking()
                                                    .FirstOrDefault(bai => bai.bai_codigo == intCodigo);
                if (enDirecao == enNavegacao.Proximo)
                    objBairro = _objCtx.tbBairro.AsNoTracking()
                                                    .Where(bai => bai.bai_codigo > intCodigo)
                                                    .OrderBy(bai => bai.bai_codigo).FirstOrDefault();
                if (enDirecao == enNavegacao.Anterior)
                    objBairro = _objCtx.tbBairro.AsNoTracking()
                                                    .Where(bai => bai.bai_codigo < intCodigo)
                                                    .OrderByDescending(bai => bai.bai_codigo).FirstOrDefault();
                if (objBairro != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objBairro;
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

        public Retorno RetornaListaBairro(string strCodigo, string strNome, int intSkip, int intTake)
        {
            var objRetorno = new Retorno();
            try
            {
                var arrBairro = _objCtx.tbBairro.AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(strCodigo))
                {
                    int intCodigo = Convert.ToInt32(strCodigo);
                    arrBairro = arrBairro.Where(bai => bai.bai_codigo == intCodigo).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(strNome))
                    arrBairro = arrBairro.Where(bai => bai.bai_nome.Contains(strNome)).AsQueryable();
                objRetorno.intCodigoErro = 0;
                if (intSkip == 0)
                    objRetorno.intQtdeRegistro = arrBairro.Count();
                objRetorno.objRetorno = arrBairro.OrderBy(bai => bai.bai_codigo).Skip(intSkip).Take(intTake).ToList();
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

        public Retorno RetornaListaBairro()
        {
            var objRetorno = new Retorno();
            try
            {
                var arrBairro = _objCtx.tbBairro.AsNoTracking()
                                                      .OrderBy(bai => bai.bai_nome)
                                                      .ToList();
                objRetorno.intCodigoErro = 0;
                objRetorno.objRetorno = arrBairro;
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

        public Retorno SalvarBairro(tbBairro objBairro, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaBairro(objBairro);
            try
            {
                if (strValidacao == string.Empty)
                {
                    enOperacao enTipoOperacao;
                    if (objBairro.bai_codigo == 0)
                    {
                        enTipoOperacao = enOperacao.Inclusao;
                        _objCtx.tbBairro.Add(objBairro);
                    }
                    else
                    {
                        enTipoOperacao = enOperacao.Alteracao;
                        var objBairroContexto = _objCtx.tbBairro.FirstOrDefault(bai => bai.bai_codigo == objBairro.bai_codigo);
                        _objCtx.Entry(objBairroContexto).CurrentValues.SetValues(objBairro);
                    }
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objBairro.bai_codigo, enTipoOperacao, objBairro, intFunCodigo);
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

        public Retorno ExcluirBairro(int intCodigo)
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
                            var objBairro = objContexto.tbBairro.FirstOrDefault(bai => bai.bai_codigo == intCodigo);
                            if (objBairro != null)
                            {
                                //Tenta excluir o perfil no contexto isolado
                                objContexto.tbBairro.Remove(objBairro);
                                objContexto.SaveChanges();
                                transacao.Commit();

                                objRetorno.intCodigoErro = 0;
                                objRetorno.objRetorno = true;
                            }
                            else
                            {
                                objRetorno.intCodigoErro = 48;
                                objRetorno.strMsgErro = "Bairro não encontrado para exclusão";
                            }
                        }
                        catch (Exception)
                        {
                            //Se deu erro é porque o perfil tem  registros relacionado
                            transacao.Rollback();
                            objRetorno.intCodigoErro = 48;
                            objRetorno.strMsgErro = "Bairro não pode ser excluido pois há registros relacionados ao mesmo.";
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

        private string ValidaBairro(tbBairro objBairro)
        {
            if (string.IsNullOrEmpty(objBairro.bai_nome) || string.IsNullOrWhiteSpace(objBairro.bai_nome))
                return "O nome deve ser informado.";

            return _objCtx.tbBairro.AsNoTracking().Any(bai => (bai.bai_nome.Equals(objBairro.bai_nome)) && bai.bai_codigo != objBairro.bai_codigo) ? "Já existe Bairro com esse nome." : string.Empty;
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}
