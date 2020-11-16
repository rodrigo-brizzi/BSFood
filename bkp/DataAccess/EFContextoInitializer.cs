using BSFoodFramework.Migrations;
using BSFoodFramework.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BSFoodFramework.DataAccess
{
    public class EFContextoInitializer : IDatabaseInitializer<EFContexto>
    {
        //https://msdn.microsoft.com/pt-br/library/jj591330.aspx - exemplo de como usera o code-first migrations
        private readonly DbMigrationsConfiguration objDbMigrationsConfiguration;

        public EFContextoInitializer()
        {
            objDbMigrationsConfiguration = new Configuration();
        }

        public void InitializeDatabase(EFContexto objCtx)
        {
            if (!objCtx.Database.Exists())
            {
                objCtx.Database.Create();
                this.CargaInicial(objCtx);
            }
            else
            {
                var migrator = new DbMigrator(objDbMigrationsConfiguration);
                if (migrator.GetPendingMigrations().Any())
                    migrator.Update();
            }
        }

        public void CargaInicial(EFContexto objCtx)
        {
            //Auditoria Operação
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "INCLUSÃO" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "ALTERAÇÃO" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "EXCLUSÃO" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "OUTRO" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "CANCELAMENTO" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "CANCELAMENTO DE ITEM" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "AUTORIZAÇÃO PARA CLIENTE BLOQUEADO" });
            objCtx.tbAuditoriaOperacao.Add(new tbAuditoriaOperacao { audo_descricao = "AUTORIZAÇÃO PARA CLIENTE NEGATIVADO" });
                        
            //Mesas
            for (int i = 0; i < 30; i++)
            {
                objCtx.tbMesa.Add(new tbMesa { mes_descricao = "Mesa", mes_numero = (i + 1), mes_status = "L" });
            }

            //Caixa Operação
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "ABERTURA DE CAIXA", caio_tipoOperacao = "E" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "VENDA FISCAL", caio_tipoOperacao = "E" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "PEDIDO MESA", caio_tipoOperacao = "E" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "PEDIDO ENTREGA", caio_tipoOperacao = "E" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "SUPRIMENTO", caio_tipoOperacao = "E" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "SANGRIA", caio_tipoOperacao = "S" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "TROCO", caio_tipoOperacao = "S" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "VALE", caio_tipoOperacao = "S" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "PAGAMENTOS DIVERSOS", caio_tipoOperacao = "S" });
            objCtx.tbCaixaOperacao.Add(new tbCaixaOperacao { caio_descricao = "FECHAMENTO DE CAIXA", caio_tipoOperacao = "S" });

            //Estados
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "AC", est_nome = "ACRE" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "AL", est_nome = "ALAGOAS" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "AP", est_nome = "AMAPÁ" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "AM", est_nome = "AMAZONAS" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "BA", est_nome = "BAHIA" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "CE", est_nome = "CEARÁ" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "DF", est_nome = "DISTRITO FEDERAL" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "ES", est_nome = "ESPÍRITO SANTO" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "GO", est_nome = "GOIÁS" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "MA", est_nome = "MARANHÃO" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "MT", est_nome = "MATO GROSSO" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "MS", est_nome = "MATO GROSSO DO SUL" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "MG", est_nome = "MINAS GERAIS" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "PA", est_nome = "PARÁ" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "PB", est_nome = "PARAÍBA" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "PR", est_nome = "PARANÁ" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "PE", est_nome = "PERNAMBUCO" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "PI", est_nome = "PIAUÍ" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "RJ", est_nome = "RIO DE JANEIRO" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "RN", est_nome = "RIO GRANDE DO NORTE" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "RS", est_nome = "RIO GRANDE DO SUL" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "RO", est_nome = "RONDÔNIA" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "RR", est_nome = "RORAIMA" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "SC", est_nome = "SANTA CATARINA" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "SP", est_nome = "SÃO PAULO" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "SE", est_nome = "SERGIPE" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "TO", est_nome = "TOCANTINS" });
            objCtx.tbEstado.Add(new tbEstado { est_sigla = "EX", est_nome = "EXTERIOR" });
            objCtx.SaveChanges();

            //Cidade
            objCtx.tbCidade.Add(new tbCidade { cid_nome = "PRESIDENTE PRUDENTE", cid_ibge = "3541406", est_codigo = 25 });

            //PerfilAcesso
            objCtx.tbPerfilAcesso.Add(new tbPerfilAcesso { pac_descricao = "ADMINISTRADOR", pac_permiteDesconto = true, pac_permiteCancelarItem = true, pac_permiteCancelarVenda = true, pac_permiteVendaClienteBloqueado = true, pac_permiteVendaClienteNegativo = true });

            //Menu
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 0, men_nomeControle = null, men_cabecalho = "Cadastros", men_ordem = "1", men_imagem = "../Imagens/Menu/1.png", men_status = true, men_codigoPai = null });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.BairroViewModel", men_cabecalho = "Bairros", men_ordem = "1.1", men_imagem = "../Imagens/Menu/2.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.CidadeViewModel", men_cabecalho = "Cidades", men_ordem = "1.2", men_imagem = "../Imagens/Menu/3.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ClienteViewModel", men_cabecalho = "Clientes", men_ordem = "1.3", men_imagem = "../Imagens/Menu/4.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.FormaPagamentoViewModel", men_cabecalho = "Formas de Pagamento", men_ordem = "1.4", men_imagem = "../Imagens/Menu/5.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.FornecedorViewModel", men_cabecalho = "Fornecedores", men_ordem = "1.5", men_imagem = "../Imagens/Menu/6.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.FuncionarioViewModel", men_cabecalho = "Funcionários", men_ordem = "1.6", men_imagem = "../Imagens/Menu/7.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ClienteGrupoViewModel", men_cabecalho = "Grupos de Clientes", men_ordem = "1.7", men_imagem = "../Imagens/Menu/8.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ProdutoGrupoViewModel", men_cabecalho = "Grupos de Produtos", men_ordem = "1.8", men_imagem = "../Imagens/Menu/9.png", men_status = true, men_codigoPai = 1 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ProdutoViewModel", men_cabecalho = "Produtos", men_ordem = "1.9", men_imagem = "../Imagens/Menu/10.png", men_status = true, men_codigoPai = 1 });

            objCtx.tbMenu.Add(new tbMenu { men_nivel = 0, men_nomeControle = null, men_cabecalho = "Lançamentos", men_ordem = "2", men_imagem = "../Imagens/Menu/11.png", men_status = true, men_codigoPai = null });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Compras", men_ordem = "2.1", men_imagem = "../Imagens/Menu/12.png", men_status = true, men_codigoPai = 11 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = null, men_cabecalho = "Caixa", men_ordem = "2.2", men_imagem = "../Imagens/Menu/13.png", men_status = true, men_codigoPai = 11 });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.CaixaAberturaViewModel", men_cabecalho = "Abertura de Caixa", men_ordem = "2.2.1", men_imagem = "../Imagens/Menu/14.png", men_status = true, men_codigoPai = 13 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.CaixaViewModel", men_cabecalho = "Caixa", men_ordem = "2.2.2", men_imagem = "../Imagens/Menu/15.png", men_status = true, men_codigoPai = 13 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.CaixaFechamentoViewModel", men_cabecalho = "Fechamento de Caixa", men_ordem = "2.2.3", men_imagem = "../Imagens/Menu/16.png", men_status = true, men_codigoPai = 13 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Contas a Receber", men_ordem = "2.3", men_imagem = "../Imagens/Menu/17.png", men_status = true, men_codigoPai = 11 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Contas a Pagar", men_ordem = "2.4", men_imagem = "../Imagens/Menu/18.png", men_status = true, men_codigoPai = 11 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.EntregaViewModel", men_cabecalho = "Entregas", men_ordem = "2.5", men_imagem = "../Imagens/Menu/19.png", men_status = true, men_codigoPai = 11 });            
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.MesaViewModel", men_cabecalho = "Mesas", men_ordem = "2.6", men_imagem = "../Imagens/Menu/20.png", men_status = true, men_codigoPai = 11 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = null, men_cabecalho = "Pedidos", men_ordem = "2.7", men_imagem = "../Imagens/Menu/21.png", men_status = true, men_codigoPai = 11 });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ComandaViewModel", men_cabecalho = "Comanda", men_ordem = "2.7.1", men_imagem = "../Imagens/Menu/22.png", men_status = true, men_codigoPai = 21 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.EntregaPedidoViewModel", men_cabecalho = "Pedido de Entrega", men_ordem = "2.7.2", men_imagem = "../Imagens/Menu/23.png", men_status = true, men_codigoPai = 21 });

            objCtx.tbMenu.Add(new tbMenu { men_nivel = 0, men_nomeControle = null, men_cabecalho = "Relatórios", men_ordem = "3", men_imagem = "../Imagens/Menu/24.png", men_status = true, men_codigoPai = null });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Clientes", men_ordem = "3.1", men_imagem = "../Imagens/Menu/25.png", men_status = true, men_codigoPai = 24 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Compras", men_ordem = "3.2", men_imagem = "../Imagens/Menu/26.png", men_status = true, men_codigoPai = 24 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Caixa", men_ordem = "3.3", men_imagem = "../Imagens/Menu/27.png", men_status = true, men_codigoPai = 24 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Contas a Receber", men_ordem = "3.4", men_imagem = "../Imagens/Menu/28.png", men_status = true, men_codigoPai = 24 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Contas a Pagar", men_ordem = "3.5", men_imagem = "../Imagens/Menu/29.png", men_status = true, men_codigoPai = 24 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Fornecedores", men_ordem = "3.6", men_imagem = "../Imagens/Menu/30.png", men_status = true, men_codigoPai = 24 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = null, men_cabecalho = "Produtos", men_ordem = "3.7", men_imagem = "../Imagens/Menu/31.png", men_status = true, men_codigoPai = 24 });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Cardápio", men_ordem = "3.7.1", men_imagem = "../Imagens/Menu/32.png", men_status = true, men_codigoPai = 31 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Estoque", men_ordem = "3.7.2", men_imagem = "../Imagens/Menu/33.png", men_status = true, men_codigoPai = 31 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Movimentado", men_ordem = "3.7.3", men_imagem = "../Imagens/Menu/34.png", men_status = true, men_codigoPai = 31 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Para Compra", men_ordem = "3.7.4", men_imagem = "../Imagens/Menu/35.png", men_status = true, men_codigoPai = 31 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Sem Giro", men_ordem = "3.7.5", men_imagem = "../Imagens/Menu/36.png", men_status = true, men_codigoPai = 31 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = null, men_cabecalho = "Pedidos", men_ordem = "3.8", men_imagem = "../Imagens/Menu/37.png", men_status = true, men_codigoPai = 24 });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Cliente", men_ordem = "3.8.1", men_imagem = "../Imagens/Menu/38.png", men_status = true, men_codigoPai = 37 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.RelPedidoEntregadorViewModel", men_cabecalho = "Entregador", men_ordem = "3.8.2", men_imagem = "../Imagens/Menu/39.png", men_status = true, men_codigoPai = 37 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Estatística", men_ordem = "3.8.3", men_imagem = "../Imagens/Menu/40.png", men_status = true, men_codigoPai = 37 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Fechamento", men_ordem = "3.8.4", men_imagem = "../Imagens/Menu/41.png", men_status = true, men_codigoPai = 37 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Grupo", men_ordem = "3.8.5", men_imagem = "../Imagens/Menu/42.png", men_status = true, men_codigoPai = 37 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 3, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Período", men_ordem = "3.8.6", men_imagem = "../Imagens/Menu/43.png", men_status = true, men_codigoPai = 37 });

            objCtx.tbMenu.Add(new tbMenu { men_nivel = 0, men_nomeControle = null, men_cabecalho = "Utilitários", men_ordem = "4", men_imagem = "../Imagens/Menu/44.png", men_status = true, men_codigoPai = null });
            objCtx.SaveChanges();
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Backup", men_ordem = "4.1", men_imagem = "../Imagens/Menu/45.png", men_status = true, men_codigoPai = 44 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Configuração", men_ordem = "4.2", men_imagem = "../Imagens/Menu/46.png", men_status = true, men_codigoPai = 44 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.PerfilAcessoViewModel", men_cabecalho = "Perfil de acesso", men_ordem = "4.3", men_imagem = "../Imagens/Menu/47.png", men_status = true, men_codigoPai = 44 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.EmpresaViewModel", men_cabecalho = "Empresa", men_ordem = "4.4", men_imagem = "../Imagens/Menu/48.png", men_status = true, men_codigoPai = 44 });
            objCtx.tbMenu.Add(new tbMenu { men_nivel = 1, men_nomeControle = "BSFood.ViewModel.ConfiguracaoViewModel", men_cabecalho = "Sobre", men_ordem = "4.5", men_imagem = "../Imagens/Menu/49.png", men_status = true, men_codigoPai = 44 });

            objCtx.SaveChanges();

            //PerfilAcessoMenu
            for (int i = 1; i <= 49; i++)
            {
                if (i == 4 || i == 10 || i == 15 || i == 19 || i == 20 || i == 23)
                    objCtx.tbPerfilAcessoMenu.Add(new tbPerfilAcessoMenu { pac_codigo = 1, men_codigo = i, pam_permiteAlteracao = true, pam_permiteExclusao = true, pam_permiteInclusao = true, pam_permiteVisualizacao = true, pam_toolBar = true });
                else
                    objCtx.tbPerfilAcessoMenu.Add(new tbPerfilAcessoMenu { pac_codigo = 1, men_codigo = i, pam_permiteAlteracao = true, pam_permiteExclusao = true, pam_permiteInclusao = true, pam_permiteVisualizacao = true, pam_toolBar = false });
            }
            objCtx.SaveChanges();

            //Funcionario padrão
            objCtx.tbFuncionario.Add(new tbFuncionario { fun_nome = "ADMINISTRADOR", fun_login = "ADM", fun_senha = "023123", fun_logradouro = "RUA GIACOMO DOMINGOS MUNGO", fun_numero = "206", fun_cep = "19041330", fun_bairro="JARDIM ITATIAIA", cid_codigo = 1, est_codigo = 25, pac_codigo = 1 });
            objCtx.SaveChanges();

            //Empresa
            objCtx.tbEmpresa.Add(new tbEmpresa { emp_nome = "BRIZZISOFT", emp_nomeFantasia = "BRIZZISOFT", emp_logradouro = "RUA GIACOMO D. MUNGO", emp_numero = "206", emp_bairro = "JD ITATIAIA", est_codigo = 25, cid_codigo = 1, emp_telefone = "01839173706", emp_fax = "018997190639" });
            objCtx.SaveChanges();

            //Grupo de cliente consumidor
            objCtx.tbClienteGrupo.Add(new tbClienteGrupo { cgr_nome = "OUTROS", cgr_fechamento = 0, cgr_vencimento = 0 });
            objCtx.SaveChanges();

            //Cliente consumidor
            objCtx.tbCliente.Add(new tbCliente { cli_nome = "CONSUMIDOR", cli_tipo = "F", cli_sexo = "M", cli_fechamento = 0, cli_vencimento = 0, cli_limiteCredito = 0, cgr_codigo = 1 });
            objCtx.SaveChanges();

            //Bairro do cliente consumidor
            objCtx.tbBairro.Add(new tbBairro { bai_nome = "PARQUE NOVO ALVORADA", bai_taxaEntrega = 0 });
            objCtx.SaveChanges();

            //Endereço do cliente consumidor
            objCtx.tbClienteEndereco.Add(new tbClienteEndereco { cen_logradouro = "RUA ARIVERSON JUARES NOBRE", cen_numero = "95", bai_codigo = 1, cid_codigo = 1, est_codigo = 25, cli_codigo = 1 });
            objCtx.SaveChanges();

            //Telefone do cliente consumidor
            objCtx.tbClienteTelefone.Add(new tbClienteTelefone { ctl_numero = "32216195", cli_codigo = 1 });
            objCtx.SaveChanges();

            //Formas de pagamento
            objCtx.tbFormaPagamento.Add(new tbFormaPagamento { fpg_descricao = "DINHEIRO", fpg_cobranca = "V" });
            objCtx.tbFormaPagamento.Add(new tbFormaPagamento { fpg_descricao = "CARTAO CREDITO", fpg_cobranca = "V" });
            objCtx.tbFormaPagamento.Add(new tbFormaPagamento { fpg_descricao = "CARTAO DEBITO", fpg_cobranca = "V" });
            objCtx.tbFormaPagamento.Add(new tbFormaPagamento { fpg_descricao = "CHEQUE", fpg_cobranca = "V" });
            objCtx.tbFormaPagamento.Add(new tbFormaPagamento { fpg_descricao = "CONVENIO", fpg_cobranca = "P" });
            objCtx.SaveChanges();

            //Configuração
            objCtx.tbConfiguracao.Add(new tbConfiguracao { cfg_senhaMaster = "023123", cfg_quantidadeMesa = 30, cgr_codigo = 1, cli_codigo = 1, fpg_codigo = 1 });
            objCtx.SaveChanges();
        }
    }
}
