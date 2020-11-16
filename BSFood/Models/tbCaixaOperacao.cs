using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbCaixaOperacao
    {
        public int caio_codigo { get; set; }
        public string caio_descricao { get; set; }
        public string caio_tipoOperacao { get; set; }

        public virtual ICollection<tbCaixaMovimento> tbCaixaMovimento { get; set; }
    }
}
