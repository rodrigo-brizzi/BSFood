using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbProdutoGrupoConfig : EntityTypeConfiguration<tbProdutoGrupo>
    {
        public tbProdutoGrupoConfig()
        {
            this.ToTable("tbProdutoGrupo");
            this.HasKey(e => e.pgr_codigo);

            this.Property(e => e.pgr_codigo).HasColumnName("pgr_codigo");
            this.Property(e => e.pgr_nome).HasColumnName("pgr_nome").HasMaxLength(100);
        }
    }
}
