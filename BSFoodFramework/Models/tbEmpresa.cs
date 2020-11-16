namespace BSFoodFramework.Models
{
    public class tbEmpresa
    {
        public int emp_codigo { get; set; }
        public string emp_nome { get; set; }
        public string emp_nomeFantasia { get; set; }
        public string emp_cnpj { get; set; }
        public string emp_ie { get; set; }
        public string emp_im { get; set; }
        public string emp_assinaturaSat { get; set; }
        public string emp_logradouro { get; set; }
        public string emp_numero { get; set; }
        public string emp_bairro { get; set; }
        public string emp_cep { get; set; }
        public string emp_complemento { get; set; }
        public string emp_telefone { get; set; }
        public string emp_fax { get; set; }
        public byte[] emp_logo { get; set; }
        public string emp_logoFormato { get; set; }

        public int cid_codigo { get; set; }
        public virtual tbCidade tbCidade { get; set; }

        public int est_codigo { get; set; }
        public virtual tbEstado tbEstado { get; set; }
    }
}
