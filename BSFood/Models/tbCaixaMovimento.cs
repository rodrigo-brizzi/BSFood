using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbCaixaMovimento
    {
        public int caim_codigo { get; set; }
        public DateTime? caim_data { get; set; }
        public decimal caim_valor { get; set; }
        public string caim_observacao { get; set; }

        public int fpg_codigo { get; set; }
        public virtual tbFormaPagamento tbFormaPagamento { get; set; }

        public int caio_codigo { get; set; }
        public virtual tbCaixaOperacao tbCaixaOperacao { get; set; }

        public int cai_codigo { get; set; }
        public virtual tbCaixa tbCaixa { get; set; }
    }
}
