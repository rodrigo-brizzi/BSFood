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
    public class FormaPagamentoViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand NovoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand LogCommand { get; set; }

        public FormaPagamentoViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            EditarCommand = new DelegateCommand(Editar, CanEditar);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ExcluirCommand = new DelegateCommand(Excluir, CanExcluir);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);
            LogCommand = new DelegateCommand(Log, CanLog);
            CarregaComboFormaPagamentoTipo();
            Pesquisar(0);
        }


        #region Propriedades

        private string _strFpgCodigoPesquisa;
        public string strFpgCodigoPesquisa
        {
            get { return _strFpgCodigoPesquisa; }
            set
            {
                _strFpgCodigoPesquisa = value;
            }
        }

        private string _strFpgDescricaoPesquisa;
        public string strFpgDescricaoPesquisa
        {
            get { return _strFpgDescricaoPesquisa; }
            set
            {
                _strFpgDescricaoPesquisa = value;
            }
        }

        [Required(ErrorMessage = "Nome da FormaPagamento obrigatório")]
        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string fpg_descricao
        {
            get { return objFormaPagamento == null ? string.Empty : objFormaPagamento.fpg_descricao; }
            set
            {
                if (objFormaPagamento.fpg_descricao != value)
                {
                    objFormaPagamento.fpg_descricao = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Tipo da forma obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Tipo da forma obrigatório")]
        public int fpt_codigo
        {
            get { return objFormaPagamento == null ? 0 : objFormaPagamento.fpt_codigo; }
            set
            {
                if (objFormaPagamento.fpt_codigo != value)
                {
                    objFormaPagamento.fpt_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        private tbFormaPagamento _objFormaPagamento;
        public tbFormaPagamento objFormaPagamento
        {
            get { return _objFormaPagamento; }
            set
            {
                _objFormaPagamento = value;
                RaisePropertyChanged(null);
            }
        }

        private List<tbFormaPagamento> _arrFormaPagamentoPesquisa;
        public List<tbFormaPagamento> arrFormaPagamentoPesquisa
        {
            get { return _arrFormaPagamentoPesquisa; }
            set
            {
                _arrFormaPagamentoPesquisa = value;
                RaisePropertyChanged();
                if (_arrFormaPagamentoPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
            }
        }

        public List<tbAuditoria> arrAuditoria { get; set; }

        private List<tbFormaPagamentoTipo> _arrFormaPagamentoTipo;
        public List<tbFormaPagamentoTipo> arrFormaPagamentoTipo
        {
            get { return _arrFormaPagamentoTipo; }
            set
            {
                _arrFormaPagamentoTipo = value;
                RaisePropertyChanged();
            }
        }

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
            tbFormaPagamento objFormaPagamentoAux = new tbFormaPagamento();
            objFormaPagamento = objFormaPagamentoAux;
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
                using (var objBLL = new FormaPagamento())
                {
                    objRetorno = objBLL.RetornaFormaPagamento((int)objParam, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFormaPagamento = (tbFormaPagamento)objRetorno.objRetorno;
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
                using (var objBLL = new FormaPagamento())
                {
                    objRetorno = objBLL.SalvarFormaPagamento(objFormaPagamento, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFormaPagamento = null;
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
                objFormaPagamento = null;
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
                    using (var objBLL = new FormaPagamento())
                    {
                        objRetorno = objBLL.ExcluirFormaPagamento((int)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objFormaPagamento = null;
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
            if (objParam != null && objParam.GetType() == typeof(tbFormaPagamento))
            {
                if (base.blnJanela)
                {
                    _objFormaPagamento = (tbFormaPagamento)objParam;
                    Dispose();
                }
            }
            else
            {
                int intSkip;
                if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                    intSkip = 0;

                Retorno objRetorno;
                using (var objBLL = new FormaPagamento())
                {
                    objRetorno = objBLL.RetornaListaFormaPagamento(strFpgCodigoPesquisa, strFpgDescricaoPesquisa, intSkip, base.intQtdeRegPagina);
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
                    arrFormaPagamentoPesquisa = (List<tbFormaPagamento>)objRetorno.objRetorno;
                    if (arrFormaPagamentoPesquisa.Count() == 0)
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
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao && objFormaPagamento != null && objFormaPagamento.fpg_codigo > 0;
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
                        objRetorno = objBll.RetornaListaAuditoria(objFormaPagamento.fpg_codigo, objFormaPagamento);
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

        #endregion Comandos



        #region Eventos



        #endregion Eventos



        #region Métodos

        private void CarregaComboFormaPagamentoTipo()
        {
            Retorno objRetorno;
            using (var objBLL = new FormaPagamento())
            {
                objRetorno = objBLL.RetornaListaFormaPagamentoTipo();
            }
            if (objRetorno.intCodigoErro == 0)
                arrFormaPagamentoTipo = (List<tbFormaPagamentoTipo>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}