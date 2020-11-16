using BSFood.View;
using BSFood.Apoio;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class ProdutoViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand NovoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand ProdutoGrupoCommand { get; set; }

        public ProdutoViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            EditarCommand = new DelegateCommand(Editar, CanEditar);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ExcluirCommand = new DelegateCommand(Excluir, CanExcluir);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);
            LogCommand = new DelegateCommand(Log, CanLog);
            ProdutoGrupoCommand = new DelegateCommand(ProdutoGrupo);
            CarregaComboGrupo();
            Pesquisar(0);
        }


        #region Propriedades

        private string _strProCodigoPesquisa;
        public string strProCodigoPesquisa
        {
            get { return _strProCodigoPesquisa; }
            set
            {
                _strProCodigoPesquisa = value;
            }
        }

        private string _strProNomePesquisa;
        public string strProNomePesquisa
        {
            get { return _strProNomePesquisa; }
            set
            {
                _strProNomePesquisa = value;
            }
        }

        [Required(ErrorMessage = "Nome do Produto obrigatório")]
        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string pro_nome
        {
            get { return objProduto == null ? string.Empty : objProduto.pro_nome; }
            set
            {
                if (objProduto.pro_nome != value)
                {
                    objProduto.pro_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Grupo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Grupo obrigatório")]
        public int pgr_codigo
        {
            get { return objProduto == null ? 0 : objProduto.pgr_codigo; }
            set
            {
                if (objProduto.pgr_codigo != value)
                {
                    objProduto.pgr_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Sub Grupo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Sub Grupo obrigatório")]
        public int psgr_codigo
        {
            get
            {
                if (objProduto == null)
                    return 0;
                else
                {
                    RaisePropertyChanged("pgr_codigo", false);
                    return objProduto.psgr_codigo;
                }
            }
            set
            {
                if (objProduto.psgr_codigo != value)
                {
                    objProduto.psgr_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(250, ErrorMessage = "É permitido apenas 250 caracteres")]
        public string pro_observacao
        {
            get { return objProduto == null ? string.Empty : objProduto.pro_observacao; }
            set
            {
                if (objProduto.pro_observacao != value)
                {
                    objProduto.pro_observacao = value;
                    RaisePropertyChanged();
                }
            }
        }

        private tbProduto _objProduto;
        public tbProduto objProduto
        {
            get { return _objProduto; }
            set
            {
                _objProduto = value;
                RaisePropertyChanged(null);
            }
        }

        private List<tbProduto> _arrProdutoPesquisa;
        public List<tbProduto> arrProdutoPesquisa
        {
            get { return _arrProdutoPesquisa; }
            set
            {
                _arrProdutoPesquisa = value;
                RaisePropertyChanged();
                if (_arrProdutoPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
            }
        }

        private List<tbProdutoGrupo> _arrProdutoGrupo;
        public List<tbProdutoGrupo> arrProdutoGrupo
        {
            get { return _arrProdutoGrupo; }
            set
            {
                _arrProdutoGrupo = value;
                RaisePropertyChanged();
            }
        }

        public List<tbAuditoria> arrAuditoria { get; set; }

        #endregion Propriedades



        #region Comandos

        private bool CanNavegar(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao);
        }
        private void Navegar(object objParam)
        {
            if (objParam != null)
            {
                if (objParam.ToString() == "Proximo")
                {
                    if (base.intPaginaAtual < base.intTotalPagina)
                    {
                        Pesquisar(base.intPaginaAtual * base.intQtdeRegPagina);
                        base.intPaginaAtual++;
                    }
                }
                else if (objParam.ToString() == "Anterior")
                {
                    if (base.intPaginaAtual > 1)
                    {
                        base.intPaginaAtual--;
                        if (base.intPaginaAtual == 1)
                            Pesquisar(0);
                        else
                            Pesquisar((base.intPaginaAtual - 1) * base.intQtdeRegPagina);
                    }
                }
            }
        }

        private bool CanNovo(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteInclusaoRegistro;
        }
        private void Novo(object objParam)
        {
            tbProduto objProdutoAux = new tbProduto();
            objProduto = objProdutoAux;
            base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
        }

        private bool CanEditar(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteAlteracaoRegistro);
        }
        private void Editar(object objParam)
        {
            if (objParam != null)
            {
                Retorno objRetorno;
                using (var objBLL = new Produtos())
                {
                    objRetorno = objBLL.RetornaProduto((int)objParam, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objProduto = (tbProduto)objRetorno.objRetorno;
                    base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
                }
                else
                {
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
            }
        }

        private bool CanSalvar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao;
        }
        private void Salvar(object objParam)
        {
            var focusedElement = Keyboard.FocusedElement as FrameworkElement;
            if (focusedElement is TextBox)
            {
                var expression = focusedElement.GetBindingExpression(TextBox.TextProperty);
                if (expression != null && expression.ParentBinding.UpdateSourceTrigger == System.Windows.Data.UpdateSourceTrigger.LostFocus)
                    expression.UpdateSource();
            }

            Validate();
            if (!HasErrors)
            {
                Retorno objRetorno;
                using (var objBLL = new Produtos())
                {
                    objRetorno = objBLL.SalvarProduto(objProduto, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objProduto = null;
                    ClearAllErrorsAsync();
                    base.enStatusTelaAtual = enStatusTela.Navegacao;
                    Pesquisar(null);
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private bool CanCancelar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao;
        }
        private void Cancelar(object objParam)
        {
            if (MessageBox.Show("Todas as alterações serão perdidas, deseja cancelar a edição?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objProduto = null;
                ClearAllErrorsAsync();
                base.enStatusTelaAtual = enStatusTela.Navegacao;
                Pesquisar(null);
            }
        }

        private bool CanExcluir(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteExclusaoRegistro);
        }
        private void Excluir(object objParam)
        {
            if (objParam != null)
            {
                if (MessageBox.Show("Tem Certeza que deseja excluir o registro selecionado?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Retorno objRetorno;
                    using (var objBLL = new Produtos())
                    {
                        objRetorno = objBLL.ExcluirProduto((int)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objProduto = null;
                        ClearAllErrorsAsync();
                        base.enStatusTelaAtual = enStatusTela.Navegacao;
                        Pesquisar(null);
                    }
                    else
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
            }
        }

        private bool CanPesquisar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao;
        }
        public void Pesquisar(object objParam)
        {
            if (objParam != null && objParam.GetType() == typeof(tbProduto))
            {
                if (base.blnJanela)
                {
                    _objProduto = (tbProduto)objParam;
                    Dispose();
                }
            }
            else
            {
                int intSkip;
                if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                    intSkip = 0;

                Retorno objRetorno;
                using (var objBLL = new Produtos())
                {
                    objRetorno = objBLL.RetornaListaProduto(strProCodigoPesquisa, strProNomePesquisa, intSkip, base.intQtdeRegPagina);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    if (objRetorno.intQtdeRegistro > 0)
                    {
                        if ((objRetorno.intQtdeRegistro % base.intQtdeRegPagina) > 0)
                            base.intTotalPagina = (int)(objRetorno.intQtdeRegistro / base.intQtdeRegPagina) + 1;
                        else
                            base.intTotalPagina = (int)(objRetorno.intQtdeRegistro / base.intQtdeRegPagina);
                        base.intPaginaAtual = 1;
                        base.intQtdeReg = objRetorno.intQtdeRegistro;
                    }
                    arrProdutoPesquisa = (List<tbProduto>)objRetorno.objRetorno;
                    if (arrProdutoPesquisa.Count() == 0)
                    {
                        base.intTotalPagina = 1;
                        base.intPaginaAtual = 1;
                        base.intQtdeReg = 0;
                    }
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private bool CanLog(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao && objProduto != null && objProduto.pro_codigo > 0;
        }
        private void Log(object objParam)
        {
            if (objParam != null)
            {
                if (objParam.ToString() == "AbrirTela")
                {
                    Retorno objRetorno;
                    using (var objBll = new Auditoria())
                    {
                        objRetorno = objBll.RetornaListaAuditoria(objProduto.pro_codigo, objProduto);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        arrAuditoria = (List<tbAuditoria>)objRetorno.objRetorno;
                        winAuditoria objTelaAuditoria = new winAuditoria();
                        objTelaAuditoria.Title = "Auditoria - " + base.strNomeTela;
                        objTelaAuditoria.DataContext = this;
                        objTelaAuditoria.Owner = (Window)Application.Current.MainWindow;
                        objTelaAuditoria.Closed += (sen, eve) =>
                        {
                            arrAuditoria = null;
                            objTelaAuditoria = null;
                        };
                        objTelaAuditoria.ShowDialog();
                    }
                    else
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
                else if (objParam.GetType() == typeof(winAuditoria))
                {
                    ((Window)objParam).Close();
                }
            }
        }

        private void ProdutoGrupo(object objParam)
        {
            winCadastro objTelaCadastro = new winCadastro();
            ProdutoGrupoViewModel objProdutoGrupoViewModel = new ProdutoGrupoViewModel();
            objProdutoGrupoViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
            objProdutoGrupoViewModel.blnJanela = true;
            objTelaCadastro.Title = "Cadastro - " + objProdutoGrupoViewModel.strNomeTela;
            objTelaCadastro.DataContext = objProdutoGrupoViewModel;
            objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
            objTelaCadastro.Closed += (sen, eve) =>
            {
                if (objProdutoGrupoViewModel.objProdutoGrupo != null)
                {
                    CarregaComboGrupo();
                    objProduto.pgr_codigo = (int)objProdutoGrupoViewModel.objProdutoGrupo.pgr_codigo;
                    RaisePropertyChanged("pgr_codigo");
                }
                objProdutoGrupoViewModel = null;
                objTelaCadastro = null;
            };
            objTelaCadastro.ShowDialog();
        }

        #endregion Comandos



        #region Eventos



        #endregion Eventos



        #region Métodos

        private void CarregaComboGrupo()
        {
            Retorno objRetorno;
            using (var objBLL = new ProdutoGrupos())
            {
                objRetorno = objBLL.RetornaListaProdutoGrupo();
            }
            if (objRetorno.intCodigoErro == 0)
                arrProdutoGrupo = (List<tbProdutoGrupo>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}