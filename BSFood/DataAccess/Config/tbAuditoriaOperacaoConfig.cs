using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbAuditoriaOperacaoConfig : EntityTypeConfiguration<tbAuditoriaOperacao>
    {
        public tbAuditoriaOperacaoConfig()
        {
            this.ToTable("tbAuditoriaOperacao");
            this.HasKey(e => e.audo_codigo);

            this.Property(e => e.audo_codigo).HasColumnName("audo_codigo");
            this.Property(e => e.audo_descricao).HasColumnName("audo_descricao").HasMaxLength(50);
        }
    }
}
