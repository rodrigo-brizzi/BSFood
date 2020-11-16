using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbConfiguracao
    {
        public int cfg_codigo { get; set; }
        public DateTime? cfg_ultimoLogin { get; set; }
        public string cfg_senhaMaster { get; set; }
        public string cfg_cnpjSoftwareHouse { get; set; }
        public string cfg_impressoraEntrega { get; set; }
        public string cfg_impressoraComanda { get; set; }
        public string cfg_impressoraBebida { get; set; }
        public string cfg_impressoraBalcao { get; set; }
        public int cfg_quantidadeMesa { get; set; }

        public int cgr_codigo { get; set; }
        public virtual tbClienteGrupo tbClienteGrupo { get; set; }

        public int cli_codigo { get; set; }
        public virtual tbCliente tbCliente { get; set; }

        public int fpg_codigo { get; set; }
        public virtual tbFormaPagamento tbFormaPagamento { get; set; }
    }
}
