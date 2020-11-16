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
    public class FuncionarioViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand NovoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand PerfilAcessoCommand { get; set; }
        public ICommand CidadeCommand { get; set; }


        public FuncionarioViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            EditarCommand = new DelegateCommand(Editar, CanEditar);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ExcluirCommand = new DelegateCommand(Excluir, CanExcluir);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);
            LogCommand = new DelegateCommand(Log, CanLog);
            PerfilAcessoCommand = new DelegateCommand(PerfilAcesso);
            CidadeCommand = new DelegateCommand(Cidade);
            CarregaComboEstado();
            Pesquisar(0);
        }


        #region Propriedades

        private string _strFunCodigoPesquisa;
        public string strFunCodigoPesquisa
        {
            get { return _strFunCodigoPesquisa; }
            set
            {
                _strFunCodigoPesquisa = value;
            }
        }

        private string _strFunNomePesquisa;
        public string strFunNomePesquisa
        {
            get { return _strFunNomePesquisa; }
            set
            {
                _strFunNomePesquisa = value;
            }
        }

        [Required(ErrorMessage = "Nome do Funcionario obrigatório")]
        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string fun_nome
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_nome; }
            set
            {
                if (objFuncionario.fun_nome != value)
                {
                    objFuncionario.fun_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Login obrigatório")]
        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        public string fun_login
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_login; }
            set
            {
                if (objFuncionario.fun_login != value)
                {
                    objFuncionario.fun_login = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Senha obrigatório")]
        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        public string fun_senha
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_senha; }
            set
            {
                if (objFuncionario.fun_senha != value)
                {
                    objFuncionario.fun_senha = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(200, ErrorMessage = "É permitido apenas 200 caracteres")]
        public string fun_logradouro
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_logradouro; }
            set
            {
                if (objFuncionario.fun_logradouro != value)
                {
                    objFuncionario.fun_logradouro = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(10, ErrorMessage = "É permitido apenas 10 caracteres")]
        public string fun_numero
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_numero; }
            set
            {
                if (objFuncionario.fun_numero != value)
                {
                    objFuncionario.fun_numero = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string fun_bairro
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_bairro; }
            set
            {
                if (objFuncionario.fun_bairro != value)
                {
                    objFuncionario.fun_bairro = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(9, ErrorMessage = "É permitido apenas 9 caracteres")]
        public string fun_cep
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_cep; }
            set
            {
                if (objFuncionario.fun_cep != value)
                {
                    objFuncionario.fun_cep = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(150, ErrorMessage = "É permitido apenas 150 caracteres")]
        public string fun_complemento
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_complemento; }
            set
            {
                if (objFuncionario.fun_complemento != value)
                {
                    objFuncionario.fun_complemento = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Estado obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Estado obrigatório")]
        public int est_codigo
        {
            get { return objFuncionario == null ? 0 : objFuncionario.est_codigo; }
            set
            {
                if (objFuncionario.est_codigo != value)
                {
                    objFuncionario.est_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Cidade obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Cidade obrigatório")]
        public int cid_codigo
        {
            get
            {
                if (objFuncionario == null)
                    return 0;
                else
                {
                    RaisePropertyChanged("est_codigo", false);
                    return objFuncionario.cid_codigo;
                }
            }
            set
            {
                if (objFuncionario.cid_codigo != value)
                {
                    objFuncionario.cid_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        public string fun_telefone
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_telefone; }
            set
            {
                if (objFuncionario.fun_telefone != value)
                {
                    objFuncionario.fun_telefone = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        public string fun_celular
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.fun_celular; }
            set
            {
                if (objFuncionario.fun_celular != value)
                {
                    objFuncionario.fun_celular = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Perfil Acesso obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Perfil Acesso obrigatório")]
        public int? pac_codigo
        {
            get
            {
                if (objFuncionario == null || objFuncionario.pac_codigo == 0)
                    return null;
                else
                    return objFuncionario.pac_codigo;
            }
            set
            {
                if (objFuncionario.pac_codigo != value)
                {
                    objFuncionario.pac_codigo = value == null ? 0 : (int)value;
                    PerfilAcesso(objFuncionario.pac_codigo);
                }
            }
        }

        public string pac_descricao
        {
            get { return objFuncionario == null ? string.Empty : objFuncionario.tbPerfilAcesso.pac_descricao; }
            set
            {
                if (objFuncionario.tbPerfilAcesso.pac_descricao != value)
                {
                    objFuncionario.tbPerfilAcesso.pac_descricao = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _blnLoginFocus;
        public bool blnLoginFocus
        {
            get { return _blnLoginFocus; }
            set
            {
                _blnLoginFocus = value;
                RaisePropertyChanged();
            }
        }

        private tbFuncionario _objFuncionario;
        public tbFuncionario objFuncionario
        {
            get { return _objFuncionario; }
            set
            {
                _objFuncionario = value;
                RaisePropertyChanged(null);
            }
        }

        private List<tbFuncionario> _arrFuncionarioPesquisa;
        public List<tbFuncionario> arrFuncionarioPesquisa
        {
            get { return _arrFuncionarioPesquisa; }
            set
            {
                _arrFuncionarioPesquisa = value;
                RaisePropertyChanged();
                if (_arrFuncionarioPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
            }
        }

        private List<tbEstado> _arrEstado;
        public List<tbEstado> arrEstado
        {
            get { return _arrEstado; }
            set
            {
                _arrEstado = value;
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
            tbFuncionario objFuncionarioAux = new tbFuncionario();
            objFuncionarioAux.tbPerfilAcesso = new tbPerfilAcesso();
            objFuncionarioAux.est_codigo = FrameworkUtil.objConfigStorage.objEmpresa.est_codigo;
            objFuncionarioAux.cid_codigo = FrameworkUtil.objConfigStorage.objEmpresa.cid_codigo;
            objFuncionario = objFuncionarioAux;
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
                using (var objBLL = new Funcionarios())
                {
                    objRetorno = objBLL.RetornaFuncionario((int)objParam, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFuncionario = (tbFuncionario)objRetorno.objRetorno;
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
                using (var objBLL = new Funcionarios())
                {
                    objRetorno = objBLL.SalvarFuncionario(objFuncionario, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFuncionario = null;
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
                objFuncionario = null;
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
                    using (var objBLL = new Funcionarios())
                    {
                        objRetorno = objBLL.ExcluirFuncionario((int)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objFuncionario = null;
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
            if (objParam != null && objParam.GetType() == typeof(tbFuncionario))
            {
                if (base.blnJanela)
                {
                    _objFuncionario = (tbFuncionario)objParam;
                    Dispose();
                }
            }
            else
            {
                int intSkip;
                if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                    intSkip = 0;

                Retorno objRetorno;
                using (var objBLL = new Funcionarios())
                {
                    objRetorno = objBLL.RetornaListaFuncionario(strFunCodigoPesquisa, strFunNomePesquisa, intSkip, base.intQtdeRegPagina);
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
                    arrFuncionarioPesquisa = (List<tbFuncionario>)objRetorno.objRetorno;
                    if (arrFuncionarioPesquisa.Count() == 0)
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
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao && objFuncionario != null && objFuncionario.fun_codigo > 0;
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
                        objRetorno = objBll.RetornaListaAuditoria(objFuncionario.fun_codigo, objFuncionario);
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

        private void PerfilAcesso(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                blnLoginFocus = false;
                if (objParam.GetType() == typeof(tbPerfilAcesso))
                {
                    if (((tbPerfilAcesso)objParam).pac_codigo > 0)
                    {
                        objFuncionario.pac_codigo = ((tbPerfilAcesso)objParam).pac_codigo;
                        objFuncionario.tbPerfilAcesso.pac_descricao = ((tbPerfilAcesso)objParam).pac_descricao;
                        _blnLoginFocus = true;
                    }
                    else
                    {
                        objFuncionario.pac_codigo = 0;
                        objFuncionario.tbPerfilAcesso.pac_descricao = string.Empty;
                    }
                    RaisePropertyChanged("pac_codigo");
                    RaisePropertyChanged("pac_descricao");
                    RaisePropertyChanged("blnLoginFocus");
                }
                else if (objParam.ToString() == "Pesquisar")
                {
                    winCadastro objTelaCadastro = new winCadastro();
                    PerfilAcessoViewModel objPerfilAcessoViewModel = new PerfilAcessoViewModel();
                    objPerfilAcessoViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
                    objPerfilAcessoViewModel.blnJanela = true;
                    objTelaCadastro.Title = "Cadastro - " + objPerfilAcessoViewModel.strNomeTela;
                    objTelaCadastro.DataContext = objPerfilAcessoViewModel;
                    objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
                    objTelaCadastro.Closed += (sen, eve) =>
                    {
                        PerfilAcesso(objPerfilAcessoViewModel.objPerfilAcesso);
                        objPerfilAcessoViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new PerfilAcesso())
                    {
                        objRetorno = objBLL.RetornaPerfilAcesso(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        PerfilAcesso((tbPerfilAcesso)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        PerfilAcesso(new tbPerfilAcesso());
                    }
                }
                else
                    PerfilAcesso("Pesquisar");
            }
        }

        private void Cidade(object objParam)
        {
            winCadastro objTelaCadastro = new winCadastro();
            CidadeViewModel objCidadeViewModel = new CidadeViewModel();
            objCidadeViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
            objCidadeViewModel.blnJanela = true;
            objTelaCadastro.Title = "Cadastro - " + objCidadeViewModel.strNomeTela;
            objTelaCadastro.DataContext = objCidadeViewModel;
            objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
            objTelaCadastro.Closed += (sen, eve) =>
            {
                if (objCidadeViewModel.objCidade != null)
                {
                    CarregaComboEstado();
                    objFuncionario.est_codigo = (int)objCidadeViewModel.objCidade.est_codigo;
                    RaisePropertyChanged("est_codigo");
                    objFuncionario.cid_codigo = (int)objCidadeViewModel.objCidade.cid_codigo;
                    RaisePropertyChanged("cid_codigo");
                }
                objCidadeViewModel = null;
                objTelaCadastro = null;
            };
            objTelaCadastro.ShowDialog();
        }

        #endregion Comandos



        #region Eventos



        #endregion Eventos



        #region Métodos

        private void CarregaComboEstado()
        {
            Retorno objRetorno;
            using (var objBLL = new Cidades())
            {
                objRetorno = objBLL.RetornaListaEstado();
            }
            if (objRetorno.intCodigoErro == 0)
                arrEstado = (List<tbEstado>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}