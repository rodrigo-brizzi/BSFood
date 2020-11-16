using BSFoodFramework.DataAccess;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;

namespace BSFoodFramework.Apoio
{
    public class GerenciaTransacao
    {
        public GerenciaTransacao(ref EFContexto _objCxt)
        {
            this.objCxt = _objCxt;
        }

        /// <summary>
        /// Objeto do tipo "Conexao" que será manipulado pela classe
        /// </summary>
        private EFContexto objCxt { get; set; }

        /// <summary>
        /// Objeto do tipo "DbContextTransaction" em que é transação do contexto é apontada
        /// </summary>
        private DbContextTransaction transacao;

        /// <summary>
        /// Indica se uma transação está sendo executada
        /// </summary>
        private bool blnEmTransacao = false;

        /// <summary>
        /// Indica o método que deu início a transação, pois apenas ele terá direito de dar Rollback e Commit
        /// </summary>
        private MethodBase metodoOrigemTransacao;

        /// <summary>
        /// Verifica se o contexto atual não tem uma transação aberta, caso não tenha, inicia uma.
        /// </summary>
        /// <returns>Retorna "True" se a transação foi iniciada, ou falso caso não tenha sido pois já há uma transação aberta</returns>
        public bool TryBeginTransaction()
        {
            try
            {
                if (!blnEmTransacao)
                {
                    transacao = this.objCxt.Database.BeginTransaction();
                    blnEmTransacao = true;
                    metodoOrigemTransacao = GetMethod();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
            }
            return false;
        }

        /// <summary>
        /// Verifica se o contexto atual tem uma transação aberta, então verifica se o método que está tentando fazer Rollback foi o método que iniciou a transação
        /// </summary>
        /// <returns>Retorna "True" se a transação foi desfeita, ou falso caso não tenha sido pois não há transação aberta ou quem está tentando desfazer não deu inicio a mesma</returns>
        public bool TryRollBackTransaction()
        {
            try
            {
                if (blnEmTransacao)
                {
                    if (GetMethod() == metodoOrigemTransacao)
                    {
                        transacao.Rollback();
                        transacao.Dispose();
                        blnEmTransacao = false;
                        metodoOrigemTransacao = null;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica se o contexto atual tem uma transação aberta, então verifica se o método que está tentando dar Commit foi o método que iniciou a transação
        /// </summary>
        /// <returns>Retorna "True" se a transação foi comitada, ou falso caso não tenha sido pois não há transação aberta ou quem está tentando dar commit não deu inicio a mesma</returns>
        public bool TryCommitTransaction()
        {
            try
            {
                if (blnEmTransacao)
                {
                    if (GetMethod() == metodoOrigemTransacao)
                    {
                        transacao.Commit();
                        transacao.Dispose();
                        blnEmTransacao = false;
                        metodoOrigemTransacao = null;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogErro(ex);
            }
            return false;
        }

        /// <summary>
        /// Navega na pilha de execução, e retorna o método que está na posição 2 da pilha, que é o método que chamou a transação
        /// </summary>
        /// <returns>Retorno o método que tentou iniciar/rollback/commit uma transação</returns>
        private MethodBase GetMethod()
        {
            StackTrace st = new StackTrace();
            // recupera o método 2 níveis acima da pilha: 
            //nível 0 = GetMethod; nivel 1 = TryBeginTransaction ou TryCommitTransaction; nivel 2 = método de classe da BLL
            StackFrame sf = st.GetFrame(2);
            MethodBase currentMethodName = sf.GetMethod();
            return currentMethodName;
        }

        public void Dispose()
        {
            if (transacao != null)
                transacao.Dispose();
        }
    }
}
