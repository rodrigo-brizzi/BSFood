using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbPedidoProdutoConfig : EntityTypeConfiguration<tbPedidoProduto>
    {
        public tbPedidoProdutoConfig()
        {
            this.ToTable("tbPedidoProduto");
            this.HasKey(e => e.ppr_codigo);

            this.Property(e => e.ppr_codigo).HasColumnName("ppr_codigo");
            this.Property(e => e.ppr_sequencia).HasColumnName("ppr_sequencia");
            this.Property(e => e.ppr_quantidade).HasColumnName("ppr_quantidade");
            this.Property(e => e.ppr_valorUnitario).HasColumnName("ppr_valorUnitario");
            this.Property(e => e.ppr_valorTotal).HasColumnName("ppr_valorTotal");
            this.Property(e => e.ppr_observacao).HasColumnName("ppr_observacao").HasMaxLength(250);
            this.Property(e => e.ppr_impresso).HasColumnName("ppr_impresso");
            this.Property(e => e.ppr_descricao).HasColumnName("ppr_descricao").HasMaxLength(100);
            this.HasRequired(e => e.tbProduto).WithMany(e => e.tbPedidoProduto).HasForeignKey(e => e.pro_codigo);
            this.Property(e => e.pro_codigo).HasColumnName("pro_codigo");
            this.HasRequired(e => e.tbPedido).WithMany(e => e.tbPedidoProduto).HasForeignKey(e => e.ped_codigo);
            this.Property(e => e.ped_codigo).HasColumnName("ped_codigo");
        }
    }
}
