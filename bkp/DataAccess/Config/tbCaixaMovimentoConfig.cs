using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbCaixaMovimentoConfig : EntityTypeConfiguration<tbCaixaMovimento>
    {
        public tbCaixaMovimentoConfig()
        {
            this.ToTable("tbCaixaMovimento");
            this.HasKey(e => e.caim_codigo);

            this.Property(e => e.caim_codigo).HasColumnName("caim_codigo");
            this.Property(e => e.caim_data).HasColumnName("caim_data");
            this.Property(e => e.caim_valor).HasColumnName("caim_valor");
            this.Property(e => e.caim_observacao).HasColumnName("caim_observacao").HasMaxLength(250); ;
            this.HasRequired(e => e.tbFormaPagamento).WithMany(e => e.tbCaixaMovimento).HasForeignKey(e => e.fpg_codigo);
            this.Property(e => e.fpg_codigo).HasColumnName("fpg_codigo");
            this.HasRequired(e => e.tbCaixaOperacao).WithMany(e => e.tbCaixaMovimento).HasForeignKey(e => e.caio_codigo);
            this.Property(e => e.caio_codigo).HasColumnName("caio_codigo");
            this.HasRequired(e => e.tbCaixa).WithMany(e => e.tbCaixaMovimento).HasForeignKey(e => e.cai_codigo);
            this.Property(e => e.cai_codigo).HasColumnName("cai_codigo");
        }
    }
}
