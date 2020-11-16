using BSFood.Apoio;
using BSFood.View;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;

namespace BSFood.ViewModel
{
    public class EntregaPedidoProdutoViewModel : ViewModelBase
    {
        public ICommand ProdutoCommand { get; set; }
        public ICommand RemoveProdutoCommand { get; set; }
        public EntregaPedidoProdutoViewModel(tbPedidoProduto _objPedidoProduto)
        {
            ProdutoCommand = new DelegateCommand(Produto);
            RemoveProdutoCommand = new DelegateCommand(RemoveProduto);
            objPedidoProduto = _objPedidoProduto;
        }

        #region Propriedades

        public tbPedidoProduto objPedidoProduto { get; set; }

        [Required(ErrorMessage = "Produto obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Produto obrigatório")]
        public int? pro_codigo
        {
            get
            {
                if (objPedidoProduto == null || objPedidoProduto.pro_codigo == 0)
                    return null;
                else
                    return objPedidoProduto.pro_codigo;
            }
            set
            {
                if (objPedidoProduto == null || objPedidoProduto.pro_codigo != value)
                {
                    objPedidoProduto.pro_codigo = value == null ? 0 : (int)value;
                    Produto(objPedidoProduto.pro_codigo);
                }
            }
        }

        public string pro_nome
        {
            get { return objPedidoProduto == null ? string.Empty : objPedidoProduto.tbProduto.pro_nome; }
            set
            {
                if (objPedidoProduto.tbProduto.pro_nome != value)
                {
                    objPedidoProduto.tbProduto.pro_nome = value;
                    objPedidoProduto.ppr_descricao = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Quantidade obrigatória")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantidade inválida")]
        public decimal ppr_quantidade
        {
            get { return objPedidoProduto == null ? 0 : objPedidoProduto.ppr_quantidade; }
            set
            {
                if (objPedidoProduto.ppr_quantidade != value)
                {
                    objPedidoProduto.ppr_quantidade = value;
                    ppr_valorTotal = value * ppr_valorUnitario;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ppr_valorUnitario
        {
            get { return objPedidoProduto == null ? 0 : objPedidoProduto.ppr_valorUnitario; }
            set
            {
                if (objPedidoProduto.ppr_valorUnitario != value)
                {
                    objPedidoProduto.ppr_valorUnitario = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal ppr_valorTotal
        {
            get { return objPedidoProduto == null ? 0 : objPedidoProduto.ppr_valorTotal; }
            set
            {
                if (objPedidoProduto.ppr_valorTotal != value)
                {
                    objPedidoProduto.ppr_valorTotal = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _blnCodigoFocus;
        public bool blnCodigoFocus
        {
            get { return _blnCodigoFocus; }
            set
            {
                _blnCodigoFocus = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnQuantidadeFocus;
        public bool blnQuantidadeFocus
        {
            get { return _blnQuantidadeFocus; }
            set
            {
                _blnQuantidadeFocus = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private void Produto(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                blnQuantidadeFocus = false;
                if (objParam.GetType() == typeof(tbProduto))
                {
                    if (((tbProduto)objParam).pro_codigo > 0)
                    {
                        objPedidoProduto.pro_codigo = ((tbProduto)objParam).pro_codigo;
                        objPedidoProduto.tbProduto.pro_nome = ((tbProduto)objParam).pro_nome;
                        objPedidoProduto.ppr_descricao = ((tbProduto)objParam).pro_nome;
                        objPedidoProduto.ppr_valorUnitario = ((tbProduto)objParam).pro_precoEntrega;
                        objPedidoProduto.ppr_valorTotal = ((tbProduto)objParam).pro_precoEntrega;
                        objPedidoProduto.ppr_quantidade = 1;
                        _blnQuantidadeFocus = true;
                    }
                    else
                    {
                        objPedidoProduto.pro_codigo = 0;
                        objPedidoProduto.tbProduto.pro_nome = string.Empty;
                        objPedidoProduto.ppr_descricao = string.Empty;
                        objPedidoProduto.ppr_valorUnitario = 0;
                        objPedidoProduto.ppr_valorTotal = 0;
                        objPedidoProduto.ppr_quantidade = 0;
                    }
                    RaisePropertyChanged("pro_codigo");
                    RaisePropertyChanged("pro_nome");
                    RaisePropertyChanged("ppr_valorUnitario");
                    RaisePropertyChanged("ppr_valorTotal");
                    RaisePropertyChanged("ppr_quantidade");
                    RaisePropertyChanged("blnQuantidadeFocus");
                }
                else if (objParam.ToString() == "Pesquisar")
                {
                    winCadastro objTelaCadastro = new winCadastro();
                    ProdutoViewModel objProdutoViewModel = new ProdutoViewModel();
                    objProdutoViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
                    objProdutoViewModel.blnJanela = true;
                    objTelaCadastro.Title = "Cadastro - " + objProdutoViewModel.strNomeTela;
                    objTelaCadastro.DataContext = objProdutoViewModel;
                    objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
                    objTelaCadastro.Closed += (sen, eve) =>
                    {
                        Produto(objProdutoViewModel.objProduto);
                        objProdutoViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new Produtos())
                    {
                        objRetorno = objBLL.RetornaProduto(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        Produto((tbProduto)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        Produto(new tbProduto());
                    }
                }
                else
                    Produto("Pesquisar");
            }

            //if (objParam != null)
            //{
            //    if (objParam.ToString() == "Novo")
            //    {
            //        winCadastro objTelaCadastro = new winCadastro();
            //        ProdutoViewModel objProdutoViewModel = new ProdutoViewModel();
            //        if (objPedidoProduto.pro_codigo > 0)
            //            objProdutoViewModel.pro_codigo = objPedidoProduto.pro_codigo;
            //        objProdutoViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
            //        objTelaCadastro.Title = "Cadastro - " + objProdutoViewModel.strNomeTela;
            //        objTelaCadastro.DataContext = objProdutoViewModel;
            //        objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
            //        objTelaCadastro.Closed += (sen, eve) =>
            //        {
            //            if (objProdutoViewModel.pro_codigo != null)
            //            {
            //                objPedidoProduto.pro_codigo = (int)objProdutoViewModel.pro_codigo;
            //                RaisePropertyChanged("pro_codigo");
            //                objPedidoProduto.tbProduto.pro_nome = objProdutoViewModel.pro_nome;
            //                objPedidoProduto.ppr_descricao = objProdutoViewModel.pro_nome;
            //                RaisePropertyChanged("pro_nome");
            //                objPedidoProduto.ppr_valorUnitario = objProdutoViewModel.objProduto.pro_precoEntrega;
            //                RaisePropertyChanged("ppr_valorUnitario");
            //                objPedidoProduto.ppr_valorTotal = objProdutoViewModel.objProduto.pro_precoEntrega;
            //                RaisePropertyChanged("ppr_valorTotal");
            //                objPedidoProduto.ppr_quantidade = 1;
            //                RaisePropertyChanged("ppr_quantidade");
            //            }
            //            objProdutoViewModel = null;
            //            objTelaCadastro = null;
            //        };
            //        objTelaCadastro.ShowDialog();
            //    }
            //    else if (objParam.ToString() == "Pesquisar")
            //    {
            //        ProdutoViewModel objProdutoViewModel = new ProdutoViewModel();
            //        objProdutoViewModel.OnPesquisa += (sen, eve) =>
            //        {
            //            if (objProdutoViewModel.pro_codigo != null)
            //            {
            //                objPedidoProduto.pro_codigo = (int)objProdutoViewModel.pro_codigo;
            //                RaisePropertyChanged("pro_codigo");
            //                objPedidoProduto.tbProduto.pro_nome = objProdutoViewModel.pro_nome;
            //                objPedidoProduto.ppr_descricao = objProdutoViewModel.pro_nome;
            //                RaisePropertyChanged("pro_nome");
            //                objPedidoProduto.ppr_valorUnitario = objProdutoViewModel.objProduto.pro_precoEntrega;
            //                RaisePropertyChanged("ppr_valorUnitario");
            //                objPedidoProduto.ppr_valorTotal = objProdutoViewModel.objProduto.pro_precoEntrega;
            //                RaisePropertyChanged("ppr_valorTotal");
            //                objPedidoProduto.ppr_quantidade = 1;
            //                RaisePropertyChanged("ppr_quantidade");
            //            }
            //            objProdutoViewModel.Dispose();
            //        };
            //        objProdutoViewModel.Pesquisar("AbrirTela");
            //    }
            //    else
            //    {
            //        int intCodigo;
            //        if (int.TryParse(objParam.ToString(), out intCodigo))
            //            pro_codigo = intCodigo;
            //        else
            //            Produto("Pesquisar");
            //    }
            //}
        }

        private void RemoveProduto(object objParam)
        {
            Dispose();
        }

        #endregion Comandos



        #region Métodos



        #endregion Métodos
    }
}