using BSFoodFramework.DataTransfer;
using System;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace BSFoodFramework.Apoio
{
    public enum enTipoBanco { SqlServer, SqlCompact }
    public enum enStatusTela { EmInclusaoOuAlteracao, Navegacao, Padrao }
    public enum enNavegacao { Proximo, Anterior };
    public enum enOperacao { Inclusao = 1, Alteracao = 2, Exclusao = 3, Outro = 4, Cancelamento = 5, CancelamentoItem = 6, AutorizacaoClienteBloqueado = 7, AutorizacaoClienteNegativado = 8 };
    public enum enOrigemPedido { Entrega = 'E', Comanda = 'C'}
    public enum enStatusPedido { P = 0, E = 1, F = 2, X = 3 }//"P" = Produção, "E" = Entrega, "F" = Finalizado, "X" = Excluido
    public enum enStatusMesa { L = 0, O = 1 }//"L" = Livre, "O" = Ocupada
    public enum enStatusCaixa { Aberto = 0, Fechado = 1 }
    public enum enFormaCobranca { V = 0, P = 1 } //"V" = Vista, "P" = Prazo
    
    public static class Util
    {

        #region Propriedades e métodos para uso geral

        public static ConfigLocal objConfigLocal { get; set; }

        public static ConfigStorage objConfigStorage { get; set; }

        public static GerenciaCupom objGerenciaCupom { get; set; }

        public static string strIp
        {
            get
            {
                IPHostEntry objHost;
                string strLocalIP = string.Empty;
                objHost = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in objHost.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        strLocalIP = ip.ToString();
                        break;
                    }
                }
                return strLocalIP;
            }
        }

        //public static void FecharSistema()
        //{
        //    Application.Current.Shutdown();
        //    Environment.Exit(0);
        //}

        public static MessageBoxImage GetMessageImage(int intCodigo)
        {
            string strNomeEnumIcone = Enum.GetName(typeof(MessageBoxImage), intCodigo);
            return (MessageBoxImage)Enum.Parse(typeof(MessageBoxImage), strNomeEnumIcone, true);
        }

        public static string RetiraCaracterEspecial(string strTexto)
        {
            string strRetorno = string.Empty;
            strRetorno = strTexto.Replace("´", " ");
            strRetorno = strTexto.Replace("`", " ");
            strRetorno = strTexto.Replace("^", " ");
            strRetorno = strTexto.Replace("~", " ");
            strRetorno = strTexto.Replace("'", " ");
            strRetorno = strRetorno.Replace("&", "E");
            strRetorno = strRetorno.Replace("ç", "c");
            strRetorno = strRetorno.Replace("Ç", "C");
            strRetorno = strRetorno.Replace("º", "ro");
            strRetorno = strRetorno.Replace("<n>", "");
            strRetorno = strRetorno.Replace("</n>", "");
            return strRetorno;
        }

        #endregion Geral



        #region Banco de Dados

        /// <summary>
        /// Usuário padrão utilizado para acesso ao banco
        /// </summary>
        public static string strUsuarioBanco { get { return "sa"; } }

        /// <summary>
        /// Senha do banco
        /// </summary>
        public static string strSenhaBanco { get { return "developer"; } }

        /// <summary>
        /// Método para criar a string de conexão para o banco
        /// </summary>
        public static DbConnection RetornaStringConexao()
        {
            DbConnection objRetorno = new SqlConnection();
            try
            {
                if (Util.objConfigLocal == null)
                    Util.CarregarConfiguracao();
                
                SqlConnectionStringBuilder sqlBuilder;
                if (Util.objConfigLocal.TipoBanco == enTipoBanco.SqlServer)
                {
                    sqlBuilder = new SqlConnectionStringBuilder
                    {
                        DataSource = Util.objConfigLocal.strEnderecoBanco,
                        InitialCatalog = Util.objConfigLocal.strNomeBanco,
                        UserID = Util.strUsuarioBanco,
                        Password = Util.strSenhaBanco,
                        PersistSecurityInfo = true,
                        IntegratedSecurity = false,
                        MultipleActiveResultSets = true
                    };
                    objRetorno = new SqlConnection(sqlBuilder.ToString());
                }
                else if (Util.objConfigLocal.TipoBanco == enTipoBanco.SqlCompact)
                {
                    sqlBuilder = new SqlConnectionStringBuilder
                    {
                        DataSource = @"|DataDirectory|\BSFoodDb.sdf",
                        Password = Util.strSenhaBanco,
                        PersistSecurityInfo = true
                    };
                    objRetorno = new SqlCeConnection(sqlBuilder.ToString());
                }                
            }
            catch (Exception ex)
            {
                LogErro(ex);
                throw;
            }
            return objRetorno;
        }

        #endregion Banco de Dados

        

        #region Configuração

        public static void CarregarConfiguracao()
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ConfigLocal.xml"))
                {
                    string strArquivo;
                    using (StreamReader sr = new StreamReader(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "ConfigLocal.xml", FileMode.Open)))
                    {
                        strArquivo = sr.ReadToEnd();
                    }
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigLocal));
                    MemoryStream memStream = new MemoryStream(Encoding.Default.GetBytes(strArquivo));
                    Util.objConfigLocal = (ConfigLocal)xmlSerializer.Deserialize(memStream);
                    memStream.Close();
                }
                else
                {
                    Util.objConfigLocal = new ConfigLocal();
                    Util.objConfigLocal.blnSqlCompact = true;
                    Util.objConfigLocal.blnSqlServer = false;
                    Util.objConfigLocal.strEnderecoBanco = @".\SqlExpress";
                    Util.objConfigLocal.strNomeBanco = "BSFOOD_DB";
                    XmlSerializer objSerializer = new XmlSerializer(typeof(ConfigLocal));
                    FileStream objFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "ConfigLocal.xml", FileMode.Create);
                    objSerializer.Serialize(objFileStream, Util.objConfigLocal);
                    objFileStream.Close();
                }
            }
            catch(Exception ex)
            {
                Util.LogErro(ex);
            }
        }

        public static bool SalvarConfiguracao(ConfigLocal _objConfigLocal, out string strMensagem)
        {
            bool blnRetorno = false;
            strMensagem = string.Empty;
            try
            {
                if (_objConfigLocal.blnSqlServer)
                {
                    if (string.IsNullOrWhiteSpace(_objConfigLocal.strEnderecoBanco))
                        strMensagem = "Informe o endereço do banco de dados";
                    if (string.IsNullOrWhiteSpace(_objConfigLocal.strNomeBanco))
                        strMensagem = "Informe o nome do banco de dados";
                }

                if (strMensagem == string.Empty)
                {
                    XmlSerializer objSerializer = new XmlSerializer(typeof(ConfigLocal));
                    FileStream objFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "ConfigLocal.xml", FileMode.Create);
                    objSerializer.Serialize(objFileStream, _objConfigLocal);
                    objFileStream.Close();
                    blnRetorno = true;
                }
                else
                    blnRetorno = false;
            }
            catch (Exception ex)
            {
                LogErro(ex);
                blnRetorno = false;
            }
            return blnRetorno;
        }

        /// <summary>
        /// Metodo que retorna a versão do app em execução
        /// </summary>
        /// <returns>string contendo a versao completa, ex: 1.0.0.1</returns>
        public static string RetornaVersao()
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();
            string strVersaoClient = objAssembly.FullName.Split(',')[1];
            return strVersaoClient.Replace("Version=", "");
        }

        #endregion Configuração



        #region Log

        /// <summary>
        /// Método criar um log do debug para registrar dados da passagem do ponteiro
        /// </summary>
        /// <param name="strMsg">Mensagem a ser salva no arquivo do tipo "string"</param>
        public static void LogDebug(string strMsg)
        {
            try
            {
                //Salva o log de Debug no arquivo txt na pasta do sistema
                string strNomeArquivo = "logDebug.txt";
                string strPath = AppDomain.CurrentDomain.BaseDirectory + @"\" + strNomeArquivo;
                if (File.Exists(strPath))
                {
                    FileInfo fiArquivo = new FileInfo(strPath);
                    if (fiArquivo.Length > 500000)
                    {
                        File.Delete(strPath);
                        File.AppendAllText(strPath, DateTime.Now.ToString() + " " + strMsg);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(strPath))
                        {
                            sw.WriteLine(DateTime.Now.ToString() + " " + strMsg);
                        }
                    }
                }
                else
                    File.AppendAllText(strPath, DateTime.Now.ToString() + " " + strMsg);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método para tratamento de excessão (erros) da aplicação
        /// </summary>
        /// <param name="strMsg">Mensagem de erro do tipo "string"</param>
        public static void LogErro(Exception ex)
        {
            try
            {
                string strMsg = ex.ToString();
                DetalhaErro(ref strMsg, ex);

                string strNomeArquivo = "log.txt";
                string strPath = AppDomain.CurrentDomain.BaseDirectory + strNomeArquivo;
                if (File.Exists(strPath))
                {
                    FileInfo fiArquivo = new FileInfo(strPath);
                    if (fiArquivo.Length > 10000)
                    {
                        File.Delete(strPath);
                        File.AppendAllText(strPath, DateTime.Now.ToString() + " " + strMsg);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(strPath))
                        {
                            sw.WriteLine(DateTime.Now.ToString() + " " + strMsg);
                        }
                    }
                }
                else
                    File.AppendAllText(strPath, DateTime.Now.ToString() + " " + strMsg);

                //envia o log para o servidor da global ou por e-mail
                //http://link4-glbautomacao.no-ip.org/SiggaWebService/svcGlobal.svc

                using (System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog())
                {
                    appLog.Source = "BSFood";
                    appLog.WriteEntry(strMsg, EventLogEntryType.Error);
                }

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método que vasculha a pilha de erro para capturar os detalhes da mensagem
        /// </summary>
        /// <param name="strMsg">string com a mensagem de erro padrão</param>
        /// <param name="ex">objeto do tipo Exception para ser vasculhado</param>
        private static void DetalhaErro(ref string strMsg, Exception ex)
        {
            if (ex.InnerException != null)
            {
                strMsg += "\n InnerException:\n " + ex.InnerException.ToString();
            }
            if (ex is DbEntityValidationException)
            {
                DbEntityValidationException entityEx = (DbEntityValidationException)ex;
                foreach (var valError in entityEx.EntityValidationErrors)
                {
                    foreach (var item in valError.ValidationErrors)
                    {
                        strMsg += "\nEntity Error: " + item.ErrorMessage;
                    }
                }
            }

            if (ex.InnerException != null)
            {
                DetalhaErro(ref strMsg, ex.InnerException);
            }
        }

        #endregion Log
    }
}
