using BSFood.Apoio;
using BSFood.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;

namespace BSFood.ViewModel
{
    public class EntregaPedidoEnderecoViewModel : ViewModelBase
    {
        public ICommand BairroCommand { get; set; }
        public ICommand RemoveEnderecoCommand { get; set; }
        public EntregaPedidoEnderecoViewModel(tbClienteEndereco _objClienteEndereco)
        {
            BairroCommand = new DelegateCommand(Bairro);
            RemoveEnderecoCommand = new DelegateCommand(RemoveEndereco);
            CarregaComboBairro();
            objClienteEndereco = _objClienteEndereco;
        }

        #region Propriedades

        public tbClienteEndereco objClienteEndereco { get; set; }

        public int cen_codigo
        {
            get { return objClienteEndereco.cen_codigo; }
            set
            {
                objClienteEndereco.cen_codigo = value;
                RaisePropertyChanged();
            }
        }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(200, ErrorMessage = "É permitido apenas 200 caracteres")]
        public string cen_logradouro
        {
            get { return objClienteEndereco.cen_logradouro; }
            set
            {
                objClienteEndereco.cen_logradouro = value;
                RaisePropertyChanged();
            }
        }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(10, ErrorMessage = "É permitido apenas 10 caracteres")]
        public string cen_numero
        {
            get { return objClienteEndereco.cen_numero; }
            set
            {
                objClienteEndereco.cen_numero = value;
                RaisePropertyChanged();
            }
        }

        [StringLength(150, ErrorMessage = "É permitido apenas 150 caracteres")]
        public string cen_complemento
        {
            get { return objClienteEndereco.cen_complemento; }
            set
            {
                objClienteEndereco.cen_complemento = value;
                RaisePropertyChanged();
            }
        }

        [Required(ErrorMessage = "Bairro obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Bairro obrigatório")]
        public int bai_codigo
        {
            get
            {
                if (objClienteEndereco == null)
                    return 0;
                else
                    return objClienteEndereco.bai_codigo;
            }
            set
            {
                if (objClienteEndereco.bai_codigo != value)
                {
                    objClienteEndereco.tbBairro = arrBairro.FirstOrDefault(bai => bai.bai_codigo == value);
                    objClienteEndereco.bai_codigo = value;
                    RaisePropertyChanged("bai_codigo");
                    RaisePropertyChanged("bai_entrega");
                    RaisePropertyChanged("bai_taxaEntrega");
                }
            }
        }

        public string bai_entrega
        {
            get { return objClienteEndereco == null ? "SIM" : objClienteEndereco.tbBairro.bai_realizaEntrega == true ? "SIM" : "NÃO"; }
            set { }
        }

        public decimal bai_taxaEntrega
        {
            get
            {
                if (blnSelecionado && objClienteEndereco != null && objClienteEndereco.tbBairro != null)
                    return objClienteEndereco.tbBairro.bai_taxaEntrega;
                else
                    return 0;
            }
            set { }
        }

        private List<tbBairro> _arrBairro;
        public List<tbBairro> arrBairro
        {
            get { return _arrBairro; }
            set
            {
                _arrBairro = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnSelecionado;
        public bool blnSelecionado
        {
            get { return _blnSelecionado; }
            set
            {
                _blnSelecionado = value;
                RaisePropertyChanged();
                RaisePropertyChanged("bai_taxaEntrega");
            }
        }

        private bool _blnLogradouroFocus;
        public bool blnLogradouroFocus
        {
            get { return _blnLogradouroFocus; }
            set
            {
                _blnLogradouroFocus = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private void Bairro(object objParam)
        {
            winCadastro objTelaCadastro = new winCadastro();
            BairroViewModel objBairroViewModel = new BairroViewModel();
            objBairroViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
            objBairroViewModel.blnJanela = true;
            objTelaCadastro.Title = "Cadastro - " + objBairroViewModel.strNomeTela;
            objTelaCadastro.DataContext = objBairroViewModel;
            objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
            objTelaCadastro.Closed += (sen, eve) =>
            {
                if (objBairroViewModel.objBairro != null)
                {
                    CarregaComboBairro();
                    objClienteEndereco.tbBairro = objBairroViewModel.objBairro;
                    objClienteEndereco.bai_codigo = (int)objBairroViewModel.objBairro.bai_codigo;
                    RaisePropertyChanged("bai_codigo");
                    RaisePropertyChanged("bai_entrega");
                    RaisePropertyChanged("bai_taxaEntrega");
                }
                objBairroViewModel = null;
                objTelaCadastro = null;
            };
            objTelaCadastro.ShowDialog();
        }

        private void RemoveEndereco(object objParam)
        {
            Dispose();
        }

        #endregion Comandos



        #region Métodos

        private void CarregaComboBairro()
        {
            Retorno objRetorno;
            using (var objBLL = new Bairros())
            {
                objRetorno = objBLL.RetornaListaBairro();
            }
            if (objRetorno.intCodigoErro == 0)
                arrBairro = (List<tbBairro>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}