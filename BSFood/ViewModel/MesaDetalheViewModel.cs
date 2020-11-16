using BSFood.Apoio;
using BSFoodFramework.Apoio;
using BSFoodFramework.Models;

namespace BSFood.ViewModel
{
    public class MesaDetalheViewModel : ViewModelBase
    {
        public MesaDetalheViewModel(tbMesa _objMesa)
        {
            objMesa = _objMesa;
        }

        #region Propriedades

        public tbMesa objMesa { get; set; }

        public string mes_status
        {
            get { return objMesa.mes_status; }
            set
            {
                if (objMesa.mes_status != value)
                {
                    objMesa.mes_status = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string mes_terminal
        {
            get { return objMesa.mes_terminal; }
            set
            {
                if(objMesa.mes_terminal != value)
                {
                    objMesa.mes_terminal = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("blnSelecionada");
                }
            }
        }

        public bool blnSelecionada
        {
            get
            {
                return objMesa.mes_terminal == FrameworkUtil.objConfigLocal.strTerminal ? true : false;
            }
            set
            {
            }
        }

        public bool blnBloqueada
        {
            get
            {
                return objMesa.mes_terminal == FrameworkUtil.objConfigLocal.strTerminal || string.IsNullOrWhiteSpace(objMesa.mes_terminal) ? true : false;
            }
            set
            {
            }
        }

        #endregion Propriedades



        #region Comandos



        #endregion Comandos



        #region Métodos



        #endregion Métodos
    }
}