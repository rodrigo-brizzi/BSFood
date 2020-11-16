using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbVendaProdutoConfig : EntityTypeConfiguration<tbVendaProduto>
    {
        public tbVendaProdutoConfig()
        {
            this.ToTable("tbVendaProduto");
            this.HasKey(e => e.vpr_codigo);

            this.Property(e => e.vpr_codigo).HasColumnName("vpr_codigo");
            this.Property(e => e.vpr_sequencia).HasColumnName("vpr_sequencia");
            this.Property(e => e.vpr_quantidade).HasColumnName("vpr_quantidade");
            this.Property(e => e.vpr_valorUnitario).HasColumnName("vpr_valorUnitario");
            this.Property(e => e.vpr_valorTotal).HasColumnName("vpr_valorTotal");
            this.Property(e => e.vpr_cancelado).HasColumnName("vpr_cancelado");
            this.Property(e => e.vpr_descricao).HasColumnName("vpr_descricao").HasMaxLength(100);
            this.Property(e => e.vpr_unidade).HasColumnName("vpr_unidade").HasMaxLength(2);
            this.HasRequired(e => e.tbProduto).WithMany(e => e.tbVendaProduto).HasForeignKey(e => e.pro_codigo);
            this.Property(e => e.pro_codigo).HasColumnName("pro_codigo");
            this.HasRequired(e => e.tbVenda).WithMany(e => e.tbVendaProduto).HasForeignKey(e => e.vec_codigo);
            this.Property(e => e.vec_codigo).HasColumnName("vec_codigo");
        }
    }
}
