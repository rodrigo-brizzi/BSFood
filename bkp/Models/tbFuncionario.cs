using System.Collections.Generic;

namespace BSFoodFramework.Models
{
    public class tbFuncionario
    {
        public int fun_codigo { get; set; }
        public string fun_nome { get; set; }
        public string fun_login { get; set; }
        public string fun_senha { get; set; }
        public string fun_logradouro { get; set; }
        public string fun_numero { get; set; }
        public string fun_bairro { get; set; }
        public string fun_cep { get; set; }
        public string fun_complemento { get; set; }
        public string fun_telefone { get; set; }
        public string fun_celular { get; set; }

        public int cid_codigo { get; set; }
        public virtual tbCidade tbCidade { get; set; }

        public int est_codigo { get; set; }
        public virtual tbEstado tbEstado { get; set; }

        public int pac_codigo { get; set; }
        public virtual tbPerfilAcesso tbPerfilAcesso { get; set; }

        public virtual ICollection<tbAuditoria> tbAuditoria { get; set; }
        public virtual ICollection<tbCaixa> tbCaixa { get; set; }
        public virtual ICollection<tbPedido> tbPedido { get; set; }
        public virtual ICollection<tbPedido> tbPedidoEntregador { get; set; }
        public virtual ICollection<tbVenda> tbVenda { get; set; }
    }
}
