using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbProdutoSubGrupoConfig : EntityTypeConfiguration<tbProdutoSubGrupo>
    {
        public tbProdutoSubGrupoConfig()
        {
            this.ToTable("tbProdutoSubGrupo");
            this.HasKey(e => e.psgr_codigo);

            this.Property(e => e.psgr_codigo).HasColumnName("psgr_codigo");
            this.Property(e => e.psgr_nome).HasColumnName("psgr_nome").HasMaxLength(100);
            this.Property(e => e.psgr_exibeRelatorio).HasColumnName("psgr_exibeRelatorio");
            this.HasRequired(e => e.tbProdutoGrupo).WithMany(e => e.tbProdutoSubGrupo).HasForeignKey(e => e.pgr_codigo);
            this.Property(e => e.pgr_codigo).HasColumnName("pgr_codigo");
        }
    }
}
