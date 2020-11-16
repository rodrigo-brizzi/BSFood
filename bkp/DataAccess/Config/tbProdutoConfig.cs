using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbProdutoConfig : EntityTypeConfiguration<tbProduto>
    {
        public tbProdutoConfig()
        {
            this.ToTable("tbProduto");
            this.HasKey(e => e.pro_codigo);

            this.Property(e => e.pro_codigo).HasColumnName("pro_codigo");
            this.Property(e => e.pro_nome).HasColumnName("pro_nome").HasMaxLength(100);
            this.Property(e => e.pro_estoqueNegativo).HasColumnName("pro_estoqueNegativo");
            this.Property(e => e.pro_precoEntrega).HasColumnName("pro_precoEntrega");
            this.Property(e => e.pro_precoBalcao).HasColumnName("pro_precoBalcao");
            this.Property(e => e.pro_observacao).HasColumnName("pro_observacao").HasMaxLength(250);
            this.HasRequired(e => e.tbProdutoGrupo).WithMany(e => e.tbProduto).HasForeignKey(e => e.pgr_codigo);
            this.Property(e => e.pgr_codigo).HasColumnName("pgr_codigo");
            this.HasRequired(e => e.tbProdutoSubGrupo).WithMany(e => e.tbProduto).HasForeignKey(e => e.psgr_codigo);
            this.Property(e => e.psgr_codigo).HasColumnName("psgr_codigo");
        }
    }
}
