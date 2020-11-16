using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
{
    public class tbMesaConfig : EntityTypeConfiguration<tbMesa>
    {
        public tbMesaConfig()
        {
            this.ToTable("tbMesa");
            this.HasKey(e => e.mes_codigo);

            this.Property(e => e.mes_codigo).HasColumnName("mes_codigo");
            this.Property(e => e.mes_descricao).HasColumnName("mes_descricao").HasMaxLength(100);
            this.Property(e => e.mes_numero).HasColumnName("mes_numero");
            this.Property(e => e.mes_status).HasColumnName("mes_status").HasMaxLength(1);
            this.Property(e => e.mes_imagemLivre).HasColumnName("mes_imagemLivre").HasMaxLength(250);
            this.Property(e => e.mes_imagemOcupada).HasColumnName("mes_imagemOcupada").HasMaxLength(250);
            this.Property(e => e.mes_imagemConta).HasColumnName("mes_imagemConta").HasMaxLength(250);
            this.Property(e => e.mes_terminal).HasColumnName("mes_terminal").HasMaxLength(20);
            this.HasOptional(e => e.tbPedido).WithMany(e => e.tbMesa).HasForeignKey(e => e.ped_codigo);
            this.Property(e => e.ped_codigo).HasColumnName("ped_codigo");

        }
    }
}
