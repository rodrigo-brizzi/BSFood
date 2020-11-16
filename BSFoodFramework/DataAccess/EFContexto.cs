using BSFoodFramework.Apoio;
using BSFoodFramework.DataAccess.Config;
using BSFoodFramework.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace BSFoodFramework.DataAccess
{
    public class EFContexto : DbContext
    {
        public EFContexto()
            : base(FrameworkUtil.RetornaStringConexao(), true)
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            Database.SetInitializer(new EFContextoInitializer());
            //Database.SetInitializer<EFContexto>(new EFContextoInitializer());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFContexto, BSFood.Migrations.Configuration>("BSFood.DataAccess.EFContexto"));
        }

        public virtual DbSet<tbAuditoria> tbAuditoria { get; set; }
        public virtual DbSet<tbAuditoriaOperacao> tbAuditoriaOperacao { get; set; }
        public virtual DbSet<tbBairro> tbBairro { get; set; }
        public virtual DbSet<tbCaixa> tbCaixa { get; set; }
        public virtual DbSet<tbCaixaMovimento> tbCaixaMovimento { get; set; }
        public virtual DbSet<tbCaixaOperacao> tbCaixaOperacao { get; set; }
        public virtual DbSet<tbCidade> tbCidade { get; set; }
        public virtual DbSet<tbCliente> tbCliente { get; set; }
        public virtual DbSet<tbClienteEndereco> tbClienteEndereco { get; set; }
        public virtual DbSet<tbClienteGrupo> tbClienteGrupo { get; set; }
        public virtual DbSet<tbClienteTelefone> tbClienteTelefone { get; set; }
        public virtual DbSet<tbConfiguracao> tbConfiguracao { get; set; }
        public virtual DbSet<tbEmpresa> tbEmpresa { get; set; }
        public virtual DbSet<tbEstado> tbEstado { get; set; }
        public virtual DbSet<tbFormaPagamento> tbFormaPagamento { get; set; }
        public virtual DbSet<tbFormaPagamentoTipo> tbFormaPagamentoTipo { get; set; }
        public virtual DbSet<tbFornecedor> tbFornecedor { get; set; }
        public virtual DbSet<tbFuncionario> tbFuncionario { get; set; }
        public virtual DbSet<tbMenu> tbMenu { get; set; }
        public virtual DbSet<tbMesa> tbMesa { get; set; }
        public virtual DbSet<tbPedido> tbPedido { get; set; }
        public virtual DbSet<tbPedidoProduto> tbPedidoProduto { get; set; }
        public virtual DbSet<tbPerfilAcesso> tbPerfilAcesso { get; set; }
        public virtual DbSet<tbPerfilAcessoMenu> tbPerfilAcessoMenu { get; set; }
        public virtual DbSet<tbProduto> tbProduto { get; set; }
        public virtual DbSet<tbProdutoGrupo> tbProdutoGrupo { get; set; }
        public virtual DbSet<tbProdutoSubGrupo> tbProdutoSubGrupo { get; set; }
        public virtual DbSet<tbVenda> tbVenda { get; set; }
        public virtual DbSet<tbVendaProduto> tbVendaProduto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //remover algumas convenções do entity framework
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //configurar as propriedades strings para terem o tipo nvarchar no banco de dados
            if (Database.Connection is SqlCeConnection)
            {
                modelBuilder.Properties<string>().Configure(p => p.HasColumnType("nvarchar"));
            }
            else if (Database.Connection is SqlConnection)
            {
                modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
                //modelBuilder.Properties<DateTime>().Configure(p => p.HasColumnType("datetime2"));
            }

            //configurar o tamanho das strings para tamanho 100 quando não informado na configuração da entidade
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            //adicionar a configuração para cada entidade
            modelBuilder.Configurations.Add(new tbAuditoriaConfig());
            modelBuilder.Configurations.Add(new tbAuditoriaOperacaoConfig());
            modelBuilder.Configurations.Add(new tbBairroConfig());
            modelBuilder.Configurations.Add(new tbCaixaConfig());
            modelBuilder.Configurations.Add(new tbCaixaMovimentoConfig());
            modelBuilder.Configurations.Add(new tbCaixaOperacaoConfig());
            modelBuilder.Configurations.Add(new tbCidadeConfig());
            modelBuilder.Configurations.Add(new tbClienteConfig());
            modelBuilder.Configurations.Add(new tbClienteEnderecoConfig());
            modelBuilder.Configurations.Add(new tbClienteGrupoConfig());
            modelBuilder.Configurations.Add(new tbClienteTelefoneConfig());
            modelBuilder.Configurations.Add(new tbConfiguracaoConfig());
            modelBuilder.Configurations.Add(new tbEmpresaConfig());
            modelBuilder.Configurations.Add(new tbEstadoConfig());
            modelBuilder.Configurations.Add(new tbFormaPagamentoConfig());
            modelBuilder.Configurations.Add(new tbFormaPagamentoTipoConfig());
            modelBuilder.Configurations.Add(new tbFornecedorConfig());
            modelBuilder.Configurations.Add(new tbFuncionarioConfig());
            modelBuilder.Configurations.Add(new tbMenuConfig());
            modelBuilder.Configurations.Add(new tbMesaConfig());
            modelBuilder.Configurations.Add(new tbPedidoConfig());
            modelBuilder.Configurations.Add(new tbPedidoProdutoConfig());
            modelBuilder.Configurations.Add(new tbPerfilAcessoConfig());
            modelBuilder.Configurations.Add(new tbPerfilAcessoMenuConfig());
            modelBuilder.Configurations.Add(new tbProdutoConfig());
            modelBuilder.Configurations.Add(new tbProdutoGrupoConfig());
            modelBuilder.Configurations.Add(new tbProdutoSubGrupoConfig());
            modelBuilder.Configurations.Add(new tbVendaConfig());
            modelBuilder.Configurations.Add(new tbVendaProdutoConfig());
        }
    }
}
