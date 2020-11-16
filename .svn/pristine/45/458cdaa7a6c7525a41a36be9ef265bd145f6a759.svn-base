using BSFood.Apoio;
using BSFood.View;
using System;
using System.Windows;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class EntregaControleDetalheViewModel : ViewModelBase
    {
        public ICommand FuncionarioEntregadorCommand { get; set; }
        public EntregaControleDetalheViewModel(tbPedido _objPedido)
        {
            FuncionarioEntregadorCommand = new DelegateCommand(FuncionarioEntregador);
            objPedido = _objPedido;
        }

        #region Propriedades

        public tbPedido objPedido { get; set; }

        public int? fun_funcionarioEntregador
        {
            get
            {
                if (objPedido == null || objPedido.fun_funcionarioEntregador == 0)
                    return null;
                else
                    return objPedido.fun_funcionarioEntregador;
            }
            set
            {
                if (objPedido.fun_funcionarioEntregador != value)
                {
                    objPedido.fun_funcionarioEntregador = value;
                    FuncionarioEntregador(objPedido.fun_funcionarioEntregador);
                }
            }
        }
        public string fun_nomeEntregador
        {
            get { return objPedido == null ? string.Empty : (objPedido.tbFuncionarioEntregador == null ? string.Empty : objPedido.tbFuncionarioEntregador.fun_nome); }
            set
            {
                if (objPedido.tbFuncionarioEntregador.fun_nome != value)
                {
                    objPedido.tbFuncionarioEntregador.fun_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime? ped_dataEntrega
        {
            get { return objPedido.ped_dataEntrega; }
            set
            {
                if (objPedido.ped_dataEntrega != value)
                {
                    objPedido.ped_dataEntrega = value;
                    RaisePropertyChanged("ped_dataEntrega", false);
                }
            }
        }

        private TimeSpan? _tsProducao;
        public TimeSpan? tsProducao
        {
            get { return _tsProducao; }
            set
            {
                _tsProducao = value;
                RaisePropertyChanged("tsProducao", false);
            }
        }

        #endregion Propriedades



        #region Comandos

        private void FuncionarioEntregador(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                if (objParam.GetType() == typeof(tbFuncionario))
                {
                    if (((tbFuncionario)objParam).fun_codigo > 0)
                    {
                        objPedido.fun_funcionarioEntregador = ((tbFuncionario)objParam).fun_codigo;
                        objPedido.tbFuncionarioEntregador.fun_nome = ((tbFuncionario)objParam).fun_nome;
                        SalvarEntregador();
                    }
                    else
                    {
                        objPedido.fun_funcionarioEntregador = null;
                        objPedido.tbFuncionarioEntregador.fun_nome = string.Empty;
                    }
                    RaisePropertyChanged("fun_funcionarioEntregador");
                    RaisePropertyChanged("fun_nomeEntregador");
                }
                else if (objParam.ToString() == "Pesquisar")
                {
                    winCadastro objTelaCadastro = new winCadastro();
                    FuncionarioViewModel objFuncionarioViewModel = new FuncionarioViewModel();
                    objFuncionarioViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
                    objFuncionarioViewModel.blnJanela = true;
                    objTelaCadastro.Title = "Cadastro - " + objFuncionarioViewModel.strNomeTela;
                    objTelaCadastro.DataContext = objFuncionarioViewModel;
                    objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
                    objTelaCadastro.Closed += (sen, eve) =>
                    {
                        FuncionarioEntregador(objFuncionarioViewModel.objFuncionario);
                        objFuncionarioViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new Funcionarios())
                    {
                        objRetorno = objBLL.RetornaFuncionario(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        FuncionarioEntregador((tbFuncionario)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        FuncionarioEntregador(new tbFuncionario());
                    }
                }
                else
                    FuncionarioEntregador("Pesquisar");
            }
        }

        #endregion Comandos



        #region Métodos

        private void SalvarEntregador()
        {
            Retorno objRetorno;
            using (var objBLL = new Pedidos())
            {
                objRetorno = objBLL.SalvarEntregador(objPedido, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
            }
            if (objRetorno.intCodigoErro == 0)
            {
                objPedido = (tbPedido)objRetorno.objRetorno;
                RaisePropertyChanged("objPedido");
            }
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}