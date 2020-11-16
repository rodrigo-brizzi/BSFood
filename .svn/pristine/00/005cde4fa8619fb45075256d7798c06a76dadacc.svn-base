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
    public class ComandaViewModel : TelaViewModel
    {
        public ICommand NovoCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand FuncionarioCommand { get; set; }
        public ICommand AdicionaProdutoCommand { get; set; }
        public ICommand CaixaCommand { get; set; }
        public ICommand MesaCommand { get; set; }


        public ComandaViewModel()
        {
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            FuncionarioCommand = new DelegateCommand(Funcionario);
            AdicionaProdutoCommand = new DelegateCommand(AdicionaProduto);
            CaixaCommand = new DelegateCommand(Caixa);
            MesaCommand = new DelegateCommand(Mesa);
            Caixa(null);
        }


        #region Propriedades

        [Required(ErrorMessage = "Número da mesa obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Número da mesa obrigatório")]
        public int? ped_numeroMesa
        {
            get
            {
                if (objPedido == null || objPedido.ped_numeroMesa == 0)
                    return null;
                else
                    return objPedido.ped_numeroMesa;
            }
            set
            {
                if (objPedido.ped_numeroMesa != value)
                    Mesa(value);
            }
        }

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

        private bool CanNovo(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteInclusaoRegistro;
        }
        private void Novo(object objParam)
        {
            tbPedido objPedidoAux = new tbPedido();
            objPedidoAux.cli_codigo = FrameworkUtil.objConfigStorage.objConfiguracao.cli_codigo;
            objPedidoAux.fpg_codigo = FrameworkUtil.objConfigStorage.objConfiguracao.fpg_codigo;

            objPedidoAux.tbFuncionario = new tbFuncionario();

            objPedidoAux.tbPedidoProduto = new List<tbPedidoProduto>();

            tbPedidoProduto objPedidoProduto = new tbPedidoProduto();
            objPedidoProduto.tbProduto = new tbProduto();
            objPedidoAux.tbPedidoProduto.Add(objPedidoProduto);

            var objCaixa = arrCaixa.FirstOrDefault(cai => cai.fun_codigo == FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
            if (objCaixa != null)
                objPedidoAux.cai_codigo = objCaixa.cai_codigo;

            objPedido = objPedidoAux;
            base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
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
                objPedido.tbPedidoProduto.Clear();
                foreach (EntregaPedidoProdutoViewModel objEntregaPedidoProdutoViewModel in arrEntregaPedidoProdutoViewModel)
                    objPedido.tbPedidoProduto.Add(objEntregaPedidoProdutoViewModel.objPedidoProduto);

                Retorno objRetorno;
                using (var objBLL = new Pedidos())
                {
                    objRetorno = objBLL.SalvarPedidoComanda(objPedido, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    ImprimirCupom((tbPedido)objRetorno.objRetorno);
                    objPedido = null;
                    ClearAllErrorsAsync();
                    base.enStatusTelaAtual = enStatusTela.Navegacao;
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
                objPedido = null;
                ClearAllErrorsAsync();
                base.enStatusTelaAtual = enStatusTela.Navegacao;
            }
        }

        private void Mesa(object objParam)
        {
            if (objParam != null)
            {
                if (objParam.GetType() == typeof(tbPedido))
                {
                    tbPedido objPedidoAux = (tbPedido)objParam;
                    objPedidoAux.tbPedidoProduto = new List<tbPedidoProduto>();

                    tbPedidoProduto objPedidoProduto = new tbPedidoProduto();
                    objPedidoProduto.tbProduto = new tbProduto();
                    objPedidoAux.tbPedidoProduto.Add(objPedidoProduto);

                    objPedido = objPedidoAux;
                }
                else
                {
                    int intNumero;
                    if (int.TryParse(objParam.ToString(), out intNumero))
                    {
                        objPedido.ped_numeroMesa = intNumero;

                        Retorno objRetorno;
                        using (var objBLL = new Pedidos())
                        {
                            objRetorno = objBLL.RetornaPedidoComanda(intNumero);
                        }
                        if (objRetorno.intCodigoErro == 0)
                        {
                            if(((tbMesa)objRetorno.objRetorno).tbPedido != null)
                                Mesa(((tbMesa)objRetorno.objRetorno).tbPedido);
                        }
                        else
                        {
                            MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                            objPedido.ped_numeroMesa = 0;
                            RaisePropertyChanged("ped_numeroMesa");
                        }
                    }
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
                        objRetorno = objBLL.RetornaCupomComanda((tbPedido)objParam);
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
        }

        #endregion Métodos
    }
}