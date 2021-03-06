﻿using BSFood.Apoio;
using BSFood.View;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand AutenticaFuncionarioCommand { get; set; }
        public ICommand ConfiguracaoLocalCommand { get; set; }

        public LoginViewModel()
        {
            AutenticaFuncionarioCommand = new DelegateCommand(AutenticaFuncionario, CanAutenticaFuncionario);
            ConfiguracaoLocalCommand = new DelegateCommand(ConfiguracaoLocal, CanConfiguracaoLocal);
        }

        #region Propriedades

        private string _fun_login;
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "É permitido apenas 50 caracteres")]
        public string fun_login
        {
            get { return this._fun_login; }
            set
            {
                if (this._fun_login != value)
                {
                    this._fun_login = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _fun_senha;
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "É permitido apenas 50 caracteres")]
        public string fun_senha
        {
            get { return this._fun_senha; }
            set
            {
                if (this._fun_senha != value)
                {
                    this._fun_senha = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion Propriedades



        #region Comandos

        private bool CanAutenticaFuncionario(object objParam)
        {
            return !HasErrors;
        }
        private void AutenticaFuncionario(object objParam)
        {
            Validate();
            if (!HasErrors)
            {
                Retorno objRetorno;
                using (var objBLL = new Funcionarios())
                {
                    objRetorno = objBLL.AutenticaFuncionario(this.fun_login, this.fun_senha);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    FrameworkUtil.objConfigStorage = (ConfigStorage)objRetorno.objRetorno;
                    ((Window)objParam).Close();
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private bool CanConfiguracaoLocal(object objParam)
        {
            return true;
        }
        private void ConfiguracaoLocal(object objParam)
        {
            winConfiguracaoLocal objTelaConfiguracaoLocal = new winConfiguracaoLocal();
            objTelaConfiguracaoLocal.Owner = (Window)objParam;
            objTelaConfiguracaoLocal.ShowDialog();
        }        

        #endregion Comandos



        #region Eventos



        #endregion Eventos
    }
}
