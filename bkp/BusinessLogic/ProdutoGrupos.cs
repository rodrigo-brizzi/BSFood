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
    public class ProdutoGrupos : IProdutoGrupos, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public ProdutoGrupos()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public ProdutoGrupos(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaProdutoGrupo(int intCodigo, enNavegacao? enDirecao)
        {
            var objRetorno = new Retorno();
            try
            {
                tbProdutoGrupo objProdutoGrupo = null;
                if (enDirecao == null)
                    objProdutoGrupo = _objCtx.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo)
                                                      .AsNoTracking()
                                                      .FirstOrDefault(pgr => pgr.pgr_codigo == intCodigo);
                if (enDirecao == enNavegacao.Proximo)
                    objProdutoGrupo = _objCtx.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo).AsNoTracking()
                                                                          .Where(pgr => pgr.pgr_codigo > intCodigo)
                                                                          .OrderBy(pgr => pgr.pgr_codigo).FirstOrDefault();
                if (enDirecao == enNavegacao.Anterior)
                    objProdutoGrupo = _objCtx.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo).AsNoTracking()
                                                                          .Where(pgr => pgr.pgr_codigo < intCodigo)
                                                                          .OrderByDescending(pgr => pgr.pgr_codigo).FirstOrDefault();
                if (objProdutoGrupo != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objProdutoGrupo;
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

        public Retorno RetornaListaProdutoGrupo()
        {
            var objRetorno = new Retorno();
            try
            {
                var arrProdutoGrupo = _objCtx.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo).AsNoTracking()
                                                      .OrderBy(pgr => pgr.pgr_nome)
                                                      .ToList();
                objRetorno.intCodigoErro = 0;
                objRetorno.objRetorno = arrProdutoGrupo;
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

        public Retorno RetornaListaProdutoGrupo(string strCodigo, string strNome, int intSkip, int intTake)
        {
            var objRetorno = new Retorno();
            try
            {
                var arrProdutoGrupo = _objCtx.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo).AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(strCodigo))
                {
                    int intCodigo = Convert.ToInt32(strCodigo);
                    arrProdutoGrupo = arrProdutoGrupo.Where(pgr => pgr.pgr_codigo == intCodigo).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(strNome))
                    arrProdutoGrupo = arrProdutoGrupo.Where(pgr => pgr.pgr_nome.Contains(strNome)).AsQueryable();
                objRetorno.intCodigoErro = 0;
                if (intSkip == 0)
                    objRetorno.intQtdeRegistro = arrProdutoGrupo.Count();
                objRetorno.objRetorno = arrProdutoGrupo.OrderBy(pgr => pgr.pgr_codigo).Skip(intSkip).Take(intTake).ToList();
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

        public Retorno SalvarProdutoGrupo(tbProdutoGrupo objProdutoGrupo, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaProdutoGrupo(objProdutoGrupo);
            try
            {
                if (strValidacao == string.Empty)
                {
                    foreach (var objProdutoSubGrupo in objProdutoGrupo.tbProdutoSubGrupo)
                    {
                        objProdutoSubGrupo.tbProduto = null;
                        objProdutoSubGrupo.tbProdutoGrupo = null;
                    }
                    enOperacao enTipoOperacao;
                    if (objProdutoGrupo.pgr_codigo == 0)
                    {
                        enTipoOperacao = enOperacao.Inclusao;
                        _objCtx.tbProdutoGrupo.Add(objProdutoGrupo);
                    }
                    else
                    {
                        enTipoOperacao = enOperacao.Alteracao;
                        var objProdutoGrupoContexto = _objCtx.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo).FirstOrDefault(pgr => pgr.pgr_codigo == objProdutoGrupo.pgr_codigo);
                        //Remover subgrupos que não vieram na coleçao
                        var arrPsgrCodigo = objProdutoGrupo.tbProdutoSubGrupo.Select(psgr => psgr.psgr_codigo).ToArray();
                        if (objProdutoGrupoContexto != null)
                        {
                            _objCtx.tbProdutoSubGrupo.RemoveRange(objProdutoGrupoContexto.tbProdutoSubGrupo.Where(psgr => !arrPsgrCodigo.Contains(psgr.psgr_codigo)));

                            //Alterar os subgrupos que vieram na coleção
                            foreach (var objProdutoSubGrupoContexto in objProdutoGrupoContexto.tbProdutoSubGrupo.Where(psgr => arrPsgrCodigo.Contains(psgr.psgr_codigo)))
                                _objCtx.Entry(objProdutoSubGrupoContexto).CurrentValues.SetValues(objProdutoGrupo.tbProdutoSubGrupo.FirstOrDefault(psgr => psgr.psgr_codigo == objProdutoSubGrupoContexto.psgr_codigo));

                            //Inclui os subgrupos que vieram na coleção sem codigo
                            foreach (var objItem in objProdutoGrupo.tbProdutoSubGrupo.Where(psgr => psgr.psgr_codigo == 0))
                            {
                                objItem.pgr_codigo = objProdutoGrupo.pgr_codigo;
                                _objCtx.tbProdutoSubGrupo.Add(objItem);
                            }

                            //Atualiza o grupo de produtos
                            _objCtx.Entry(objProdutoGrupoContexto).CurrentValues.SetValues(objProdutoGrupo);
                        }
                    }
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objProdutoGrupo.pgr_codigo, enTipoOperacao, objProdutoGrupo, intFunCodigo);
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

        public Retorno ExcluirProdutoGrupo(int intCodigo)
        {
            var objRetorno = new Retorno();
            try
            {
                using (var objContexto = new EFContexto())
                {
                    using (var transacao = objContexto.Database.BeginTransaction())
                    {
                        try
                        {
                            var objProdutoGrupo = objContexto.tbProdutoGrupo.Include(psgr => psgr.tbProdutoSubGrupo).FirstOrDefault(pgr => pgr.pgr_codigo == intCodigo);
                            if (objProdutoGrupo != null)
                            {
                                objContexto.tbProdutoSubGrupo.RemoveRange(objProdutoGrupo.tbProdutoSubGrupo);
                                objContexto.tbProdutoGrupo.Remove(objProdutoGrupo);
                                objContexto.SaveChanges();
                                transacao.Commit();

                                objRetorno.intCodigoErro = 0;
                                objRetorno.objRetorno = true;
                            }
                            else
                            {
                                objRetorno.intCodigoErro = 48;
                                objRetorno.strMsgErro = "Produto Grupo não encontrado para exclusão";
                            }
                        }
                        catch (Exception)
                        {
                            transacao.Rollback();
                            objRetorno.intCodigoErro = 48;
                            objRetorno.strMsgErro = "Produto Grupo não pode ser excluido pois há registros relacionados ao mesmo";
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

        private string ValidaProdutoGrupo(tbProdutoGrupo objProdutoGrupo)
        {
            if (string.IsNullOrEmpty(objProdutoGrupo.pgr_nome) || string.IsNullOrWhiteSpace(objProdutoGrupo.pgr_nome))
                return "O nome deve ser informado.";

            if (_objCtx.tbProdutoGrupo.AsNoTracking().Any(pgr => (pgr.pgr_nome.Equals(objProdutoGrupo.pgr_nome)) && pgr.pgr_codigo != objProdutoGrupo.pgr_codigo))
                return "Já existe produto grupo com esse nome.";

            return objProdutoGrupo.tbProdutoSubGrupo.Count == 0 ? "Não foram informados nenhum sub grupo." : string.Empty;
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}