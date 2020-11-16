using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbVendaProduto
    {
        public int vpr_codigo { get; set; }
        public int vpr_sequencia { get; set; }
        public decimal vpr_quantidade { get; set; }
        public decimal vpr_valorUnitario { get; set; }
        public decimal vpr_valorTotal { get; set; }
        public bool vpr_cancelado { get; set; }
        public string vpr_descricao { get; set; }
        public string vpr_unidade { get; set; }

        public int pro_codigo { get; set; }
        public virtual tbProduto tbProduto { get; set; }

        public int vec_codigo { get; set; }
        public virtual tbVenda tbVenda { get; set; }
    }
}
