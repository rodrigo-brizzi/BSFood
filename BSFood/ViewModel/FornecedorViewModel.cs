﻿using BSFood.View;
using BSFood.Apoio;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class FornecedorViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand NovoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand CidadeCommand { get; set; }

        public FornecedorViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            EditarCommand = new DelegateCommand(Editar, CanEditar);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ExcluirCommand = new DelegateCommand(Excluir, CanExcluir);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);
            LogCommand = new DelegateCommand(Log, CanLog);
            CidadeCommand = new DelegateCommand(Cidade);
            CarregaComboEstado();
            Pesquisar(0);
        }


        #region Propriedades

        private string _strForCodigoPesquisa;
        public string strForCodigoPesquisa
        {
            get { return _strForCodigoPesquisa; }
            set
            {
                _strForCodigoPesquisa = value;
            }
        }

        private string _strForNomePesquisa;
        public string strForNomePesquisa
        {
            get { return _strForNomePesquisa; }
            set
            {
                _strForNomePesquisa = value;
            }
        }

        [Required(ErrorMessage = "Nome do Fornecedor obrigatório")]
        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string for_nome
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_nome; }
            set
            {
                if (objFornecedor.for_nome != value)
                {
                    objFornecedor.for_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(200, ErrorMessage = "É permitido apenas 200 caracteres")]
        public string for_logradouro
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_logradouro; }
            set
            {
                if (objFornecedor.for_logradouro != value)
                {
                    objFornecedor.for_logradouro = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(10, ErrorMessage = "É permitido apenas 10 caracteres")]
        public string for_numero
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_numero; }
            set
            {
                if (objFornecedor.for_numero != value)
                {
                    objFornecedor.for_numero = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string for_bairro
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_bairro; }
            set
            {
                if (objFornecedor.for_bairro != value)
                {
                    objFornecedor.for_bairro = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(9, ErrorMessage = "É permitido apenas 9 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string for_cep
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_cep; }
            set
            {
                if (objFornecedor.for_cep != value)
                {
                    objFornecedor.for_cep = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(150, ErrorMessage = "É permitido apenas 150 caracteres")]
        public string for_complemento
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_complemento; }
            set
            {
                if (objFornecedor.for_complemento != value)
                {
                    objFornecedor.for_complemento = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string for_telefone
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_telefone; }
            set
            {
                if (objFornecedor.for_telefone != value)
                {
                    objFornecedor.for_telefone = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string for_fax
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_fax; }
            set
            {
                if (objFornecedor.for_fax != value)
                {
                    objFornecedor.for_fax = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(150, ErrorMessage = "É permitido apenas 150 caracteres")]
        public string for_contato
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_contato; }
            set
            {
                if (objFornecedor.for_contato != value)
                {
                    objFornecedor.for_contato = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string for_email
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_email; }
            set
            {
                if (objFornecedor.for_email != value)
                {
                    objFornecedor.for_email = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string for_site
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_site; }
            set
            {
                if (objFornecedor.for_site != value)
                {
                    objFornecedor.for_site = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string for_cnpj
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_cnpj; }
            set
            {
                if (objFornecedor.for_cnpj != value)
                {
                    objFornecedor.for_cnpj = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(20, ErrorMessage = "É permitido apenas 20 caracteres")]
        [Range(0, double.MaxValue, ErrorMessage = "Informe apenas números")]
        public string for_ie
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_ie; }
            set
            {
                if (objFornecedor.for_ie != value)
                {
                    objFornecedor.for_ie = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(250, ErrorMessage = "É permitido apenas 250 caracteres")]
        public string for_observacao
        {
            get { return objFornecedor == null ? string.Empty : objFornecedor.for_observacao; }
            set
            {
                if (objFornecedor.for_observacao != value)
                {
                    objFornecedor.for_observacao = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Estado obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Estado obrigatório")]
        public int est_codigo
        {
            get { return objFornecedor == null ? 0 : objFornecedor.est_codigo; }
            set
            {
                if (objFornecedor.est_codigo != value)
                {
                    objFornecedor.est_codigo = value;
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
                if (objFornecedor == null)
                    return 0;
                else
                {
                    RaisePropertyChanged("est_codigo", false);
                    return objFornecedor.cid_codigo;
                }
            }
            set
            {
                if (objFornecedor.cid_codigo != value)
                {
                    objFornecedor.cid_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        private tbFornecedor _objFornecedor;
        public tbFornecedor objFornecedor
        {
            get { return _objFornecedor; }
            set
            {
                _objFornecedor = value;
                RaisePropertyChanged(null);
            }
        }

        private List<tbFornecedor> _arrFornecedorPesquisa;
        public List<tbFornecedor> arrFornecedorPesquisa
        {
            get { return _arrFornecedorPesquisa; }
            set
            {
                _arrFornecedorPesquisa = value;
                RaisePropertyChanged();
                if (_arrFornecedorPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
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

        public List<tbAuditoria> arrAuditoria { get; set; }

        #endregion Propriedades



        #region Comandos

        private bool CanNavegar(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao);
        }
        private void Navegar(object objParam)
        {
            if (objParam != null)
            {
                if (objParam.ToString() == "Proximo")
                {
                    if (base.intPaginaAtual < base.intTotalPagina)
                    {
                        Pesquisar(base.intPaginaAtual * base.intQtdeRegPagina);
                        base.intPaginaAtual++;
                    }
                }
                else if (objParam.ToString() == "Anterior")
                {
                    if (base.intPaginaAtual > 1)
                    {
                        base.intPaginaAtual--;
                        if (base.intPaginaAtual == 1)
                            Pesquisar(0);
                        else
                            Pesquisar((base.intPaginaAtual - 1) * base.intQtdeRegPagina);
                    }
                }
            }
        }

        private bool CanNovo(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteInclusaoRegistro;
        }
        private void Novo(object objParam)
        {
            tbFornecedor objFornecedorAux = new tbFornecedor();
            objFornecedorAux.est_codigo = FrameworkUtil.objConfigStorage.objEmpresa.est_codigo;
            objFornecedorAux.cid_codigo = FrameworkUtil.objConfigStorage.objEmpresa.cid_codigo;
            objFornecedor = objFornecedorAux;
            base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
        }

        private bool CanEditar(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteAlteracaoRegistro);
        }
        private void Editar(object objParam)
        {
            if (objParam != null)
            {
                Retorno objRetorno;
                using (var objBLL = new Fornecedores())
                {
                    objRetorno = objBLL.RetornaFornecedor((int)objParam, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFornecedor = (tbFornecedor)objRetorno.objRetorno;
                    base.enStatusTelaAtual = enStatusTela.EmInclusaoOuAlteracao;
                }
                else
                {
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
            }
        }

        private bool CanSalvar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao;
        }
        private void Salvar(object objParam)
        {
            var focusedElement = Keyboard.FocusedElement as FrameworkElement;
            if (focusedElement is TextBox)
            {
                var expression = focusedElement.GetBindingExpression(TextBox.TextProperty);
                if (expression != null && expression.ParentBinding.UpdateSourceTrigger == System.Windows.Data.UpdateSourceTrigger.LostFocus)
                    expression.UpdateSource();
            }

            Validate();
            if (!HasErrors)
            {
                Retorno objRetorno;
                using (var objBLL = new Fornecedores())
                {
                    objRetorno = objBLL.SalvarFornecedor(objFornecedor, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objFornecedor = null;
                    ClearAllErrorsAsync();
                    base.enStatusTelaAtual = enStatusTela.Navegacao;
                    Pesquisar(null);
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private bool CanCancelar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao;
        }
        private void Cancelar(object objParam)
        {
            if (MessageBox.Show("Todas as alterações serão perdidas, deseja cancelar a edição?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objFornecedor = null;
                ClearAllErrorsAsync();
                base.enStatusTelaAtual = enStatusTela.Navegacao;
                Pesquisar(null);
            }
        }

        private bool CanExcluir(object objParam)
        {
            return (base.enStatusTelaAtual == enStatusTela.Navegacao && base.blnPermiteExclusaoRegistro);
        }
        private void Excluir(object objParam)
        {
            if (objParam != null)
            {
                if (MessageBox.Show("Tem Certeza que deseja excluir o registro selecionado?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Retorno objRetorno;
                    using (var objBLL = new Fornecedores())
                    {
                        objRetorno = objBLL.ExcluirFornecedor((int)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objFornecedor = null;
                        ClearAllErrorsAsync();
                        base.enStatusTelaAtual = enStatusTela.Navegacao;
                        Pesquisar(null);
                    }
                    else
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
            }
        }

        private bool CanPesquisar(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.Navegacao;
        }
        public void Pesquisar(object objParam)
        {
            if (objParam != null && objParam.GetType() == typeof(tbFornecedor))
            {
                if (base.blnJanela)
                {
                    _objFornecedor = (tbFornecedor)objParam;
                    Dispose();
                }
            }
            else
            {
                int intSkip;
                if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                    intSkip = 0;

                Retorno objRetorno;
                using (var objBLL = new Fornecedores())
                {
                    objRetorno = objBLL.RetornaListaFornecedor(strForCodigoPesquisa, strForNomePesquisa, intSkip, base.intQtdeRegPagina);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    if (objRetorno.intQtdeRegistro > 0)
                    {
                        if ((objRetorno.intQtdeRegistro % base.intQtdeRegPagina) > 0)
                            base.intTotalPagina = (int)(objRetorno.intQtdeRegistro / base.intQtdeRegPagina) + 1;
                        else
                            base.intTotalPagina = (int)(objRetorno.intQtdeRegistro / base.intQtdeRegPagina);
                        base.intPaginaAtual = 1;
                        base.intQtdeReg = objRetorno.intQtdeRegistro;
                    }
                    arrFornecedorPesquisa = (List<tbFornecedor>)objRetorno.objRetorno;
                    if (arrFornecedorPesquisa.Count() == 0)
                    {
                        base.intTotalPagina = 1;
                        base.intPaginaAtual = 1;
                        base.intQtdeReg = 0;
                    }
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private bool CanLog(object objParam)
        {
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao && objFornecedor != null && objFornecedor.for_codigo > 0;
        }
        private void Log(object objParam)
        {
            if (objParam != null)
            {
                if (objParam.ToString() == "AbrirTela")
                {
                    Retorno objRetorno;
                    using (var objBll = new Auditoria())
                    {
                        objRetorno = objBll.RetornaListaAuditoria(objFornecedor.for_codigo, objFornecedor);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        arrAuditoria = (List<tbAuditoria>)objRetorno.objRetorno;
                        winAuditoria objTelaAuditoria = new winAuditoria();
                        objTelaAuditoria.Title = "Auditoria - " + base.strNomeTela;
                        objTelaAuditoria.DataContext = this;
                        objTelaAuditoria.Owner = (Window)Application.Current.MainWindow;
                        objTelaAuditoria.Closed += (sen, eve) =>
                        {
                            arrAuditoria = null;
                            objTelaAuditoria = null;
                        };
                        objTelaAuditoria.ShowDialog();
                    }
                    else
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                }
                else if (objParam.GetType() == typeof(winAuditoria))
                {
                    ((Window)objParam).Close();
                }
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
                    objFornecedor.est_codigo = (int)objCidadeViewModel.objCidade.est_codigo;
                    RaisePropertyChanged("est_codigo");
                    objFornecedor.cid_codigo = (int)objCidadeViewModel.objCidade.cid_codigo;
                    RaisePropertyChanged("cid_codigo");
                }
                objCidadeViewModel = null;
                objTelaCadastro = null;
            };
            objTelaCadastro.ShowDialog();
        }

        #endregion Comandos



        #region Eventos



        #endregion Eventos



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