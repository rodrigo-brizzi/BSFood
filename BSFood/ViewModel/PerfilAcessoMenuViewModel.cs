using BSFoodFramework.Models;

namespace BSFood.ViewModel
{
    public class PerfilAcessoMenuViewModel : ViewModelBase
    {
        public PerfilAcessoMenuViewModel(tbPerfilAcessoMenu _objPerfilAcessoMenu)
        {
            objPerfilAcessoMenu = _objPerfilAcessoMenu;
        }

        #region Propriedades

        public tbPerfilAcessoMenu objPerfilAcessoMenu { get; set; }
        
        public bool pam_permiteAlteracao
        {
            get { return objPerfilAcessoMenu.pam_permiteAlteracao; }
            set
            {
                objPerfilAcessoMenu.pam_permiteAlteracao = value;
                RaisePropertyChanged();
            }
        }

        public bool pam_permiteExclusao
        {
            get { return objPerfilAcessoMenu.pam_permiteExclusao; }
            set
            {
                objPerfilAcessoMenu.pam_permiteExclusao = value;
                RaisePropertyChanged();
            }
        }

        public bool pam_permiteInclusao
        {
            get { return objPerfilAcessoMenu.pam_permiteInclusao; }
            set
            {
                objPerfilAcessoMenu.pam_permiteInclusao = value;
                RaisePropertyChanged();
            }
        }

        public bool pam_permiteVisualizacao
        {
            get { return objPerfilAcessoMenu.pam_permiteVisualizacao; }
            set
            {
                objPerfilAcessoMenu.pam_permiteVisualizacao = value;
                RaisePropertyChanged();
            }
        }

        public bool pam_toolBar
        {
            get { return objPerfilAcessoMenu.pam_toolBar; }
            set
            {
                objPerfilAcessoMenu.pam_toolBar = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnSelecionaTodos;
        public bool blnSelecionaTodos
        {
            get { return _blnSelecionaTodos; }
            set
            {
                _blnSelecionaTodos = value;
                objPerfilAcessoMenu.pam_permiteAlteracao = value;
                objPerfilAcessoMenu.pam_permiteExclusao = value;
                objPerfilAcessoMenu.pam_permiteInclusao = value;
                objPerfilAcessoMenu.pam_permiteVisualizacao = value;
                objPerfilAcessoMenu.pam_toolBar = value;
                RaisePropertyChanged(null);
            }
        }

        #endregion Propriedades
    }
}
