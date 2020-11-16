using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbAuditoriaOperacaoConfig : EntityTypeConfiguration<tbAuditoriaOperacao>
    {
        public tbAuditoriaOperacaoConfig()
        {
            this.ToTable("tbAuditoriaOperacao");
            this.HasKey(e => e.audo_codigo);

            this.Property(e => e.audo_codigo).HasColumnName("audo_codigo");
            this.Property(e => e.audo_descricao).HasColumnName("audo_descricao").HasMaxLength(50);
        }
    }
}
