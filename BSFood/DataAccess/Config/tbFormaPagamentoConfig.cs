using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbFormaPagamentoConfig : EntityTypeConfiguration<tbFormaPagamento>
    {
        public tbFormaPagamentoConfig()
        {
            this.ToTable("tbFormaPagamento");
            this.HasKey(e => e.fpg_codigo);

            this.Property(e => e.fpg_codigo).HasColumnName("fpg_codigo");
            this.Property(e => e.fpg_descricao).HasColumnName("fpg_descricao").HasMaxLength(100);
            this.Property(e => e.fpg_cobranca).HasColumnName("fpg_cobranca").HasMaxLength(1);
            this.Property(e => e.fpg_codigoSat).HasColumnName("fpg_codigoSat").HasMaxLength(2);
        }
    }
}
