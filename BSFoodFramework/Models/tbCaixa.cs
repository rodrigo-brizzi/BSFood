using System;
using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbCaixa
    {
        public int cai_codigo { get; set; }
        public DateTime? cai_dataAbertura { get; set; }
        public DateTime? cai_dataFechamento { get; set; }
        public string cai_observacao { get; set; }
        public int cai_ordemPedido { get; set; }
        
        public int fun_codigo { get; set; }
        public virtual tbFuncionario tbFuncionario { get; set; }

        public virtual ICollection<tbCaixaMovimento> tbCaixaMovimento { get; set; }
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        public virtual ICollection<tbVenda> tbVenda { get; set; }
    }
}
