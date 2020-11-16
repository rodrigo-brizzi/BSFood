using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbFuncionarioConfig : EntityTypeConfiguration<tbFuncionario>
    {
        public tbFuncionarioConfig()
        {
            this.ToTable("tbFuncionario");
            this.HasKey(e => e.fun_codigo);

            this.Property(e => e.fun_codigo).HasColumnName("fun_codigo");
            this.Property(e => e.fun_nome).HasColumnName("fun_nome").HasMaxLength(100);
            this.Property(e => e.fun_login).HasColumnName("fun_login").HasMaxLength(20);
            this.Property(e => e.fun_senha).HasColumnName("fun_senha").HasMaxLength(20);
            this.Property(e => e.fun_logradouro).HasColumnName("fun_logradouro").HasMaxLength(200);
            this.Property(e => e.fun_numero).HasColumnName("fun_numero").HasMaxLength(10);
            this.Property(e => e.fun_bairro).HasColumnName("fun_bairro").HasMaxLength(100);
            this.Property(e => e.fun_cep).HasColumnName("fun_cep").HasMaxLength(9);
            this.Property(e => e.fun_complemento).HasColumnName("fun_complemento").HasMaxLength(150);
            this.Property(e => e.fun_telefone).HasColumnName("fun_telefone").HasMaxLength(20);
            this.Property(e => e.fun_celular).HasColumnName("fun_celular").HasMaxLength(20);
            this.HasRequired(e => e.tbCidade).WithMany(e => e.tbFuncionario).HasForeignKey(e => e.cid_codigo);
            this.Property(e => e.cid_codigo).HasColumnName("cid_codigo");
            this.HasRequired(e => e.tbEstado).WithMany(e => e.tbFuncionario).HasForeignKey(e => e.est_codigo);
            this.Property(e => e.est_codigo).HasColumnName("est_codigo");
            this.HasRequired(e => e.tbPerfilAcesso).WithMany(e => e.tbFuncionario).HasForeignKey(e => e.pac_codigo);
            this.Property(e => e.pac_codigo).HasColumnName("pac_codigo");
        }
    }
}
