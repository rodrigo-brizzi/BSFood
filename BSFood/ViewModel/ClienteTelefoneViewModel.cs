using BSFood.Apoio;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using BSFoodFramework.Models;

namespace BSFood.ViewModel
{
    public class ClienteTelefoneViewModel : ViewModelBase
    {
        public ICommand RemoveTelefoneCommand { get; set; }
        public ClienteTelefoneViewModel(tbClienteTelefone _objClienteTelefone)
        {
            RemoveTelefoneCommand = new DelegateCommand(RemoveTelefone, CanRemoveTelefone);
            this.objClienteTelefone = _objClienteTelefone;
        }

        #region Propriedades

        public tbClienteTelefone objClienteTelefone { get; set; }

        [Required(ErrorMessage = "Telefone obrigatório")]
        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string ctl_numero
        {
            get { return this.objClienteTelefone.ctl_numero; }
            set
            {
                this.objClienteTelefone.ctl_numero = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnTelefoneFocus;
        public bool blnTelefoneFocus
        {
            get { return _blnTelefoneFocus; }
            set
            {
                _blnTelefoneFocus = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private bool CanRemoveTelefone(object objParam)
        {
            return true;
        }
        private void RemoveTelefone(object objParam)
        {
            this.Dispose();
        }

        #endregion Comandos
    }
}
