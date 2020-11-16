using BSFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFood.DataAccess.Config
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
