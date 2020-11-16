using BSFoodFramework.Models;
using System.Data.Entity.ModelConfiguration;

namespace BSFoodFramework.DataAccess.Config
{
    public class tbClienteTelefoneConfig : EntityTypeConfiguration<tbClienteTelefone>
    {
        public tbClienteTelefoneConfig()
        {
            this.ToTable("tbClienteTelefone");
            this.HasKey(e => e.ctl_codigo);

            this.Property(e => e.ctl_codigo).HasColumnName("ctl_codigo");
            this.Property(e => e.ctl_numero).HasColumnName("ctl_numero").HasMaxLength(20);
            this.HasRequired(e => e.tbCliente).WithMany(e => e.tbClienteTelefone).HasForeignKey(e => e.cli_codigo);
            this.Property(e => e.cli_codigo).HasColumnName("cli_codigo");
        }
    }
}
