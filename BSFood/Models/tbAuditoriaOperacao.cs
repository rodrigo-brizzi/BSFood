using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbAuditoriaOperacao
    {
        public int audo_codigo { get; set; }
        public string audo_descricao { get; set; }

        public virtual ICollection<tbAuditoria> tbAuditoria { get; set; }
    }
}
