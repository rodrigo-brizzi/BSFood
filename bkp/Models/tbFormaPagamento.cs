using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbFormaPagamento
    {
        public int fpg_codigo { get; set; }
        public string fpg_descricao { get; set; }
        public string fpg_cobranca { get; set; }
        public string fpg_codigoSat { get; set; }

        public virtual ICollection<tbCaixaMovimento> tbCaixaMovimento { get; set; }
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        public virtual ICollection<tbConfiguracao> tbConfiguracao { get; set; }
    }
}
