using BSFoodFramework.DataAccess;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.Apoio;
using BSFoodFramework.BusinessLogic.Interfaces;
using BSFoodFramework.Models;
using System;
using System.Linq;

namespace BSFoodFramework.BusinessLogic
{
    public class Fornecedores : IFornecedores, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public Fornecedores()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Fornecedores(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaFornecedor(int intCodigo, enNavegacao? enDirecao)
        {
            var objRetorno = new Retorno();
            try
            {
                tbFornecedor objFornecedor = null;
                if (enDirecao == null)
                    objFornecedor = _objCtx.tbFornecedor
                                                .AsNoTracking()
                                                .FirstOrDefault(forn => forn.for_codigo == intCodigo);
                if (enDirecao == enNavegacao.Proximo)
                    objFornecedor = _objCtx.tbFornecedor.AsNoTracking()
                                                .Where(forn => forn.for_codigo > intCodigo)
                                                .OrderBy(forn => forn.for_codigo).FirstOrDefault();
                if (enDirecao == enNavegacao.Anterior)
                    objFornecedor = _objCtx.tbFornecedor.AsNoTracking()
                                                .Where(forn => forn.for_codigo < intCodigo)
                                                .OrderByDescending(forn => forn.for_codigo).FirstOrDefault();
                if (objFornecedor != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objFornecedor;
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

        public Retorno RetornaListaFornecedor(string strCodigo, string strNome, int intSkip, int intTake)
        {
            var objRetorno = new Retorno();
            try
            {
                var arrFornecedor = _objCtx.tbFornecedor.AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(strCodigo))
                {
                    int intCodigo = Convert.ToInt32(strCodigo);
                    arrFornecedor = arrFornecedor.Where(forn => forn.for_codigo == intCodigo).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(strNome))
                    arrFornecedor = arrFornecedor.Where(forn => forn.for_nome.Contains(strNome)).AsQueryable();
                objRetorno.intCodigoErro = 0;
                if (intSkip == 0)
                    objRetorno.intQtdeRegistro = arrFornecedor.Count();
                objRetorno.objRetorno = arrFornecedor.OrderBy(forn => forn.for_codigo).Skip(intSkip).Take(intTake).ToList();
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

        public Retorno SalvarFornecedor(tbFornecedor objFornecedor, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaFornecedor(objFornecedor);
            try
            {
                if (strValidacao == string.Empty)
                {
                    enOperacao enTipoOperacao;
                    if (objFornecedor.for_codigo == 0)
                    {
                        enTipoOperacao = enOperacao.Inclusao;
                        _objCtx.tbFornecedor.Add(objFornecedor);
                    }
                    else
                    {
                        enTipoOperacao = enOperacao.Alteracao;
                        var objFornecedorContexto = _objCtx.tbFornecedor.FirstOrDefault(forn => forn.for_codigo == objFornecedor.for_codigo);
                        _objCtx.Entry(objFornecedorContexto).CurrentValues.SetValues(objFornecedor);
                    }
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objFornecedor.for_codigo, enTipoOperacao, objFornecedor, intFunCodigo);
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

        public Retorno ExcluirFornecedor(int intCodigo)
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
                            var objFornecedor = objContexto.tbFornecedor.FirstOrDefault(forn => forn.for_codigo == intCodigo);
                            if (objFornecedor != null)
                            {
                                //Tenta excluir o perfil no contexto isolado
                                objContexto.tbFornecedor.Remove(objFornecedor);
                                objContexto.SaveChanges();
                                transacao.Commit();

                                objRetorno.intCodigoErro = 0;
                                objRetorno.objRetorno = true;
                            }
                            else
                            {
                                objRetorno.intCodigoErro = 48;
                                objRetorno.strMsgErro = "Fornecedor não encontrado para exclusão";
                            }
                        }
                        catch (Exception)
                        {
                            //Se deu erro é porque o perfil tem  registros relacionado
                            transacao.Rollback();
                            objRetorno.intCodigoErro = 48;
                            objRetorno.strMsgErro = "Fornecedor não pode ser excluido pois há registros relacionados ao mesmo";
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

        private string ValidaFornecedor(tbFornecedor objFornecedor)
        {
            if (string.IsNullOrEmpty(objFornecedor.for_nome) || string.IsNullOrWhiteSpace(objFornecedor.for_nome))
                return "O nome deve ser informado.";

            return _objCtx.tbFornecedor.AsNoTracking().Any(forn => (forn.for_nome.Equals(objFornecedor.for_nome)) && forn.for_codigo != objFornecedor.for_codigo) ? "Já existe fornecedor com esse nome." : string.Empty;
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}
