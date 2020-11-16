namespace BSFoodFramework.Models
{
    public class tbClienteEndereco
    {
        public int cen_codigo { get; set; }
        public string cen_logradouro { get; set; }
        public string cen_numero { get; set; }
        public string cen_cep { get; set; }
        public string cen_complemento { get; set; }

        public int bai_codigo { get; set; }
        public virtual tbBairro tbBairro { get; set; }

        public int cid_codigo { get; set; }
        public virtual tbCidade tbCidade { get; set; }

        public int est_codigo { get; set; }
        public virtual tbEstado tbEstado { get; set; }

        public int cli_codigo { get; set; }
        public virtual tbCliente tbCliente { get; set; }
    }
}
