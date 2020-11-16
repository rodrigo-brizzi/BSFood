using BSFood.Apoio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class CaixaViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand AbrirCaixaCommand { get; set; }
        public ICommand LancarMovimentoCommand { get; set; }
        public ICommand FecharCaixaCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }

        public CaixaViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            AbrirCaixaCommand = new DelegateCommand(AbrirCaixa, CanAbrirCaixa);
            LancarMovimentoCommand = new DelegateCommand(LancarMovimento, CanLancarMovimento);
            FecharCaixaCommand = new DelegateCommand(FecharCaixa, CanFecharCaixa);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);

            objCaixaAberturaViewModel = new CaixaAberturaViewModel();
            objCaixaMovimentoViewModel = new CaixaMovimentoViewModel(null);
            objCaixaFechamentoViewModel = new CaixaFechamentoViewModel();
            strCaiStatusPesquisa = "T";
        }


        #region Propriedades

        private string _strCaiCodigoPesquisa;
        public string strCaiCodigoPesquisa
        {
            get { return _strCaiCodigoPesquisa; }
            set
            {
                _strCaiCodigoPesquisa = value;
            }
        }

        private string _strCaiStatusPesquisa;
        public string strCaiStatusPesquisa
        {
            get { return _strCaiStatusPesquisa; }
            set
            {
                _strCaiStatusPesquisa = value;
                RaisePropertyChanged();
                Pesquisar(0);
            }
        }

        private string _strCaiFuncionarioPesquisa;
        public string strCaiFuncionarioPesquisa
        {
            get { return _strCaiFuncionarioPesquisa; }
            set
            {
                _strCaiFuncionarioPesquisa = value;
            }
        }

        private DateTime? _dtCaiDataAberturaPesquisa;
        public DateTime? dtCaiDataAberturaPesquisa
        {
            get { return _dtCaiDataAberturaPesquisa; }
            set
            {
                _dtCaiDataAberturaPesquisa = value;
            }
        }

        private List<tbCaixa> _arrCaixaPesquisa;
        public List<tbCaixa> arrCaixaPesquisa
        {
            get { return _arrCaixaPesquisa; }
            set
            {
                _arrCaixaPesquisa = value;
                RaisePropertyChanged();
                if (_arrCaixaPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
            }
        }

        private CaixaAberturaViewModel _objCaixaAberturaViewModel;
        public CaixaAberturaViewModel objCaixaAberturaViewModel
        {
            get { return _objCaixaAberturaViewModel; }
            set
            {
                _objCaixaAberturaViewModel = value;
                RaisePropertyChanged();
            }
        }

        private CaixaMovimentoViewModel _objCaixaMovimentoViewModel;
        public CaixaMovimentoViewModel objCaixaMovimentoViewModel
        {
            get { return _objCaixaMovimentoViewModel; }
            set
            {
                _objCaixaMovimentoViewModel = value;
                RaisePropertyChanged();
            }
        }

        private CaixaFechamentoViewModel _objCaixaFechamentoViewModel;
        public CaixaFechamentoViewModel objCaixaFechamentoViewModel
        {
            get { return _objCaixaFechamentoViewModel; }
            set
            {
                _objCaixaFechamentoViewModel = value;
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

        private bool CanAbrirCaixa(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteInclusaoRegistro;
        }
        private void AbrirCaixa(object objParam)
        {
            Retorno objRetorno;
            tbCaixa objCaixaAux = new tbCaixa();
            objCaixaAux.tbFuncionario = new tbFuncionario();
            objCaixaAux.tbCaixaMovimento = new List<tbCaixaMovimento>();
            using (var objBLL = new FormaPagamento())
            {
                objRetorno = objBLL.RetornaListaFormaPagamento();
            }
            if (objRetorno.intCodigoErro == 0)
            {
                List<tbFormaPagamento> arrFormaPagamento = (List<tbFormaPagamento>)objRetorno.objRetorno;
                if (arrFormaPagamento.Count > 0)
                {
                    foreach (tbFormaPagamento objFormaPagamento in arrFormaPagamento)
                    {
                        tbCaixaMovimento objCaixaMovimento = new tbCaixaMovimento();
                        objCaixaMovimento.tbFormaPagamento = objFormaPagamento;
                        objCaixaMovimento.fpg_codigo = objFormaPagamento.fpg_codigo;
                        objCaixaAux.tbCaixaMovimento.Add(objCaixaMovimento);
                    }
                }
                else
                {
                    tbCaixaMovimento objCaixaMovimento = new tbCaixaMovimento();
                    objCaixaMovimento.tbFormaPagamento = new tbFormaPagamento();
                    objCaixaAux.tbCaixaMovimento.Add(objCaixaMovimento);
                }
                objCaixaAberturaViewModel = new CaixaAberturaViewModel();
                objCaixaAberturaViewModel.OnDispose += CaixaAberturaViewModel_OnDispose;
                objCaixaAberturaViewModel.objCaixa = objCaixaAux;
                base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
                base.intSelectedIndexTabPrincipal = 1;
            }
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        private bool CanLancarMovimento(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteAlteracaoRegistro);
        }
        private void LancarMovimento(object objParam)
        {
            if (objParam != null)
            {
                var objValores = (object[])objParam;
                var intCaiCodigo = (int)objValores[0];
                var strFuncionario = (string)objValores[1];
                var dtFechamento = objValores[2];
                if (dtFechamento == null)
                {
                    tbCaixaMovimento objCaixaMovimento = new tbCaixaMovimento();
                    objCaixaMovimento.tbFormaPagamento = new tbFormaPagamento();
                    objCaixaMovimento.cai_codigo = intCaiCodigo;

                    objCaixaMovimentoViewModel = new CaixaMovimentoViewModel(null);
                    objCaixaMovimentoViewModel.OnDispose += CaixaMovimentoViewModel_OnDispose;
                    objCaixaMovimentoViewModel.CarregaComboOperacao();
                    objCaixaMovimentoViewModel.objCaixaMovimento = objCaixaMovimento;
                    objCaixaMovimentoViewModel.strFuncionario = strFuncionario;
                    base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
                    base.intSelectedIndexTabPrincipal = 2;
                }
                else
                    MessageBox.Show("Caixa fechado não pode receber lançamento!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanFecharCaixa(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteAlteracaoRegistro);
        }
        private void FecharCaixa(object objParam)
        {
            if (objParam != null)
            {
                Retorno objRetorno;
                using (var objBLL = new Caixa())
                {
                    objRetorno = objBLL.RetornaCaixaFechamento((int)objParam);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objCaixaFechamentoViewModel = new CaixaFechamentoViewModel();
                    objCaixaFechamentoViewModel.OnDispose += CaixaFechamentoViewModel_OnDispose;
                    objCaixaFechamentoViewModel.objFechamentoCaixa = (FechamentoCaixa)objRetorno.objRetorno;
                    base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
                    base.intSelectedIndexTabPrincipal = 3;
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));                
            }
        }

        private bool CanPesquisar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao;
        }
        public void Pesquisar(object objParam)
        {
            int intSkip;
            if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                intSkip = 0;

            Retorno objRetorno;
            using (var objBLL = new Caixa())
            {
                objRetorno = objBLL.RetornaListaCaixa(strCaiCodigoPesquisa, strCaiFuncionarioPesquisa, strCaiStatusPesquisa, dtCaiDataAberturaPesquisa != null ? ((DateTime)dtCaiDataAberturaPesquisa).ToShortDateString() : string.Empty, intSkip, base.intQtdeRegPagina);
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
                arrCaixaPesquisa = (List<tbCaixa>)objRetorno.objRetorno;
                if (arrCaixaPesquisa.Count() == 0)
                {
                    base.intTotalPagina = 1;
                    base.intPaginaAtual = 1;
                    base.intQtdeReg = 0;
                }
            }
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Comandos



        #region Eventos

        private void CaixaAberturaViewModel_OnDispose(object sender, EventArgs e)
        {
            objCaixaAberturaViewModel = null;
            objCaixaAberturaViewModel = new CaixaAberturaViewModel();
            base.enStatusTelaAtual = enStatusTela.Navegacao;
            base.intSelectedIndexTabPrincipal = 0;
            Pesquisar(0);
        }

        private void CaixaMovimentoViewModel_OnDispose(object sender, EventArgs e)
        {
            objCaixaMovimentoViewModel = null;
            objCaixaMovimentoViewModel = new CaixaMovimentoViewModel(null);
            base.enStatusTelaAtual = enStatusTela.Navegacao;
            base.intSelectedIndexTabPrincipal = 0;
            Pesquisar(0);
        }

        private void CaixaFechamentoViewModel_OnDispose(object sender, EventArgs e)
        {
            objCaixaFechamentoViewModel = null;
            objCaixaFechamentoViewModel = new CaixaFechamentoViewModel();
            base.enStatusTelaAtual = enStatusTela.Navegacao;
            base.intSelectedIndexTabPrincipal = 0;
            Pesquisar(0);
        }

        #endregion Eventos



        #region Métodos



        #endregion Métodos
    }
}