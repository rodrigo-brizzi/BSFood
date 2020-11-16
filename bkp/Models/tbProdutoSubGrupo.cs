using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbProdutoSubGrupo
    {
        public int psgr_codigo { get; set; }
        public string psgr_nome { get; set; }
        public bool psgr_exibeRelatorio { get; set; }

        public int pgr_codigo { get; set; }
        public virtual tbProdutoGrupo tbProdutoGrupo { get; set; }

        public virtual ICollection<tbProduto> tbProduto { get; set; }
    }
}
