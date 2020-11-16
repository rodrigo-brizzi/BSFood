using System;

namespace BSFoodFramework.Models
{
    public class tbAuditoria
    {
        public int aud_codigo { get; set; }
        public int aud_codigoRegistro { get; set; }
        public DateTime? aud_data { get; set; }
        public string aud_nomeTabela { get; set; }
        public string aud_login { get; set; }

        public int fun_codigo { get; set; }
        public virtual tbFuncionario tbFuncionario { get; set; }

        public int audo_codigo { get; set; }
        public virtual tbAuditoriaOperacao tbAuditoriaOperacao { get; set; }
    }
}
