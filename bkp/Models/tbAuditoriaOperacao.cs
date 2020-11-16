using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbAuditoriaOperacao
    {
        public int audo_codigo { get; set; }
        public string audo_descricao { get; set; }

        public virtual ICollection<tbAuditoria> tbAuditoria { get; set; }
    }
}
