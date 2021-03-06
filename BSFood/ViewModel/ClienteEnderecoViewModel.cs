﻿using BSFood.Apoio;
using BSFood.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;

namespace BSFood.ViewModel
{
    public class ClienteEnderecoViewModel : ViewModelBase
    {
        public ICommand BairroCommand { get; set; }
        public ICommand CidadeCommand { get; set; }
        public ICommand RemoveEnderecoCommand { get; set; }
        public ClienteEnderecoViewModel(tbClienteEndereco _objClienteEndereco)
        {
            BairroCommand = new DelegateCommand(Bairro);
            CidadeCommand = new DelegateCommand(Cidade);
            RemoveEnderecoCommand = new DelegateCommand(RemoveEndereco);
            CarregaComboEstado();
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

        [StringLength(9, ErrorMessage = "É permitido apenas 9 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string cen_cep 
        {
            get { return objClienteEndereco.cen_cep; }
            set
            {
                objClienteEndereco.cen_cep = value;
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
        public int? bai_codigo
        {
            get
            {
                if (objClienteEndereco == null || objClienteEndereco.bai_codigo == 0)
                    return null;
                else
                    return objClienteEndereco.bai_codigo;
            }
            set
            {
                if (objClienteEndereco.bai_codigo != value)
                {
                    objClienteEndereco.bai_codigo = value == null ? 0 : (int)value;
                    Bairro(objClienteEndereco.bai_codigo);
                }
            }
        }
        
        public string bai_nome
        {
            get { return objClienteEndereco == null ? string.Empty : objClienteEndereco.tbBairro.bai_nome; }
            set
            {
                if (objClienteEndereco.tbBairro.bai_nome != value)
                {
                    objClienteEndereco.tbBairro.bai_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string bai_entrega
        {
            get { return objClienteEndereco == null ? string.Empty : objClienteEndereco.tbBairro.bai_realizaEntrega == true ? string.Empty : "NÃO ENTREGA"; }
            set { }
        }
        
        [Required(ErrorMessage = "Estado obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Estado obrigatório")]
        public int est_codigo
        {
            get { return objClienteEndereco == null ? 0 : objClienteEndereco.est_codigo; }
            set
            {
                if (objClienteEndereco.est_codigo != value)
                {
                    objClienteEndereco.est_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        [Required(ErrorMessage = "Cidade obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Cidade obrigatório")]
        public int cid_codigo
        {
            get 
            {
                if (objClienteEndereco == null)
                    return 0;
                else
                {
                    RaisePropertyChanged("est_codigo", false);
                    return objClienteEndereco.cid_codigo; 
                }
            }
            set
            {
                if (objClienteEndereco.cid_codigo != value)
                {
                    objClienteEndereco.cid_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        private List<tbEstado> _arrEstado;
        public List<tbEstado> arrEstado
        {
            get { return _arrEstado; }
            set
            {
                _arrEstado = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnCepFocus;
        public bool blnCepFocus
        {
            get { return _blnCepFocus; }
            set
            {
                _blnCepFocus = value;
                RaisePropertyChanged();
            }
        }

        private bool _blnComplementoFocus;
        public bool blnComplementoFocus
        {
            get { return _blnComplementoFocus; }
            set
            {
                _blnComplementoFocus = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private void Bairro(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                blnComplementoFocus = false;
                if (objParam.GetType() == typeof(tbBairro))
                {
                    if(((tbBairro)objParam).bai_codigo > 0)
                    {
                        objClienteEndereco.bai_codigo = ((tbBairro)objParam).bai_codigo;
                        objClienteEndereco.tbBairro.bai_nome = ((tbBairro)objParam).bai_nome;
                        objClienteEndereco.tbBairro.bai_realizaEntrega = ((tbBairro)objParam).bai_realizaEntrega;
                        _blnComplementoFocus = true;
                    }
                    else
                    {
                        objClienteEndereco.bai_codigo = 0;
                        objClienteEndereco.tbBairro.bai_nome = string.Empty;
                        objClienteEndereco.tbBairro.bai_realizaEntrega = false;
                    }
                    RaisePropertyChanged("bai_codigo");
                    RaisePropertyChanged("bai_nome");
                    RaisePropertyChanged("bai_realizaEntrega");
                    RaisePropertyChanged("blnComplementoFocus");
                }
                else if (objParam.ToString() == "Pesquisar")
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
                        Bairro(objBairroViewModel.objBairro);
                        objBairroViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new Bairros())
                    {
                        objRetorno = objBLL.RetornaBairro(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        Bairro((tbBairro)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        Bairro(new tbBairro());
                    }
                }
                else
                    Bairro("Pesquisar");
            }
        }

        private void Cidade(object objParam)
        {
            winCadastro objTelaCadastro = new winCadastro();
            CidadeViewModel objCidadeViewModel = new CidadeViewModel();
            objCidadeViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
            objCidadeViewModel.blnJanela = true;
            objTelaCadastro.Title = "Cadastro - " + objCidadeViewModel.strNomeTela;
            objTelaCadastro.DataContext = objCidadeViewModel;
            objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
            objTelaCadastro.Closed += (sen, eve) =>
            {
                if (objCidadeViewModel.objCidade != null)
                {
                    CarregaComboEstado();
                    objClienteEndereco.est_codigo = (int)objCidadeViewModel.objCidade.est_codigo;
                    RaisePropertyChanged("est_codigo");
                    objClienteEndereco.cid_codigo = (int)objCidadeViewModel.objCidade.cid_codigo;
                    RaisePropertyChanged("cid_codigo");
                }
                objCidadeViewModel = null;
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

        private void CarregaComboEstado()
        {
            Retorno objRetorno;
            using (var objBLL = new Cidades())
            {
                objRetorno = objBLL.RetornaListaEstado();
            }
            if (objRetorno.intCodigoErro == 0)
                arrEstado = (List<tbEstado>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}
