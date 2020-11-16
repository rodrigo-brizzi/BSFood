using BSFood.View;
using BSFood.Apoio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class MesaPedidoViewModel : TelaViewModel
    {
        public ICommand SalvarCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand FuncionarioCommand { get; set; }
        public ICommand FormaPagamentoCommand { get; set; }
        public ICommand AdicionaProdutoCommand { get; set; }
        public ICommand ImprimirCupomCommand { get; set; }
        public ICommand CaixaCommand { get; set; }


        public MesaPedidoViewModel()
        {
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            LogCommand = new DelegateCommand(Log);
            FuncionarioCommand = new DelegateCommand(Funcionario);
            FormaPagamentoCommand = new DelegateCommand(FormaPagamento);
            AdicionaProdutoCommand = new DelegateCommand(AdicionaProduto);
            ImprimirCupomCommand = new DelegateCommand(ImprimirCupom);
            CaixaCommand = new DelegateCommand(Caixa);
            Caixa(null);
        }


        #region Propriedades


        public int? fun_codigo
        {
            get
            {
                if (objPedido == null || objPedido.fun_codigo == 0)
                    return null;
                else
                    return objPedido.fun_codigo;
            }
            set
            {
                if (objPedido.fun_codigo != value)
                {
                    objPedido.fun_codigo = value == null ? 0 : (int)value;
                    Funcionario(objPedido.fun_codigo);
                }
            }
        }
        public string fun_nome
        {
            get { return objPedido == null ? string.Empty : objPedido.tbFuncionario.fun_nome; }
            set
            {
                if (objPedido.tbFuncionario.fun_nome != value)
                {
                    objPedido.tbFuncionario.fun_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Forma de pagamento obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Forma de pagamento obrigatória")]
        public int? fpg_codigo
        {
            get
            {
                if (objPedido == null || objPedido.fpg_codigo == 0)
                    return null;
                else
                    return objPedido.fpg_codigo;
            }
            set
            {
                if (objPedido.fpg_codigo != value)
                {
                    objPedido.fpg_codigo = value == null ? 0 : (int)value;
                    FormaPagamento(objPedido.fpg_codigo);
                }
            }
        }
        public string fpg_descricao
        {
            get { return objPedido == null ? string.Empty : objPedido.tbFormaPagamento.fpg_descricao; }
            set
            {
                if (objPedido.tbFormaPagamento.fpg_descricao != value)
                {
                    objPedido.tbFormaPagamento.fpg_descricao = value;
                    RaisePropertyChanged();
                }
            }
        }

        private tbPedido _objPedido;
        public tbPedido objPedido
        {
            get { return _objPedido; }
            set
            {
                _objPedido = value;
                if (_objPedido != null)
                {
                    ObservableCollection<EntregaPedidoProdutoViewModel> arrEntregaPedidoProdutoViewModelAux = new ObservableCollection<EntregaPedidoProdutoViewModel>();
                    foreach (tbPedidoProduto objPedidoProduto in _objPedido.tbPedidoProduto)
                    {
                        EntregaPedidoProdutoViewModel objEntregaPedidoProdutoViewModel = new EntregaPedidoProdutoViewModel(objPedidoProduto);
                        objEntregaPedidoProdutoViewModel.OnDispose += objEntregaPedidoProdutoViewModel_OnDispose;
                        objEntregaPedidoProdutoViewModel.PropertyChanged += objEntregaPedidoProdutoViewModel_PropertyChanged;
                        arrEntregaPedidoProdutoViewModelAux.Add(objEntregaPedidoProdutoViewModel);
                    }
                    _arrEntregaPedidoProdutoViewModel = arrEntregaPedidoProdutoViewModelAux;

                    var objCaixa = arrCaixa.FirstOrDefault(cai => cai.fun_codigo == FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                    if (objCaixa != null)
                        _objPedido.cai_codigo = objCaixa.cai_codigo;
                }
                else
                {
                    _arrEntregaPedidoProdutoViewModel = null;
                }

                //Prepara propriedades da viewmodel
                RaisePropertyChanged(null);
            }
        }

        private ObservableCollection<EntregaPedidoProdutoViewModel> _arrEntregaPedidoProdutoViewModel;
        public ObservableCollection<EntregaPedidoProdutoViewModel> arrEntregaPedidoProdutoViewModel
        {
            get { return _arrEntregaPedidoProdutoViewModel; }
            set
            {
                _arrEntregaPedidoProdutoViewModel = value;
                RaisePropertyChanged();
            }
        }

        [Required(ErrorMessage = "Caixa obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Caixa obrigatório")]
        public int cai_codigo
        {
            get { return objPedido == null ? 0 : objPedido.cai_codigo; }
            set
            {
                if (objPedido.cai_codigo != value)
                {
                    objPedido.cai_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ped_valorSubTotal
        {
            get { return objPedido == null ? 0 : objPedido.ped_valorSubTotal; }
            set
            {
                if (objPedido.ped_valorSubTotal != value)
                {
                    objPedido.ped_valorSubTotal = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ped_valorDespesa
        {
            get { return objPedido == null ? 0 : objPedido.ped_valorDespesa; }
            set
            {
                if (objPedido.ped_valorDespesa != value)
                {
                    objPedido.ped_valorDespesa = value;
                    CalculaValores();
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ped_valorDesconto
        {
            get { return objPedido == null ? 0 : objPedido.ped_valorDesconto; }
            set
            {
                if (objPedido.ped_valorDesconto != value)
                {
                    objPedido.ped_valorDesconto = value;
                    CalculaValores();
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Valor recebido obrigatório")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Valor recebido inválido")]
        public decimal ped_valorRecebido
        {
            get { return objPedido == null ? 0 : objPedido.ped_valorRecebido; }
            set
            {
                if (objPedido.ped_valorRecebido != value)
                {
                    objPedido.ped_valorRecebido = value;
                    CalculaValores();
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ped_valorTroco
        {
            get { return objPedido == null ? 0 : objPedido.ped_valorTroco; }
            set
            {
                if (objPedido.ped_valorTroco != value)
                {
                    objPedido.ped_valorTroco = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ped_valorTotal
        {
            get { return objPedido == null ? 0 : objPedido.ped_valorTotal; }
            set
            {
                if (objPedido.ped_valorTotal != value)
                {
                    objPedido.ped_valorTotal = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(250, ErrorMessage = "É permitido apenas 250 caracteres")]
        public string ped_observacao
        {
            get { return objPedido == null ? string.Empty : objPedido.ped_observacao; }
            set
            {
                if (objPedido.ped_observacao != value)
                {
                    objPedido.ped_observacao = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(250, ErrorMessage = "É permitido apenas 250 caracteres")]
        public string ped_motivoCancelamento
        {
            get { return objPedido == null ? string.Empty : objPedido.ped_motivoCancelamento; }
            set
            {
                if (objPedido.ped_motivoCancelamento != value)
                {
                    objPedido.ped_motivoCancelamento = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<tbAuditoria> arrAuditoria { get; set; }

        private List<tbCaixa> _arrCaixa;
        public List<tbCaixa> arrCaixa
        {
            get { return _arrCaixa; }
            set
            {
                _arrCaixa = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private bool CanSalvar(object objParam)
        {
            return _objPedido != null;
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

            bool blnAchouErroProduto = false;
            foreach (EntregaPedidoProdutoViewModel objEntregaPedidoProdutoViewModel in arrEntregaPedidoProdutoViewModel)
            {
                objEntregaPedidoProdutoViewModel.Validate();
                blnAchouErroProduto = objEntregaPedidoProdutoViewModel.HasErrors;
                if (blnAchouErroProduto)
                    break;
            }

            Validate();
            if (!HasErrors && !blnAchouErroProduto)
            {
                bool blnContinuar = false;
                if (objPedido.ped_formaPagamentoTipo == (int)enFormaPagamentoTipo.Convenio && objPedido.ped_valorTotal > objPedido.tbCliente.cli_limiteCredito)
                {
                    if (MessageBox.Show("Limite de credito excedido, deseja continuar?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        blnContinuar = true;
                }
                else
                    blnContinuar = true;

                if (blnContinuar)
                {
                    objPedido.tbPedidoProduto.Clear();
                    foreach (EntregaPedidoProdutoViewModel objEntregaPedidoProdutoViewModel in arrEntregaPedidoProdutoViewModel)
                        objPedido.tbPedidoProduto.Add(objEntregaPedidoProdutoViewModel.objPedidoProduto);

                    Retorno objRetorno;
                    using (var objBLL = new Pedidos())
                    {
                        objRetorno = objBLL.FecharPedidoMesa(objPedido);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        ImprimirCupom(objPedido);
                        objPedido = null;
                        ClearAllErrorsAsync();
                        Dispose();
                    }
                    else
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
            }
        }

        private void Log(object objParam)
        {
            if (objParam != null)
            {
                if (objParam.ToString() == "AbrirTela")
                {
                    Retorno objRetorno;
                    using (Auditoria objBLL = new Auditoria())
                    {
                        objRetorno = objBLL.RetornaListaAuditoria(objPedido.ped_codigo, objPedido);
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

        private void Funcionario(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                if (objParam.GetType() == typeof(tbFuncionario))
                {
                    if (((tbFuncionario)objParam).fun_codigo > 0)
                    {
                        objPedido.fun_codigo = ((tbFuncionario)objParam).fun_codigo;
                        objPedido.tbFuncionario.fun_nome = ((tbFuncionario)objParam).fun_nome;
                    }
                    else
                    {
                        objPedido.fun_codigo = 0;
                        objPedido.tbFuncionario.fun_nome = string.Empty;
                    }
                    RaisePropertyChanged("fun_codigo");
                    RaisePropertyChanged("fun_nome");
                }
                else if (objParam.ToString() == "Pesquisar")
                {
                    winCadastro objTelaCadastro = new winCadastro();
                    FuncionarioViewModel objFuncionarioViewModel = new FuncionarioViewModel();
                    objFuncionarioViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
                    objFuncionarioViewModel.blnJanela = true;
                    objTelaCadastro.Title = "Cadastro - " + objFuncionarioViewModel.strNomeTela;
                    objTelaCadastro.DataContext = objFuncionarioViewModel;
                    objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
                    objTelaCadastro.Closed += (sen, eve) =>
                    {
                        Funcionario(objFuncionarioViewModel.objFuncionario);
                        objFuncionarioViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new Funcionarios())
                    {
                        objRetorno = objBLL.RetornaFuncionario(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        Funcionario((tbFuncionario)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        Funcionario(new tbFuncionario());
                    }
                }
                else
                    Funcionario("Pesquisar");
            }
        }

        private void FormaPagamento(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                if (objParam.GetType() == typeof(tbFormaPagamento))
                {
                    if (((tbFormaPagamento)objParam).fpg_codigo > 0)
                    {
                        objPedido.fpg_codigo = ((tbFormaPagamento)objParam).fpg_codigo;
                        objPedido.tbFormaPagamento.fpg_descricao = ((tbFormaPagamento)objParam).fpg_descricao;
                        objPedido.ped_cobranca = ((tbFormaPagamento)objParam).tbFormaPagamentoTipo.fpt_cobranca;
                        objPedido.ped_formaPagamentoTipo = ((tbFormaPagamento)objParam).fpt_codigo;
                        objPedido.ped_formaPagamentoDescricao = ((tbFormaPagamento)objParam).fpg_descricao;
                    }
                    else
                    {
                        objPedido.fpg_codigo = 0;
                        objPedido.tbFormaPagamento.fpg_descricao = string.Empty;
                        objPedido.ped_cobranca = string.Empty;
                        objPedido.ped_formaPagamentoTipo = 0;
                        objPedido.ped_formaPagamentoDescricao = string.Empty;
                    }
                    RaisePropertyChanged("fpg_codigo");
                    RaisePropertyChanged("fpg_descricao");
                }
                else if (objParam.ToString() == "Pesquisar")
                {
                    winCadastro objTelaCadastro = new winCadastro();
                    FormaPagamentoViewModel objFormaPagamentoViewModel = new FormaPagamentoViewModel();
                    objFormaPagamentoViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
                    objFormaPagamentoViewModel.blnJanela = true;
                    objTelaCadastro.Title = "Cadastro - " + objFormaPagamentoViewModel.strNomeTela;
                    objTelaCadastro.DataContext = objFormaPagamentoViewModel;
                    objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
                    objTelaCadastro.Closed += (sen, eve) =>
                    {
                        FormaPagamento(objFormaPagamentoViewModel.objFormaPagamento);
                        objFormaPagamentoViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new FormaPagamento())
                    {
                        objRetorno = objBLL.RetornaFormaPagamento(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        FormaPagamento((tbFormaPagamento)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        FormaPagamento(new tbFormaPagamento());
                    }
                }
                else
                    FormaPagamento("Pesquisar");
            }
        }

        private void AdicionaProduto(object objParam)
        {
            tbPedidoProduto objPedidoProduto = new tbPedidoProduto();
            objPedidoProduto.tbProduto = new tbProduto();
            EntregaPedidoProdutoViewModel objEntregaPedidoProdutoViewModel = new EntregaPedidoProdutoViewModel(objPedidoProduto);
            objEntregaPedidoProdutoViewModel.blnCodigoFocus = true;
            objEntregaPedidoProdutoViewModel.OnDispose += objEntregaPedidoProdutoViewModel_OnDispose;
            objEntregaPedidoProdutoViewModel.PropertyChanged += objEntregaPedidoProdutoViewModel_PropertyChanged;
            arrEntregaPedidoProdutoViewModel.Add(objEntregaPedidoProdutoViewModel);
        }

        private void ImprimirCupom(object objParam)
        {
            try
            {
                if (objParam != null)
                {
                    Retorno objRetorno;
                    using (Relatorios objBLL = new Relatorios())
                    {
                        objRetorno = objBLL.RetornaTicketConferencia((tbPedido)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        string strConteudo = (string)objRetorno.objRetorno;
                        string[] arrLinhas = strConteudo.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        FrameworkUtil.objGerenciaCupom.AbreRelatorio();
                        for (int i = 0; i < arrLinhas.Length; i++)
                        {
                            FrameworkUtil.objGerenciaCupom.LinhaRelatorio(arrLinhas[i]);
                        }
                        if (string.IsNullOrWhiteSpace(FrameworkUtil.objConfigStorage.objConfiguracao.cfg_impressoraEntrega))
                            MessageBox.Show("Impressora não configurada!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                            FrameworkUtil.objGerenciaCupom.FechaRelatorio(FrameworkUtil.objConfigStorage.objConfiguracao.cfg_impressoraEntrega);
                    }
                    else
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Caixa(object objParam)
        {
            Retorno objRetorno;
            using (var objBLL = new Caixa())
            {
                objRetorno = objBLL.RetornaListaCaixaAberto();
            }
            if (objRetorno.intCodigoErro == 0)
                arrCaixa = (List<tbCaixa>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Comandos



        #region Eventos

        void objEntregaPedidoProdutoViewModel_OnDispose(object sender, EventArgs e)
        {
            arrEntregaPedidoProdutoViewModel.Remove((EntregaPedidoProdutoViewModel)sender);
            CalculaValores();
        }

        void objEntregaPedidoProdutoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ppr_quantidade")
                CalculaValores();
        }

        #endregion Eventos



        #region Métodos

        private void CalculaValores()
        {
            ped_valorSubTotal = arrEntregaPedidoProdutoViewModel.Sum(ppr => ppr.ppr_valorTotal);
            ped_valorTotal = (ped_valorSubTotal + ped_valorDespesa) - ped_valorDesconto;
            if ((ped_valorRecebido - ped_valorTotal) > 0)
                ped_valorTroco = ped_valorRecebido - ped_valorTotal;
            else
                ped_valorTroco = 0;
        }

        #endregion Métodos
    }
}