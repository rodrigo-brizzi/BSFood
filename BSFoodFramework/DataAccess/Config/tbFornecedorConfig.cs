using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbFornecedorConfig : EntityTypeConfiguration<tbFornecedor>
    {
        public tbFornecedorConfig()
        {
            this.ToTable("tbFornecedor");
            this.HasKey(e => e.for_codigo);

            this.Property(e => e.for_codigo).HasColumnName("for_codigo");
            this.Property(e => e.for_nome).HasColumnName("for_nome").HasMaxLength(100);
            this.Property(e => e.for_logradouro).HasColumnName("for_logradouro").HasMaxLength(200);
            this.Property(e => e.for_numero).HasColumnName("for_numero").HasMaxLength(10);
            this.Property(e => e.for_bairro).HasColumnName("for_bairro").HasMaxLength(100);
            this.Property(e => e.for_cep).HasColumnName("for_cep").HasMaxLength(9);
            this.Property(e => e.for_complemento).HasColumnName("for_complemento").HasMaxLength(150);
            this.Property(e => e.for_telefone).HasColumnName("for_telefone").HasMaxLength(20);
            this.Property(e => e.for_fax).HasColumnName("for_fax").HasMaxLength(20);
            this.Property(e => e.for_contato).HasColumnName("for_contato").HasMaxLength(150);
            this.Property(e => e.for_email).HasColumnName("for_email").HasMaxLength(100);
            this.Property(e => e.for_site).HasColumnName("for_site").HasMaxLength(100);
            this.Property(e => e.for_cnpj).HasColumnName("for_cnpj").HasMaxLength(20);
            this.Property(e => e.for_ie).HasColumnName("for_ie").HasMaxLength(20);
            this.Property(e => e.for_observacao).HasColumnName("for_observacao").HasMaxLength(250);
            this.HasRequired(e => e.tbCidade).WithMany(e => e.tbFornecedor).HasForeignKey(e => e.cid_codigo);
            this.Property(e => e.cid_codigo).HasColumnName("cid_codigo");
            this.HasRequired(e => e.tbEstado).WithMany(e => e.tbFornecedor).HasForeignKey(e => e.est_codigo);
            this.Property(e => e.est_codigo).HasColumnName("est_codigo");
        }
    }
}
