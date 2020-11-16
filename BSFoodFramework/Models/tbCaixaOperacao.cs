using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbCaixaOperacao
    {
        public int caio_codigo { get; set; }
        public string caio_descricao { get; set; }
        public string caio_tipoOperacao { get; set; }

        public virtual ICollection<tbCaixaMovimento> tbCaixaMovimento { get; set; }
    }
}
