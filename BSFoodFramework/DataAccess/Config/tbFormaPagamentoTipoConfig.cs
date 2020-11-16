using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbFormaPagamentoTipoConfig : EntityTypeConfiguration<tbFormaPagamentoTipo>
    {
        public tbFormaPagamentoTipoConfig()
        {
            this.ToTable("tbFormaPagamentoTipo");
            this.HasKey(e => e.fpt_codigo);

            this.Property(e => e.fpt_codigo).HasColumnName("fpt_codigo");
            this.Property(e => e.fpt_descricao).HasColumnName("fpt_descricao").HasMaxLength(100);
            this.Property(e => e.fpt_cobranca).HasColumnName("fpt_cobranca").HasMaxLength(1);
            this.Property(e => e.fpt_codigoSat).HasColumnName("fpt_codigoSat").HasMaxLength(2);
        }
    }
}