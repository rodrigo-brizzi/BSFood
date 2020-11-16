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
    public class PerfilAcessoViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand NovoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand HerdarCommand { get; set; }

        public PerfilAcessoViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            EditarCommand = new DelegateCommand(Editar, CanEditar);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ExcluirCommand = new DelegateCommand(Excluir, CanExcluir);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);
            LogCommand = new DelegateCommand(Log, CanLog);
            HerdarCommand = new DelegateCommand(Herdar);
            Pesquisar(0);
        }


        #region Propriedades

        private string _strPacCodigoPesquisa;
        public string strPacCodigoPesquisa
        {
            get { return _strPacCodigoPesquisa; }
            set
            {
                _strPacCodigoPesquisa = value;
            }
        }

        private string _strPacDescricaoPesquisa;
        public string strPacDescricaoPesquisa
        {
            get { return _strPacDescricaoPesquisa; }
            set
            {
                _strPacDescricaoPesquisa = value;
            }
        }

        [Required(ErrorMessage = "Descrição do perfil de acesso obrigatório")]
        [StringLength(30, ErrorMessage = "É permitido apenas 30 caracteres")]
        public string pac_descricao
        {
            get { return objPerfilAcesso == null ? string.Empty : objPerfilAcesso.pac_descricao; }
            set
            {
                if (objPerfilAcesso.pac_descricao != value)
                {
                    objPerfilAcesso.pac_descricao = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<tbPerfilAcesso> _arrPerfilAcessoHerdar;
        public List<tbPerfilAcesso> arrPerfilAcessoHerdar
        {
            get { return _arrPerfilAcessoHerdar; }
            set
            {
                _arrPerfilAcessoHerdar = value;
                RaisePropertyChanged();
            }
        }

        private tbPerfilAcesso _objPerfilAcesso;
        public tbPerfilAcesso objPerfilAcesso
        {
            get { return _objPerfilAcesso; }
            set
            {
                _objPerfilAcesso = value;
                //Prepara a coleção PerfilAcessoMenu
                if (_objPerfilAcesso != null)
                {
                    List<PerfilAcessoMenuViewModel> arrPerfilAcessoMenuViewModelAux = new List<PerfilAcessoMenuViewModel>();
                    foreach (tbPerfilAcessoMenu objPerfilAcessoMenu in _objPerfilAcesso.tbPerfilAcessoMenu)
                        arrPerfilAcessoMenuViewModelAux.Add(new PerfilAcessoMenuViewModel(objPerfilAcessoMenu));
                    _arrPerfilAcessoMenuViewModel = arrPerfilAcessoMenuViewModelAux;
                }
                else
                    _arrPerfilAcessoMenuViewModel = null;

                //Prepara propriedades da viewmodel
                _blnPermiteAlteracaoTodos = false;
                _blnPermiteVisualizacaoTodos = false;
                _blnPermiteExclusaoTodos = false;
                _blnPermiteInclusaoTodos = false;
                _blnToolBarTodos = false;
                _intSelectedIndexTab = 0;
                RaisePropertyChanged(null);
            }
        }

        private List<PerfilAcessoMenuViewModel> _arrPerfilAcessoMenuViewModel;
        public List<PerfilAcessoMenuViewModel> arrPerfilAcessoMenuViewModel
        {
            get { return _arrPerfilAcessoMenuViewModel; }
            set
            {
                _arrPerfilAcessoMenuViewModel = value;
                RaisePropertyChanged();
            }
        }

        private List<tbPerfilAcesso> _arrPerfilAcessoPesquisa;
        public List<tbPerfilAcesso> arrPerfilAcessoPesquisa
        {
            get { return _arrPerfilAcessoPesquisa; }
            set
            {
                _arrPerfilAcessoPesquisa = value;
                RaisePropertyChanged();
                if (_arrPerfilAcessoPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
            }
        }

        private bool _blnPermiteInclusaoTodos;
        public bool blnPermiteInclusaoTodos
        {
            get { return _blnPermiteInclusaoTodos; }
            set
            {
                _blnPermiteInclusaoTodos = value;
                foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel)
                    objPerfilAcessoMenuViewModel.pam_permiteInclusao = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnPermiteAlteracaoTodos;
        public bool blnPermiteAlteracaoTodos
        {
            get { return _blnPermiteAlteracaoTodos; }
            set
            {
                _blnPermiteAlteracaoTodos = value;
                foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel)
                    objPerfilAcessoMenuViewModel.pam_permiteAlteracao = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnPermiteExclusaoTodos;
        public bool blnPermiteExclusaoTodos
        {
            get { return _blnPermiteExclusaoTodos; }
            set
            {
                _blnPermiteExclusaoTodos = value;
                foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel)
                    objPerfilAcessoMenuViewModel.pam_permiteExclusao = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnPermiteVisualizacaoTodos;
        public bool blnPermiteVisualizacaoTodos
        {
            get { return _blnPermiteVisualizacaoTodos; }
            set
            {
                _blnPermiteVisualizacaoTodos = value;
                foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel)
                    objPerfilAcessoMenuViewModel.pam_permiteVisualizacao = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnToolBarTodos;
        public bool blnToolBarTodos
        {
            get { return _blnToolBarTodos; }
            set
            {
                _blnToolBarTodos = value;
                foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel)
                    objPerfilAcessoMenuViewModel.pam_toolBar = value;
                RaisePropertyChanged();
            }
        }

        private int _intSelectedIndexTab;
        public int intSelectedIndexTab
        {
            get { return _intSelectedIndexTab; }
            set
            {
                _intSelectedIndexTab = value;
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
            //Carrega a lista de objetos referente ao combo
            Retorno objRetorno;
            Retorno objRetornoMenu;
            using (var objBLL = new PerfilAcesso())
            {
                objRetorno = objBLL.RetornaListaPerfilAcesso();
                objRetornoMenu = objBLL.RetornaListaMenu();
            }
            if (objRetorno.intCodigoErro == 0)
                arrPerfilAcessoHerdar = (List<tbPerfilAcesso>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));

            //Prepara a Model que será bindada na tela
            if (objRetornoMenu.intCodigoErro == 0)
            {
                tbPerfilAcesso objPerfilAcessoAux = new tbPerfilAcesso();
                objPerfilAcessoAux.tbPerfilAcessoMenu = new List<tbPerfilAcessoMenu>();
                foreach (tbMenu objMenu in (List<tbMenu>)objRetornoMenu.objRetorno)
                {
                    objPerfilAcessoAux.tbPerfilAcessoMenu.Add(new tbPerfilAcessoMenu
                    {
                        pac_codigo = 0,
                        men_codigo = objMenu.men_codigo,
                        pam_permiteAlteracao = false,
                        pam_permiteExclusao = false,
                        pam_permiteInclusao = false,
                        pam_permiteVisualizacao = false,
                        pam_toolBar = false,
                        tbMenu = objMenu
                    });
                }
                objPerfilAcesso = objPerfilAcessoAux;
                base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
            }
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
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
                using (var objBLL = new PerfilAcesso())
                {
                    objRetorno = objBLL.RetornaPerfilAcesso((int)objParam, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objPerfilAcesso = (tbPerfilAcesso)objRetorno.objRetorno;
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
                objPerfilAcesso.tbPerfilAcessoMenu.Clear();
                foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel)
                    objPerfilAcesso.tbPerfilAcessoMenu.Add(objPerfilAcessoMenuViewModel.objPerfilAcessoMenu);

                Retorno objRetorno;
                using (var objBLL = new PerfilAcesso())
                {
                    objRetorno = objBLL.SalvarPerfilAcesso(objPerfilAcesso, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    if (objPerfilAcesso.pac_codigo == FrameworkUtil.objConfigStorage.objPerfilAcesso.pac_codigo)
                    {
                        MessageBox.Show("O perfil do funcionário atual foi alterado, será necessário fechar o sistema!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Util.FecharSistema();
                    }
                    else
                    {
                        objPerfilAcesso = null;
                        ClearAllErrorsAsync();
                        base.enStatusTelaAtual = enStatusTela.Navegacao;
                        Pesquisar(null);
                    }
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
                objPerfilAcesso = null;
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
                    using (var objBLL = new PerfilAcesso())
                    {
                        objRetorno = objBLL.ExcluirPerfilAcesso((int)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objPerfilAcesso = null;
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
            if (objParam != null && objParam.GetType() == typeof(tbPerfilAcesso))
            {
                if (base.blnJanela)
                {
                    _objPerfilAcesso = (tbPerfilAcesso)objParam;
                    Dispose();
                }
            }
            else
            {
                int intSkip;
                if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                    intSkip = 0;

                Retorno objRetorno;
                using (var objBLL = new PerfilAcesso())
                {
                    objRetorno = objBLL.RetornaListaPerfilAcesso(strPacCodigoPesquisa, strPacDescricaoPesquisa, intSkip, base.intQtdeRegPagina);
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
                    arrPerfilAcessoPesquisa = (List<tbPerfilAcesso>)objRetorno.objRetorno;
                    if (arrPerfilAcessoPesquisa.Count() == 0)
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
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao && objPerfilAcesso != null && objPerfilAcesso.pac_codigo > 0;
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
                        objRetorno = objBll.RetornaListaAuditoria(objPerfilAcesso.pac_codigo, objPerfilAcesso);
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

        private void Herdar(object objParam)
        {
            tbPerfilAcesso objPerfilAcessoHerdar = objParam as tbPerfilAcesso;
            if (objPerfilAcessoHerdar != null)
            {
                Retorno objRetorno;
                using (var objBLL = new PerfilAcesso())
                {
                    objRetorno = objBLL.RetornaPerfilAcesso(objPerfilAcessoHerdar.pac_codigo, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objPerfilAcessoHerdar = (tbPerfilAcesso)objRetorno.objRetorno;
                    foreach (tbPerfilAcessoMenu objPerfilAcessoMenuHerdar in objPerfilAcessoHerdar.tbPerfilAcessoMenu)
                    {
                        foreach (PerfilAcessoMenuViewModel objPerfilAcessoMenuViewModel in arrPerfilAcessoMenuViewModel.Where(pam => pam.objPerfilAcessoMenu.tbMenu.men_codigo == objPerfilAcessoMenuHerdar.men_codigo))
                        {
                            objPerfilAcessoMenuViewModel.pam_permiteAlteracao = objPerfilAcessoMenuHerdar.pam_permiteAlteracao;
                            objPerfilAcessoMenuViewModel.pam_permiteInclusao = objPerfilAcessoMenuHerdar.pam_permiteInclusao;
                            objPerfilAcessoMenuViewModel.pam_permiteExclusao = objPerfilAcessoMenuHerdar.pam_permiteExclusao;
                            objPerfilAcessoMenuViewModel.pam_permiteVisualizacao = objPerfilAcessoMenuHerdar.pam_permiteVisualizacao;
                            objPerfilAcessoMenuViewModel.pam_toolBar = objPerfilAcessoMenuHerdar.pam_toolBar;
                        }
                    }
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        #endregion Comandos



        #region Eventos



        #endregion Eventos



        #region Métodos



        #endregion Métodos
    }
}