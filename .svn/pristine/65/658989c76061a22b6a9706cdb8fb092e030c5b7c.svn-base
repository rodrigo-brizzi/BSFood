using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbVendaConfig : EntityTypeConfiguration<tbVenda>
    {
        public tbVendaConfig()
        {
            this.ToTable("tbVenda");
            this.HasKey(e => e.vec_codigo);

            this.Property(e => e.vec_codigo).HasColumnName("vec_codigo");
            this.Property(e => e.vec_data).HasColumnName("vec_data");
            this.Property(e => e.vec_cpfCnpj).HasColumnName("vec_cpfCnpj").HasMaxLength(20);
            this.Property(e => e.vec_observacao).HasColumnName("vec_observacao").HasMaxLength(250);
            this.Property(e => e.vec_cancelado).HasColumnName("vec_cancelado");
            this.Property(e => e.vec_valorDespesa).HasColumnName("vec_valorDespesa");
            this.Property(e => e.vec_valorDesconto).HasColumnName("vec_valorDesconto");
            this.Property(e => e.vec_valorRecebido).HasColumnName("vec_valorRecebido");
            this.Property(e => e.vec_valorTroco).HasColumnName("vec_valorTroco");
            this.Property(e => e.vec_valorTotal).HasColumnName("vec_valorTotal");
            this.HasRequired(e => e.tbCliente).WithMany(e => e.tbVenda).HasForeignKey(e => e.cli_codigo);
            this.Property(e => e.cli_codigo).HasColumnName("cli_codigo");
            this.HasRequired(e => e.tbFuncionario).WithMany(e => e.tbVenda).HasForeignKey(e => e.fun_codigo);
            this.Property(e => e.fun_codigo).HasColumnName("fun_codigo");
            this.HasRequired(e => e.tbCaixa).WithMany(e => e.tbVenda).HasForeignKey(e => e.cai_codigo);
            this.Property(e => e.cai_codigo).HasColumnName("cai_codigo");
            this.HasRequired(e => e.tbPedido).WithMany(e => e.tbVenda).HasForeignKey(e => e.ped_codigo);
            this.Property(e => e.ped_codigo).HasColumnName("ped_codigo");
        }
    }
}
