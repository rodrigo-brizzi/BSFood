using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbPerfilAcesso
    {
        public int pac_codigo { get; set; }
        public string pac_descricao { get; set; }
        public bool pac_permiteDesconto { get; set; }
        public bool pac_permiteCancelarItem { get; set; }
        public bool pac_permiteCancelarVenda { get; set; }
        public bool pac_permiteVendaClienteBloqueado { get; set; }
        public bool pac_permiteVendaClienteNegativo { get; set; }

        public virtual ICollection<tbPerfilAcessoMenu> tbPerfilAcessoMenu { get; set; }
        public virtual ICollection<tbFuncionario> tbFuncionario { get; set; }
    }
}
