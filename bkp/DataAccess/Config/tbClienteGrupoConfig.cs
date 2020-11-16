using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbClienteGrupoConfig : EntityTypeConfiguration<tbClienteGrupo>
    {
        public tbClienteGrupoConfig()
        {
            this.ToTable("tbClienteGrupo");
            this.HasKey(e => e.cgr_codigo);

            this.Property(e => e.cgr_codigo).HasColumnName("cgr_codigo");
            this.Property(e => e.cgr_nome).HasColumnName("cgr_nome").HasMaxLength(100);
            this.Property(e => e.cgr_fechamento).HasColumnName("cgr_fechamento");
            this.Property(e => e.cgr_vencimento).HasColumnName("cgr_vencimento");
        }
    }
}
