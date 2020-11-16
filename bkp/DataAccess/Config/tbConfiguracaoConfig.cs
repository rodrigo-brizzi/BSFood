using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbConfiguracaoConfig : EntityTypeConfiguration<tbConfiguracao>
    {
        public tbConfiguracaoConfig()
        {
            this.ToTable("tbConfiguracao");
            this.HasKey(e => e.cfg_codigo);

            this.Property(e => e.cfg_codigo).HasColumnName("cfg_codigo");
            this.Property(e => e.cfg_ultimoLogin).HasColumnName("cfg_ultimoLogin");
            this.Property(e => e.cfg_senhaMaster).HasColumnName("cfg_senhaMaster").HasMaxLength(30);
            this.Property(e => e.cfg_cnpjSoftwareHouse).HasColumnName("cfg_cnpjSoftwareHouse").HasMaxLength(20);
            this.Property(e => e.cfg_impressoraEntrega).HasColumnName("cfg_impressoraEntrega").HasMaxLength(150);
            this.Property(e => e.cfg_impressoraComanda).HasColumnName("cfg_impressoraComanda").HasMaxLength(150);
            this.Property(e => e.cfg_impressoraBebida).HasColumnName("cfg_impressoraBebida").HasMaxLength(150);
            this.Property(e => e.cfg_impressoraBalcao).HasColumnName("cfg_impressoraBalcao").HasMaxLength(150);
            this.Property(e => e.cfg_quantidadeMesa).HasColumnName("cfg_quantidadeMesa");
            this.HasRequired(e => e.tbClienteGrupo).WithMany(e => e.tbConfiguracao).HasForeignKey(e => e.cgr_codigo);
            this.Property(e => e.cgr_codigo).HasColumnName("cgr_codigo");
            this.HasRequired(e => e.tbCliente).WithMany(e => e.tbConfiguracao).HasForeignKey(e => e.cli_codigo);
            this.Property(e => e.cli_codigo).HasColumnName("cli_codigo");
            this.HasRequired(e => e.tbFormaPagamento).WithMany(e => e.tbConfiguracao).HasForeignKey(e => e.fpg_codigo);
            this.Property(e => e.fpg_codigo).HasColumnName("fpg_codigo");
        }
    }
}
