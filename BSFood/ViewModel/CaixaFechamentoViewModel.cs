using BSFood.Apoio;
using System;
using System.Windows;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class CaixaFechamentoViewModel : ViewModelBase
    {
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ImprimirFechamentoCommand { get; set; }

        public CaixaFechamentoViewModel()
        {
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ImprimirFechamentoCommand = new DelegateCommand(ImprimirFechamento);
        }


        #region Propriedades

        private FechamentoCaixa _objFechamentoCaixa;
        public FechamentoCaixa objFechamentoCaixa
        {
            get { return _objFechamentoCaixa; }
            set
            {
                _objFechamentoCaixa = value;
                RaisePropertyChanged(null);
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

        private string _strRelatorioTela;
        public string strRelatorioTela
        {
            get { return objFechamentoCaixa == null ? string.Empty : objFechamentoCaixa.strRelatorio; }
            set
            {
                _strRelatorioTela = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private bool CanSalvar(object objParam)
        {
            return _objFechamentoCaixa != null && objFechamentoCaixa.objCaixa.cai_dataFechamento == null;
        }
        private void Salvar(object objParam)
        {
            Validate();
            if (!HasErrors)
            {
                Retorno objRetorno;
                using (var objBLL = new Caixa())
                {
                    objRetorno = objBLL.FecharCaixa(objFechamentoCaixa, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFechamentoCaixa = (FechamentoCaixa)objRetorno.objRetorno;
                    ImprimirFechamento(null);
                    objFechamentoCaixa = null;
                    ClearAllErrorsAsync();
                    Dispose();
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private bool CanCancelar(object objParam)
        {
            return _objFechamentoCaixa != null;
        }
        private void Cancelar(object objParam)
        {
            if (MessageBox.Show("Todas as alterações serão perdidas, deseja cancelar a edição?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objFechamentoCaixa = null;
                ClearAllErrorsAsync();
                Dispose();
            }
        }


        private bool CanImprimirFechamento(object objParam)
        {
            return true;
        }
        private void ImprimirFechamento(object objParam)
        {
            try
            {
                if (objFechamentoCaixa != null && objFechamentoCaixa.strRelatorio != string.Empty)
                {
                    string[] arrLinhas = objFechamentoCaixa.strRelatorio.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (arrLinhas.Length > 0)
                    {
                        if (string.IsNullOrWhiteSpace(FrameworkUtil.objConfigStorage.objConfiguracao.cfg_impressoraEntrega))
                            MessageBox.Show("Impressora não configurada!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            FrameworkUtil.objGerenciaCupom.AbreRelatorio();
                            for (int i = 0; i < arrLinhas.Length; i++)
                            {
                                FrameworkUtil.objGerenciaCupom.LinhaRelatorio(arrLinhas[i]);
                            }
                            FrameworkUtil.objGerenciaCupom.FechaRelatorio(FrameworkUtil.objConfigStorage.objConfiguracao.cfg_impressoraEntrega);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion Comandos



        #region Eventos


        #endregion Eventos



        #region Métodos



        #endregion Métodos
    }
}