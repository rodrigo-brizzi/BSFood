using System.Collections.Generic;

namespace BSFoodFramework.Models
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
