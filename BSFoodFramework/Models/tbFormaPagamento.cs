using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbFormaPagamento
    {
        public int fpg_codigo { get; set; }
        public string fpg_descricao { get; set; }
        public int fpg_numero { get; set; }
        public bool fpg_status { get; set; }
        public bool fpg_tef { get; set; }
        public string fpg_diretorioTef { get; set; }
        public int fpg_qtdeViasTef { get; set; }
        public bool fpg_exibePdv { get; set; }
        public bool fpg_imprimeComprovante { get; set; }
        public int fpg_qtdeViasComprovante { get; set; }

        public int fpt_codigo { get; set; }
        public virtual tbFormaPagamentoTipo tbFormaPagamentoTipo { get; set; }

        public virtual ICollection<tbCaixaMovimento> tbCaixaMovimento { get; set; }
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        public virtual ICollection<tbConfiguracao> tbConfiguracao { get; set; }
    }
}
