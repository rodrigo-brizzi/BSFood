﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.Models
{
    public class tbMesa
    {
        public int mes_codigo { get; set; }
        public string mes_descricao { get; set; }
        public int mes_numero { get; set; }
        public string mes_status { get; set; }
        public string mes_imagemLivre { get; set; }
        public string mes_imagemOcupada { get; set; }
        public string mes_imagemConta { get; set; }
        public string mes_terminal { get; set; }

        public int? ped_codigo { get; set; }
        public virtual tbPedido tbPedido { get; set; }
    }
}