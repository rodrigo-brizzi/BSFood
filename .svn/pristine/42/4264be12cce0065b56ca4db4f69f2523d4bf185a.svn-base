using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbClienteEnderecoConfig : EntityTypeConfiguration<tbClienteEndereco>
    {
        public tbClienteEnderecoConfig()
        {
            this.ToTable("tbClienteEndereco");
            this.HasKey(e => e.cen_codigo);

            this.Property(e => e.cen_codigo).HasColumnName("cen_codigo");
            this.Property(e => e.cen_logradouro).HasColumnName("cen_logradouro").HasMaxLength(200);
            this.Property(e => e.cen_numero).HasColumnName("cen_numero").HasMaxLength(10);
            this.Property(e => e.cen_cep).HasColumnName("cen_cep").HasMaxLength(9);
            this.Property(e => e.cen_complemento).HasColumnName("cen_complemento").HasMaxLength(150);
            this.HasRequired(e => e.tbBairro).WithMany(e => e.tbClienteEndereco).HasForeignKey(e => e.bai_codigo);
            this.Property(e => e.bai_codigo).HasColumnName("bai_codigo");
            this.HasRequired(e => e.tbCidade).WithMany(e => e.tbClienteEndereco).HasForeignKey(e => e.cid_codigo);
            this.Property(e => e.cid_codigo).HasColumnName("cid_codigo");
            this.HasRequired(e => e.tbEstado).WithMany(e => e.tbClienteEndereco).HasForeignKey(e => e.est_codigo);
            this.Property(e => e.est_codigo).HasColumnName("est_codigo");
            this.HasRequired(e => e.tbCliente).WithMany(e => e.tbClienteEndereco).HasForeignKey(e => e.cli_codigo);
            this.Property(e => e.cli_codigo).HasColumnName("cli_codigo");
        }
    }
}
