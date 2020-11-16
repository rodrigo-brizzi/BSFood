using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbVenda
    {
        public int vec_codigo { get; set; }
        public DateTime? vec_data { get; set; }
        public string vec_cpfCnpj { get; set; }
        public string vec_observacao { get; set; }
        public bool vec_cancelado { get; set; }
        public decimal vec_valorDespesa { get; set; }
        public decimal vec_valorDesconto { get; set; }
        public decimal vec_valorRecebido { get; set; }
        public decimal vec_valorTroco { get; set; }
        public decimal vec_valorTotal { get; set; }

        public int cli_codigo { get; set; }
        public virtual tbCliente tbCliente { get; set; }

        public int fun_codigo { get; set; }
        public virtual tbFuncionario tbFuncionario { get; set; }

        public int cai_codigo { get; set; }
        public virtual tbCaixa tbCaixa { get; set; }

        public int ped_codigo { get; set; }
        public virtual tbPedido tbPedido { get; set; }

        public virtual ICollection<tbVendaProduto> tbVendaProduto { get; set; }
    }
}
