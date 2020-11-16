using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbBairro
    {
        public int bai_codigo { get; set; }
        public string bai_nome { get; set; }
        public decimal bai_taxaEntrega { get; set; }
        public bool bai_realizaEntrega { get; set; }

        public virtual ICollection<tbClienteEndereco> tbClienteEndereco { get; set; }
        public virtual ICollection<tbPedido> tbPedido { get; set; }
    }
}
