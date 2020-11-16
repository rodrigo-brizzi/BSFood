using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbPedidoProduto
    {
        public int ppr_codigo { get; set; }
        public int ppr_sequencia { get; set; }
        public decimal ppr_quantidade { get; set; }
        public decimal ppr_valorUnitario { get; set; }
        public decimal ppr_valorTotal { get; set; }
        public string ppr_observacao { get; set; }
        public bool ppr_impresso { get; set; }
        public string ppr_descricao { get; set; }

        public int pro_codigo { get; set; }
        public virtual tbProduto tbProduto { get; set; }

        public int ped_codigo { get; set; }
        public virtual tbPedido tbPedido { get; set; }
    }
}
