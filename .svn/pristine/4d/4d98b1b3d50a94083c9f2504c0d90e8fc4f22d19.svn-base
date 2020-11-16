using BSFood.Apoio;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class MesaViewModel : TelaViewModel
    {
        public ICommand BuscarCommand { get; set; }
        public ICommand SelecionarCommand { get; set; }

        private DispatcherTimer timerAtualizaMesa;

        public MesaViewModel()
        {
            BuscarCommand = new DelegateCommand(Buscar);
            SelecionarCommand = new DelegateCommand(Selecionar);
            _blnLivre = true;
            _blnOcupado = true;
            FiltraMesa();

            timerAtualizaMesa = new DispatcherTimer();
            timerAtualizaMesa.Interval = new TimeSpan(0, 1, 0); // 1 minuto
            timerAtualizaMesa.Tick += timerAtualizaMesa_Tick;
            timerAtualizaMesa.Start();
        }



        #region Propriedades

        private int? _intNumero;
        public int? intNumero
        {
            get { return _intNumero; }
            set
            {
                if(_intNumero != value)
                {
                    _intNumero = value;
                    RaisePropertyChanged();
                    FiltraMesa();
                }
            }
        }

        private bool _blnLivre;
        public bool blnLivre
        {
            get { return _blnLivre; }
            set
            {
                if (_blnLivre != value)
                {
                    _blnLivre = value;
                    RaisePropertyChanged();
                    FiltraMesa();
                }
            }
        }

        private bool _blnOcupado;
        public bool blnOcupado
        {
            get { return _blnOcupado; }
            set
            {
                if (_blnOcupado != value)
                {
                    _blnOcupado = value;
                    RaisePropertyChanged();
                    FiltraMesa();
                }
            }
        }

        private ObservableCollection<MesaDetalheViewModel> _arrMesaDetalheViewModel;
        public ObservableCollection<MesaDetalheViewModel> arrMesaDetalheViewModel
        {
            get { return _arrMesaDetalheViewModel; }
            set
            {
                _arrMesaDetalheViewModel = value;
                RaisePropertyChanged();
            }
        }

        private MesaPedidoViewModel _objMesaPedidoViewModel;
        public MesaPedidoViewModel objMesaPedidoViewModel
        {
            get { return _objMesaPedidoViewModel; }
            set
            {
                _objMesaPedidoViewModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private void Buscar(object objParam)
        {
            FiltraMesa();
        }

        private void Selecionar(object objParam)
        {
            if (objParam != null)
            {
                int intNumero;
                if (int.TryParse(objParam.ToString(), out intNumero))
                {
                    Retorno objRetorno;
                    using (var objBLL = new Pedidos())
                    {
                        objRetorno = objBLL.RetornaPedidoMesa(intNumero, FrameworkUtil.objConfigLocal.strTerminal);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        tbMesa objMesa = (tbMesa)objRetorno.objRetorno;
                        foreach (var objMesaAux in arrMesaDetalheViewModel)
                        {
                            if (objMesaAux.objMesa.mes_codigo == objMesa.mes_codigo)
                                objMesaAux.mes_terminal = FrameworkUtil.objConfigLocal.strTerminal;
                            else
                                objMesaAux.mes_terminal = null;
                        }
                        if (objMesa.tbPedido == null)
                        {
                            tbPedido objPedidoAux = new tbPedido();
                            objPedidoAux.cli_codigo = FrameworkUtil.objConfigStorage.objConfiguracao.cli_codigo;
                            objPedidoAux.fpg_codigo = FrameworkUtil.objConfigStorage.objConfiguracao.fpg_codigo;
                            objPedidoAux.ped_numeroMesa = intNumero;

                            objPedidoAux.tbFuncionario = new tbFuncionario();

                            objPedidoAux.tbFormaPagamento = new tbFormaPagamento();

                            objPedidoAux.tbPedidoProduto = new List<tbPedidoProduto>();

                            tbPedidoProduto objPedidoProduto = new tbPedidoProduto();
                            objPedidoProduto.tbProduto = new tbProduto();
                            objPedidoAux.tbPedidoProduto.Add(objPedidoProduto);

                            //var objCaixa = arrCaixa.FirstOrDefault(cai => cai.fun_codigo == FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                            //if (objCaixa != null)
                            //    objPedidoAux.cai_codigo = objCaixa.cai_codigo;

                            objMesa.tbPedido = objPedidoAux;
                        }
                        MesaPedidoViewModel objMesaPedidoViewModelAux = new MesaPedidoViewModel();
                        objMesaPedidoViewModelAux.OnDispose += ObjMesaPedidoViewModel_OnDispose;
                        objMesaPedidoViewModelAux.objPedido = objMesa.tbPedido;
                        objMesaPedidoViewModel = objMesaPedidoViewModelAux;
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                    }
                }
            }





            //if (objParam != null)
            //{
            //    int intNumero;
            //    if (int.TryParse(objParam.ToString(), out intNumero))
            //    {
            //        Retorno objRetorno;
            //        using (var objBLL = new Pedidos())
            //        {
            //            objRetorno = objBLL.RetornaPedidoMesa(intNumero);
            //        }
            //        if (objRetorno.intCodigoErro == 0)
            //        {
            //            tbMesa objMesa = (tbMesa)objRetorno.objRetorno;
            //            foreach (var objMesaAux in arrMesaDetalheViewModel)
            //            {
            //                if (objMesaAux.objMesa.mes_codigo == objMesa.mes_codigo)
            //                    objMesaAux.mes_selecionada = true;
            //                else
            //                    objMesaAux.mes_selecionada = false;
            //            }
            //            if (objMesa.tbPedido == null)
            //            {
            //                tbPedido objPedidoAux = new tbPedido();
            //                objPedidoAux.cli_codigo = FrameworkUtil.objConfigStorage.objConfiguracao.cli_codigo;
            //                objPedidoAux.fpg_codigo = FrameworkUtil.objConfigStorage.objConfiguracao.fpg_codigo;

            //                objPedidoAux.tbFuncionario = new tbFuncionario();

            //                objPedidoAux.tbFormaPagamento = new tbFormaPagamento();

            //                objPedidoAux.tbPedidoProduto = new List<tbPedidoProduto>();

            //                tbPedidoProduto objPedidoProduto = new tbPedidoProduto();
            //                objPedidoProduto.tbProduto = new tbProduto();
            //                objPedidoAux.tbPedidoProduto.Add(objPedidoProduto);

            //                //var objCaixa = arrCaixa.FirstOrDefault(cai => cai.fun_codigo == FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
            //                //if (objCaixa != null)
            //                //    objPedidoAux.cai_codigo = objCaixa.cai_codigo;

            //                objMesa.tbPedido = objPedidoAux;
            //            }
            //            MesaPedidoViewModel objMesaPedidoViewModelAux = new MesaPedidoViewModel();
            //            objMesaPedidoViewModelAux.OnDispose += ObjMesaPedidoViewModel_OnDispose;
            //            objMesaPedidoViewModelAux.objPedido = objMesa.tbPedido;
            //            objMesaPedidoViewModel = objMesaPedidoViewModelAux;
            //        }
            //        else
            //        {
            //            MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            //        }
            //        RaisePropertyChanged("mes_numero");
            //    }
            //}
        }

        #endregion Comandos



        #region Eventos

        void timerAtualizaMesa_Tick(object sender, EventArgs e)
        {
            AtualizaMesa();
        }

        private void ObjMesaPedidoViewModel_OnDispose(object sender, EventArgs e)
        {
            objMesaPedidoViewModel = null;
            FiltraMesa();
        }

        #endregion Eventos



        #region Métodos

        private void FiltraMesa()
        {
            Retorno objRetorno;
            using (var objBLL = new Pedidos())
            {
                objRetorno = objBLL.RetornaListaMesa(intNumero, blnLivre, blnOcupado, FrameworkUtil.objConfigLocal.strTerminal);
            }
            if (objRetorno.intCodigoErro == 0)
            {
                ObservableCollection<MesaDetalheViewModel> arrMesaDetalheViewModelAux = new ObservableCollection<MesaDetalheViewModel>();
                foreach (tbMesa objMesa in (List<tbMesa>)objRetorno.objRetorno)
                {
                    arrMesaDetalheViewModelAux.Add(new MesaDetalheViewModel(objMesa));
                }
                arrMesaDetalheViewModel = arrMesaDetalheViewModelAux;
            }
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        private void AtualizaMesa()
        {
            Retorno objRetorno;
            using (var objBLL = new Pedidos())
            {
                objRetorno = objBLL.RetornaListaMesa(intNumero, blnLivre, blnOcupado);
            }
            if (objRetorno.intCodigoErro == 0)
            {
                ObservableCollection<MesaDetalheViewModel> arrMesaDetalheViewModelAux = new ObservableCollection<MesaDetalheViewModel>();
                foreach (tbMesa objMesa in (List<tbMesa>)objRetorno.objRetorno)
                {
                    arrMesaDetalheViewModelAux.Add(new MesaDetalheViewModel(objMesa));
                }
                arrMesaDetalheViewModel = arrMesaDetalheViewModelAux;
            }
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}