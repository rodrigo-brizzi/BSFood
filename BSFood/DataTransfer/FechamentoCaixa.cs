using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataTransfer
{
    public class FechamentoCaixa
    {
        public List<FechamentoCaixaForma> arrFechamentoCaixaFormaEntrega { get; set; }
        public List<FechamentoCaixaForma> arrFechamentoCaixaFormaComanda { get; set; }
        public List<tbCaixaMovimento> arrCaixaMovimento { get; set; }
        public decimal decValorFinal { get; set; }
        public string strStatusCaixa { get; set; }
        public tbCaixa objCaixa { get; set; }
        public string strRelatorio { get; set; }
    }
}
