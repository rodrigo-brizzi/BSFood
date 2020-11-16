using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbClienteGrupo
    {
        public int cgr_codigo { get; set; }
        public string cgr_nome { get; set; }
        public int cgr_fechamento { get; set; }
        public int cgr_vencimento { get; set; }

        public virtual ICollection<tbCliente> tbCliente { get; set; }
        public virtual ICollection<tbConfiguracao> tbConfiguracao { get; set; }
    }
}
