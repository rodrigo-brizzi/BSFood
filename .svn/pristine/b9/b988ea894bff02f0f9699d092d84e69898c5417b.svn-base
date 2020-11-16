using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbMenu
    {
        public int men_codigo { get; set; }
        public int? men_nivel { get; set; }
        public string men_nomeControle { get; set; }
        public string men_cabecalho { get; set; }
        public string men_ordem { get; set; }
        public string men_imagem { get; set; }
        public bool men_status { get; set; }

        public int? men_codigoPai { get; set; }
        public virtual tbMenu tbMenuPai { get; set; }

        public virtual ICollection<tbMenu> tbMenuFilho { get; set; }
        public virtual ICollection<tbPerfilAcessoMenu> tbPerfilAcessoMenu { get; set; }
    }
}
