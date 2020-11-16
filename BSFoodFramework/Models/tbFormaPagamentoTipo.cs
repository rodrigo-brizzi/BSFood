using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbFormaPagamentoTipo
    {
        public int fpt_codigo { get; set; }
        public string fpt_descricao { get; set; }
        public string fpt_cobranca { get; set; }
        public string fpt_codigoSat { get; set; }

        public virtual ICollection<tbFormaPagamento> tbFormaPagamento { get; set; }
    }
}
