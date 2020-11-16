using BSFood.Apoio;
using BSFoodFramework.Apoio;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;

namespace BSFood.ViewModel
{
    public class ConfiguracaoLocalViewModel : ViewModelBase
    {
        public ICommand SalvarCommand { get; set; }
        
        public ConfiguracaoLocalViewModel()
        {
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
        }

        #region Propriedades

        //[RequiredIf("blnSqlCompact", false, ErrorMessage = "É necessário escolher o tipo de banco.")]
        [Required(ErrorMessage="Campo obrigatorio")]
        public bool blnSqlServer 
        {
            get { return FrameworkUtil.objConfigLocal.blnSqlServer; }
            set
            {
                if(FrameworkUtil.objConfigLocal.blnSqlServer != value)
                {
                    FrameworkUtil.objConfigLocal.blnSqlServer = value;
                    RaisePropertyChanged();
                }
            }
        }

        //[RequiredIf("blnSqlServer", false, ErrorMessage = "É necessário escolher o tipo de banco.")]
        [Required(ErrorMessage="Campo obrigatorio")]
        public bool blnSqlCompact 
        {
            get { return FrameworkUtil.objConfigLocal.blnSqlCompact; }
            set
            {
                if (FrameworkUtil.objConfigLocal.blnSqlCompact != value)
                {
                    FrameworkUtil.objConfigLocal.blnSqlCompact = value;
                    RaisePropertyChanged();
                }
            }
        }

        [RequiredIf("blnSqlServer", true, ErrorMessage = "Informe o endereço do banco de dados.")]
        public string strEnderecoBanco 
        {
            get { return FrameworkUtil.objConfigLocal.strEnderecoBanco; }
            set
            {
                if (FrameworkUtil.objConfigLocal.strEnderecoBanco != value)
                {
                    FrameworkUtil.objConfigLocal.strEnderecoBanco = value;
                    RaisePropertyChanged();
                }
            }
        }

        [RequiredIf("blnSqlServer", true, ErrorMessage = "Informe o nome do banco de dados.")]
        public string strNomeBanco 
        {
            get { return FrameworkUtil.objConfigLocal.strNomeBanco; }
            set
            {
                if (FrameworkUtil.objConfigLocal.strNomeBanco != value)
                {
                    FrameworkUtil.objConfigLocal.strNomeBanco = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        public string strTerminal
        {
            get { return FrameworkUtil.objConfigLocal.strTerminal; }
            set
            {
                if (FrameworkUtil.objConfigLocal.strTerminal != value)
                {
                    FrameworkUtil.objConfigLocal.strTerminal = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion Propriedades



        #region Comandos


        private bool CanSalvar(object objParam)
        {
            return !HasErrors;
        }

        private void Salvar(object objParam)
        {
            Validate();
            if (!HasErrors)
            {
                string strMensagem;
                if (FrameworkUtil.SalvarConfiguracao(FrameworkUtil.objConfigLocal, out strMensagem))
                    ((Window)objParam).Close();
                else
                    MessageBox.Show(strMensagem, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }        

        #endregion Comandos
    }
}
