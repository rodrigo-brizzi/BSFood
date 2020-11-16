using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbFormaPagamentoConfig : EntityTypeConfiguration<tbFormaPagamento>
    {
        public tbFormaPagamentoConfig()
        {
            this.ToTable("tbFormaPagamento");
            this.HasKey(e => e.fpg_codigo);

            this.Property(e => e.fpg_codigo).HasColumnName("fpg_codigo");
            this.Property(e => e.fpg_descricao).HasColumnName("fpg_descricao").HasMaxLength(100);
            this.Property(e => e.fpg_numero).HasColumnName("fpg_numero");
            this.Property(e => e.fpg_status).HasColumnName("fpg_status");
            this.Property(e => e.fpg_tef).HasColumnName("fpg_tef");
            this.Property(e => e.fpg_diretorioTef).HasColumnName("fpg_diretorioTef").HasMaxLength(100);
            this.Property(e => e.fpg_qtdeViasTef).HasColumnName("fpg_qtdeViasTef");
            this.Property(e => e.fpg_exibePdv).HasColumnName("fpg_exibePdv");
            this.Property(e => e.fpg_imprimeComprovante).HasColumnName("fpg_imprimeComprovante");
            this.Property(e => e.fpg_qtdeViasComprovante).HasColumnName("fpg_qtdeViasComprovante");
            this.HasRequired(e => e.tbFormaPagamentoTipo).WithMany(e => e.tbFormaPagamento).HasForeignKey(e => e.fpt_codigo);
            this.Property(e => e.fpt_codigo).HasColumnName("fpt_codigo");
        }
    }
}
