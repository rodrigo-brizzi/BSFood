namespace BSFoodFramework.Models
{
    public class tbClienteTelefone
    {
        public int ctl_codigo { get; set; }
        public string ctl_numero { get; set; }

        public int cli_codigo { get; set; }
        public virtual tbCliente tbCliente { get; set; }
    }
}
