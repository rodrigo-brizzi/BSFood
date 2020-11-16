using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbPerfilAcessoConfig : EntityTypeConfiguration<tbPerfilAcesso>
    {
        public tbPerfilAcessoConfig()
        {
            this.ToTable("tbPerfilAcesso");
            this.HasKey(e => e.pac_codigo);

            this.Property(e => e.pac_codigo).HasColumnName("pac_codigo");
            this.Property(e => e.pac_descricao).HasColumnName("pac_descricao").HasMaxLength(30);
            this.Property(e => e.pac_permiteDesconto).HasColumnName("pac_permiteDesconto");
            this.Property(e => e.pac_permiteCancelarItem).HasColumnName("pac_permiteCancelarItem");
            this.Property(e => e.pac_permiteCancelarVenda).HasColumnName("pac_permiteCancelarVenda");
            this.Property(e => e.pac_permiteVendaClienteBloqueado).HasColumnName("pac_permiteVendaClienteBloqueado");
            this.Property(e => e.pac_permiteVendaClienteNegativo).HasColumnName("pac_permiteVendaClienteNegativo");
        }
    }
}
