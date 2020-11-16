using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbClienteConfig : EntityTypeConfiguration<tbCliente>
    {
        public tbClienteConfig()
        {
            this.ToTable("tbCliente");
            this.HasKey(e => e.cli_codigo);

            this.Property(e => e.cli_codigo).HasColumnName("cli_codigo");
            this.Property(e => e.cli_nome).HasColumnName("cli_nome").HasMaxLength(100);
            this.Property(e => e.cli_nomeFantasia).HasColumnName("cli_nomeFantasia").HasMaxLength(100);
            this.Property(e => e.cli_tipo).HasColumnName("cli_tipo").HasMaxLength(1);
            this.Property(e => e.cli_cpfCnpj).HasColumnName("cli_cpfCnpj").HasMaxLength(20);
            this.Property(e => e.cli_rgIe).HasColumnName("cli_rgIe").HasMaxLength(20);
            this.Property(e => e.cli_sexo).HasColumnName("cli_sexo").HasMaxLength(1);
            this.Property(e => e.cli_email).HasColumnName("cli_email").HasMaxLength(100);
            this.Property(e => e.cli_observacao).HasColumnName("cli_observacao").HasMaxLength(250);
            this.Property(e => e.cli_fechamento).HasColumnName("cli_fechamento");
            this.Property(e => e.cli_vencimento).HasColumnName("cli_vencimento");
            this.Property(e => e.cli_limiteCredito).HasColumnName("cli_limiteCredito");
            this.Property(e => e.cli_dataNascimento).HasColumnName("cli_dataNascimento");
            this.HasRequired(e => e.tbClienteGrupo).WithMany(e => e.tbCliente).HasForeignKey(e => e.cgr_codigo);
            this.Property(e => e.cgr_codigo).HasColumnName("cgr_codigo");
        }
    }
}
