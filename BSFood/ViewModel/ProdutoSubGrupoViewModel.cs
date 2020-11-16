using BSFood.Apoio;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using BSFoodFramework.Models;

namespace BSFood.ViewModel
{
    public class ProdutoSubGrupoViewModel : ViewModelBase
    {
        public ICommand RemoveSubGrupoCommand { get; set; }
        public ProdutoSubGrupoViewModel(tbProdutoSubGrupo _objProdutoSubGrupo)
        {
            RemoveSubGrupoCommand = new DelegateCommand(RemoveSubGrupo, CanRemoveSubGrupo);
            this.objProdutoSubGrupo = _objProdutoSubGrupo;
        }

        #region Propriedades

        public tbProdutoSubGrupo objProdutoSubGrupo { get; set; }

        [Required(ErrorMessage = "Nome do grupo obrigatório")]
        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]        
        public string psgr_nome
        {
            get { return this.objProdutoSubGrupo.psgr_nome; }
            set
            {
                this.objProdutoSubGrupo.psgr_nome = value;
                RaisePropertyChanged();
            }
        }

        public bool psgr_exibeRelatorio
        {
            get { return this.objProdutoSubGrupo.psgr_exibeRelatorio; }
            set
            {
                this.objProdutoSubGrupo.psgr_exibeRelatorio = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnPsgrNomeFocus;
        public bool blnPsgrNomeFocus
        {
            get { return _blnPsgrNomeFocus; }
            set
            {
                _blnPsgrNomeFocus = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private bool CanRemoveSubGrupo(object objParam)
        {
            return true;
        }
        private void RemoveSubGrupo(object objParam)
        {
            this.Dispose();
        }

        #endregion Comandos
    }
}
