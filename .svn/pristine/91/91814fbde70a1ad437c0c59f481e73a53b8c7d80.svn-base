using BSFoodFramework.Apoio;
using System.Xml.Serialization;

namespace BSFoodFramework.DataTransfer
{
    public class ConfigLocal
    {
        public bool blnSqlServer { get; set; }

        public bool blnSqlCompact { get; set; }

        public string strEnderecoBanco { get; set; }

        public string strNomeBanco { get; set; }

        public string strTerminal { get; set; }

        [XmlIgnore()]
        public enTipoBanco TipoBanco
        {
            get
            {
                if (blnSqlServer)
                    return enTipoBanco.SqlServer;
                else
                    return enTipoBanco.SqlCompact;
            }
        }
    }
}
