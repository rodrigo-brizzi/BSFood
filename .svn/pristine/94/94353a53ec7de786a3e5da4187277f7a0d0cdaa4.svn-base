using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbCaixaOperacaoConfig : EntityTypeConfiguration<tbCaixaOperacao>
    {
        public tbCaixaOperacaoConfig()
        {
            this.ToTable("tbCaixaOperacao");
            this.HasKey(e => e.caio_codigo);

            this.Property(e => e.caio_codigo).HasColumnName("caio_codigo");
            this.Property(e => e.caio_descricao).HasColumnName("caio_descricao").HasMaxLength(100);
            this.Property(e => e.caio_tipoOperacao).HasColumnName("caio_tipoOperacao").HasMaxLength(1);
        }
    }
}
