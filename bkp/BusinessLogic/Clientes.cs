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
    public class Clientes : IClientes, IDisposable
    {
        private readonly bool _blnFecharCon;
        private EFContexto _objCtx;
        private GerenciaTransacao _objTransacao;

        public Clientes()
        {
            _objCtx = new EFContexto();
            _objTransacao = new GerenciaTransacao(ref _objCtx);
            _blnFecharCon = true;
        }

        public Clientes(ref EFContexto objCtx, ref GerenciaTransacao objTransacao)
        {
            _objCtx = objCtx;
            _objTransacao = objTransacao;
            _blnFecharCon = false;
        }

        public Retorno RetornaCliente(int intCodigo, enNavegacao? enDirecao)
        {
            var objRetorno = new Retorno();
            try
            {
                tbCliente objCliente = null;
                if (enDirecao == null)
                    objCliente = _objCtx.tbCliente.Include(cen => cen.tbClienteEndereco.Select(bai => bai.tbBairro))
                                                  .Include(ctl => ctl.tbClienteTelefone).Include(cgr => cgr.tbClienteGrupo)
                                                  .AsNoTracking()
                                                  .FirstOrDefault(cli => cli.cli_codigo == intCodigo || cli.tbClienteTelefone.Any(ctl => ctl.ctl_numero.Equals(intCodigo.ToString())));
                if (enDirecao == enNavegacao.Proximo)
                    objCliente = _objCtx.tbCliente.Include(cen => cen.tbClienteEndereco.Select(bai => bai.tbBairro))
                                                      .Include(ctl => ctl.tbClienteTelefone)
                                                      .Include(cgr => cgr.tbClienteGrupo).AsNoTracking()
                                                      .Where(cli => cli.cli_codigo > intCodigo)
                                                      .OrderBy(cli => cli.cli_codigo).FirstOrDefault();
                if (enDirecao == enNavegacao.Anterior)
                    objCliente = _objCtx.tbCliente.Include(cen => cen.tbClienteEndereco.Select(bai => bai.tbBairro))
                                                      .Include(ctl => ctl.tbClienteTelefone)
                                                      .Include(cgr => cgr.tbClienteGrupo).AsNoTracking()
                                                      .Where(cli => cli.cli_codigo < intCodigo)
                                                      .OrderByDescending(cli => cli.cli_codigo).FirstOrDefault();
                if (objCliente != null)
                {
                    objRetorno.intCodigoErro = 0;
                    objRetorno.objRetorno = objCliente;
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

        public Retorno RetornaListaCliente(string strCodigo, string strNome, string strTelefone, int intSkip, int intTake)
        {
            var objRetorno = new Retorno();
            try
            {
                var arrCliente = _objCtx.tbCliente.AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(strCodigo))
                {
                    int intCodigo = Convert.ToInt32(strCodigo);
                    arrCliente = arrCliente.Where(cli => cli.cli_codigo == intCodigo).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(strNome))
                    arrCliente = arrCliente.Where(cli => cli.cli_nome.Contains(strNome)).AsQueryable();
                objRetorno.intCodigoErro = 0;
                if (intSkip == 0)
                    objRetorno.intQtdeRegistro = arrCliente.Count();
                objRetorno.objRetorno = arrCliente.OrderBy(cli => cli.cli_codigo).Skip(intSkip).Take(intTake).ToList();
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

        public Retorno SalvarCliente(tbCliente objCliente, int intFunCodigo)
        {
            var objRetorno = new Retorno();
            var strValidacao = ValidaCliente(objCliente);
            try
            {
                if (strValidacao == string.Empty)
                {
                    objCliente.tbClienteGrupo = null;
                    foreach (var objClienteEndereco in objCliente.tbClienteEndereco)
                    {
                        objClienteEndereco.tbBairro = null;
                        objClienteEndereco.tbCliente = null;
                    }
                    foreach (var objClienteTelefone in objCliente.tbClienteTelefone)
                        objClienteTelefone.tbCliente = null;
                    enOperacao enTipoOperacao;
                    if (objCliente.cli_codigo == 0)
                    {
                        enTipoOperacao = enOperacao.Inclusao;
                        _objCtx.tbCliente.Add(objCliente);
                    }
                    else
                    {
                        enTipoOperacao = enOperacao.Alteracao;
                        var objClienteContexto = _objCtx.tbCliente.Include(cen => cen.tbClienteEndereco)
                                                                  .Include(ctl => ctl.tbClienteTelefone).FirstOrDefault(cli => cli.cli_codigo == objCliente.cli_codigo);
                        if (objClienteContexto != null)
                        {
                            _objCtx.tbClienteEndereco.RemoveRange(objClienteContexto.tbClienteEndereco);
                            _objCtx.tbClienteTelefone.RemoveRange(objClienteContexto.tbClienteTelefone);
                            _objCtx.Entry(objClienteContexto).CurrentValues.SetValues(objCliente);
                        }

                        foreach (var objItemEndereco in objCliente.tbClienteEndereco)
                        {
                            objItemEndereco.cli_codigo = objCliente.cli_codigo;
                            _objCtx.tbClienteEndereco.Add(objItemEndereco);
                        }
                        foreach (var objItemTelefone in objCliente.tbClienteTelefone)
                        {
                            objItemTelefone.cli_codigo = objCliente.cli_codigo;
                            _objCtx.tbClienteTelefone.Add(objItemTelefone);
                        }
                    }
                    _objCtx.SaveChanges();
                    using (var objBll = new Auditoria(ref _objCtx, ref _objTransacao))
                        objBll.SalvarAuditoria(objCliente.cli_codigo, enTipoOperacao, objCliente, intFunCodigo);
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

        public Retorno ExcluirCliente(int intCodigo)
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
                            var objCliente = objContexto.tbCliente.Include(cen => cen.tbClienteEndereco)
                                                                  .Include(ctl => ctl.tbClienteTelefone).FirstOrDefault(cli => cli.cli_codigo == intCodigo);
                            if (objCliente != null)
                            {
                                objContexto.tbClienteEndereco.RemoveRange(objCliente.tbClienteEndereco);
                                objContexto.tbClienteTelefone.RemoveRange(objCliente.tbClienteTelefone);
                                objContexto.tbCliente.Remove(objCliente);
                                objContexto.SaveChanges();
                                transacao.Commit();

                                objRetorno.intCodigoErro = 0;
                                objRetorno.objRetorno = true;
                            }
                            else
                            {
                                objRetorno.intCodigoErro = 48;
                                objRetorno.strMsgErro = "Cliente não encontrado para exclusão";
                            }
                        }
                        catch (Exception)
                        {
                            //Se deu erro é porque o perfil tem  registros relacionado
                            transacao.Rollback();
                            objRetorno.intCodigoErro = 48;
                            objRetorno.strMsgErro = "Cliente não pode ser excluido pois há registros relacionados ao mesmo";
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

        private string ValidaCliente(tbCliente objCliente)
        {
            if (string.IsNullOrEmpty(objCliente.cli_nome) || string.IsNullOrWhiteSpace(objCliente.cli_nome))
                return "O nome deve ser informado.";

            if (objCliente.cgr_codigo == 0)
                return "O grupo do cliente deve ser informado.";

            if (_objCtx.tbCliente.AsNoTracking().Any(cli => (cli.cli_nome.Equals(objCliente.cli_nome)) && cli.cli_codigo != objCliente.cli_codigo))
                return "Já existe cliente com esse nome.";

            foreach (var objClienteTelefone in objCliente.tbClienteTelefone)
            {
                if (_objCtx.tbClienteTelefone.AsNoTracking().Any(ctl => (ctl.ctl_numero.Equals(objClienteTelefone.ctl_numero)) && ctl.cli_codigo != objCliente.cli_codigo))
                    return "Já existe cliente com o telefone: " + objClienteTelefone.ctl_numero + ".";
            }

            if (objCliente.tbClienteEndereco.Count == 0)
                return "Não foram informados nenhum endereço.";

            return objCliente.tbClienteTelefone.Count == 0 ? "Não foram informados nenhum telefone." : string.Empty;
        }

        public void Dispose()
        {
            if (!_blnFecharCon) return;
            _objCtx.Dispose();
            _objCtx = null;
        }
    }
}
