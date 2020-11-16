using System.Collections.ObjectModel;

namespace BSFood.ViewModel
{
    public class EntregaViewModel : TelaViewModel
    {
        public EntregaViewModel()
        {
            arrEntregaPedidoViewModel = new ObservableCollection<ViewModelBase>();
            EntregaControleViewModel objEntregaControleViewModel = new EntregaControleViewModel(this);
            objEntregaControleViewModel.strNomeTela = "Pesquisa";
            arrEntregaPedidoViewModel.Add(objEntregaControleViewModel);
            objEntregaPedidoViewModel = objEntregaControleViewModel;
        }


        #region Propriedades

        private ViewModelBase _objEntregaPedidoViewModel;
        public ViewModelBase objEntregaPedidoViewModel
        {
            get { return _objEntregaPedidoViewModel; }
            set
            {
                _objEntregaPedidoViewModel = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ViewModelBase> _arrEntregaPedidoViewModel;
        public ObservableCollection<ViewModelBase> arrEntregaPedidoViewModel
        {
            get { return _arrEntregaPedidoViewModel; }
            set
            {
                _arrEntregaPedidoViewModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos



        #endregion Comandos



        #region Eventos



        #endregion Eventos



        #region Métodos



        #endregion Métodos
    }
}