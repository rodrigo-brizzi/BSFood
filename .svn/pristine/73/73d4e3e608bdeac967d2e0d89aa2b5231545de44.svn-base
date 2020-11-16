using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbEmpresaConfig : EntityTypeConfiguration<tbEmpresa>
    {
        public tbEmpresaConfig()
        {
            this.ToTable("tbEmpresa");
            this.HasKey(e => e.emp_codigo);

            this.Property(e => e.emp_codigo).HasColumnName("emp_codigo");
            this.Property(e => e.emp_nome).HasColumnName("emp_nome").HasMaxLength(100);
            this.Property(e => e.emp_nomeFantasia).HasColumnName("emp_nomeFantasia").HasMaxLength(100);
            this.Property(e => e.emp_cnpj).HasColumnName("emp_cnpj").HasMaxLength(20);
            this.Property(e => e.emp_ie).HasColumnName("emp_ie").HasMaxLength(20);
            this.Property(e => e.emp_im).HasColumnName("emp_im").HasMaxLength(20);
            this.Property(e => e.emp_assinaturaSat).HasColumnName("emp_assinaturaSat").HasMaxLength(256);
            this.Property(e => e.emp_logradouro).HasColumnName("emp_logradouro").HasMaxLength(200);
            this.Property(e => e.emp_numero).HasColumnName("emp_numero").HasMaxLength(10);
            this.Property(e => e.emp_bairro).HasColumnName("emp_bairro").HasMaxLength(100);
            this.Property(e => e.emp_cep).HasColumnName("emp_cep").HasMaxLength(9);
            this.Property(e => e.emp_complemento).HasColumnName("emp_complemento").HasMaxLength(150);
            this.Property(e => e.emp_telefone).HasColumnName("emp_telefone").HasMaxLength(20);
            this.Property(e => e.emp_fax).HasColumnName("emp_fax").HasMaxLength(20);
            this.Property(e => e.emp_logo).HasColumnName("emp_logo").HasMaxLength(8000);
            this.Property(e => e.emp_logoFormato).HasColumnName("emp_logoFormato").HasMaxLength(5);

            this.HasRequired(e => e.tbCidade).WithMany(e => e.tbEmpresa).HasForeignKey(e => e.cid_codigo);
            this.Property(e => e.cid_codigo).HasColumnName("cid_codigo");

            this.HasRequired(e => e.tbEstado).WithMany(e => e.tbEmpresa).HasForeignKey(e => e.est_codigo);
            this.Property(e => e.est_codigo).HasColumnName("est_codigo");
        }
    }
}