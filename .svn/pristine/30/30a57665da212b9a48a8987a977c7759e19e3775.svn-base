using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbAuditoriaConfig : EntityTypeConfiguration<tbAuditoria>
    {
        public tbAuditoriaConfig()
        {
            this.ToTable("tbAuditoria");
            this.HasKey(e => e.aud_codigo);

            this.Property(e => e.aud_codigo).HasColumnName("aud_codigo");
            this.Property(e => e.aud_codigoRegistro).HasColumnName("aud_codigoRegistro");
            this.Property(e => e.aud_data).HasColumnName("aud_data");
            this.Property(e => e.aud_nomeTabela).HasColumnName("aud_nomeTabela").HasMaxLength(50);
            this.Property(e => e.aud_login).HasColumnName("aud_login").HasMaxLength(20);
            this.HasRequired(e => e.tbFuncionario).WithMany(e => e.tbAuditoria).HasForeignKey(e => e.fun_codigo);
            this.Property(e => e.fun_codigo).HasColumnName("fun_codigo");
            this.HasRequired(e => e.tbAuditoriaOperacao).WithMany(e => e.tbAuditoria).HasForeignKey(e => e.audo_codigo);
            this.Property(e => e.audo_codigo).HasColumnName("audo_codigo");

        }
    }
}
