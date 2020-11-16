﻿using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbCidade
    {
        public int cid_codigo { get; set; }
        public string cid_ibge { get; set; }
        public string cid_nome { get; set; }

        public int est_codigo { get; set; }
        public virtual tbEstado tbEstado { get; set; }

        public virtual ICollection<tbClienteEndereco> tbClienteEndereco { get; set; }
        public virtual ICollection<tbEmpresa> tbEmpresa { get; set; }
        public virtual ICollection<tbFuncionario> tbFuncionario { get; set; }
        public virtual ICollection<tbFornecedor> tbFornecedor { get; set; }
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        
    }
}
