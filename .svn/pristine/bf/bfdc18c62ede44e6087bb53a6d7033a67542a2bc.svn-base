using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbBairroConfig : EntityTypeConfiguration<tbBairro>
    {
        public tbBairroConfig()
        {
            this.ToTable("tbBairro");
            this.HasKey(e => e.bai_codigo);

            this.Property(e => e.bai_codigo).HasColumnName("bai_codigo");
            this.Property(e => e.bai_nome).HasColumnName("bai_nome").HasMaxLength(100);
            this.Property(e => e.bai_taxaEntrega).HasColumnName("bai_taxaEntrega");
            this.Property(e => e.bai_realizaEntrega).HasColumnName("bai_realizaEntrega");
        }
    }
}
