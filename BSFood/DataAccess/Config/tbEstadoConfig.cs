using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbEstadoConfig : EntityTypeConfiguration<tbEstado>
    {
        public tbEstadoConfig()
        {
            this.ToTable("tbEstado");
            this.HasKey(e => e.est_codigo);

            this.Property(e => e.est_codigo).HasColumnName("est_codigo");
            this.Property(e => e.est_sigla).HasColumnName("est_sigla").HasMaxLength(2);
            this.Property(e => e.est_nome).HasColumnName("est_nome").HasMaxLength(20);
        }
    }
}
