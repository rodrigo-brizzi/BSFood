﻿using BSFood.View;
using BSFood.Apoio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class ProdutoGrupoViewModel : TelaViewModel
    {
        public ICommand NavegarCommand { get; set; }
        public ICommand NovoCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }
        public ICommand PesquisarCommand { get; set; }
        public ICommand LogCommand { get; set; }
        public ICommand AdicionaSubGrupoCommand { get; set; }

        public ProdutoGrupoViewModel()
        {
            NavegarCommand = new DelegateCommand(Navegar, CanNavegar);
            NovoCommand = new DelegateCommand(Novo, CanNovo);
            EditarCommand = new DelegateCommand(Editar, CanEditar);
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar, CanCancelar);
            ExcluirCommand = new DelegateCommand(Excluir, CanExcluir);
            PesquisarCommand = new DelegateCommand(Pesquisar, CanPesquisar);
            LogCommand = new DelegateCommand(Log, CanLog);
            AdicionaSubGrupoCommand = new DelegateCommand(AdicionaSubGrupo);
            Pesquisar(0);
        }


        #region Propriedades

        private string _strPgrCodigoPesquisa;
        public string strPgrCodigoPesquisa
        {
            get { return _strPgrCodigoPesquisa; }
            set
            {
                _strPgrCodigoPesquisa = value;
            }
        }

        private string _strPgrNomePesquisa;
        public string strPgrNomePesquisa
        {
            get { return _strPgrNomePesquisa; }
            set
            {
                _strPgrNomePesquisa = value;
            }
        }

        [Required(ErrorMessage = "Nome do grupo obrigatório")]
        [StringLength(100, ErrorMessage = "É permitido apenas 100 caracteres")]
        public string pgr_nome
        {
            get { return objProdutoGrupo == null ? string.Empty : objProdutoGrupo.pgr_nome; }
            set
            {
                if (objProdutoGrupo.pgr_nome != value)
                {
                    objProdutoGrupo.pgr_nome = value;
                    RaisePropertyChanged();
                }
            }
        }

        private tbProdutoGrupo _objProdutoGrupo;
        public tbProdutoGrupo objProdutoGrupo
        {
            get { return _objProdutoGrupo; }
            set
            {
                _objProdutoGrupo = value;
                //Prepara a coleção ProdutoSubGrupo
                if (_objProdutoGrupo != null)
                {
                    ObservableCollection<ProdutoSubGrupoViewModel> arrProdutoSubGrupoViewModelAux = new ObservableCollection<ProdutoSubGrupoViewModel>();
                    foreach (tbProdutoSubGrupo objProdutoSubGrupo in _objProdutoGrupo.tbProdutoSubGrupo)
                    {
                        ProdutoSubGrupoViewModel objProdutoSubGrupoViewModel = new ProdutoSubGrupoViewModel(objProdutoSubGrupo);
                        objProdutoSubGrupoViewModel.OnDispose += objProdutoSubGrupoViewModel_OnDispose;
                        arrProdutoSubGrupoViewModelAux.Add(objProdutoSubGrupoViewModel);
                    }
                    _arrProdutoSubGrupoViewModel = arrProdutoSubGrupoViewModelAux;
                }
                else
                    _arrProdutoSubGrupoViewModel = null;

                //Prepara propriedades da viewmodel
                RaisePropertyChanged(null);
            }
        }

        private ObservableCollection<ProdutoSubGrupoViewModel> _arrProdutoSubGrupoViewModel;
        public ObservableCollection<ProdutoSubGrupoViewModel> arrProdutoSubGrupoViewModel
        {
            get { return _arrProdutoSubGrupoViewModel; }
            set
            {
                _arrProdutoSubGrupoViewModel = value;
                RaisePropertyChanged();
            }
        }

        private List<tbProdutoGrupo> _arrProdutoGrupoPesquisa;
        public List<tbProdutoGrupo> arrProdutoGrupoPesquisa
        {
            get { return _arrProdutoGrupoPesquisa; }
            set
            {
                _arrProdutoGrupoPesquisa = value;
                RaisePropertyChanged();
                if (_arrProdutoGrupoPesquisa.Count > 0)
                    base.intSelectedIndexGrid = 0;
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
            tbProdutoGrupo objProdutoGrupoAux = new tbProdutoGrupo();
            objProdutoGrupoAux.tbProdutoSubGrupo = new List<tbProdutoSubGrupo>();
            tbProdutoSubGrupo objProdutoSubGrupo = new tbProdutoSubGrupo();
            objProdutoGrupoAux.tbProdutoSubGrupo.Add(objProdutoSubGrupo);
            objProdutoGrupo = objProdutoGrupoAux;
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
                using (var objBLL = new ProdutoGrupos())
                {
                    objRetorno = objBLL.RetornaProdutoGrupo((int)objParam, null);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objProdutoGrupo = (tbProdutoGrupo)objRetorno.objRetorno;
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

            bool blnAchouErro = false;
            foreach (ProdutoSubGrupoViewModel objProdutoSubGrupoViewModel in arrProdutoSubGrupoViewModel)
            {
                objProdutoSubGrupoViewModel.Validate();
                blnAchouErro = objProdutoSubGrupoViewModel.HasErrors;
                if (blnAchouErro)
                    break;
            }

            Validate();
            if (!HasErrors && !blnAchouErro)
            {
                objProdutoGrupo.tbProdutoSubGrupo.Clear();
                foreach (ProdutoSubGrupoViewModel objProdutoSubGrupoViewModel in arrProdutoSubGrupoViewModel)
                    objProdutoGrupo.tbProdutoSubGrupo.Add(objProdutoSubGrupoViewModel.objProdutoSubGrupo);

                Retorno objRetorno;
                using (var objBLL = new ProdutoGrupos())
                {
                    objRetorno = objBLL.SalvarProdutoGrupo(objProdutoGrupo, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objProdutoGrupo = null;
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
                objProdutoGrupo = null;
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
                    using (var objBLL = new ProdutoGrupos())
                    {
                        objRetorno = objBLL.ExcluirProdutoGrupo((int)objParam);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        objProdutoGrupo = null;
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
            if (objParam != null && objParam.GetType() == typeof(tbProdutoGrupo))
            {
                if (base.blnJanela)
                {
                    _objProdutoGrupo = (tbProdutoGrupo)objParam;
                    Dispose();
                }
            }
            else
            {
                int intSkip;
                if (objParam == null || !int.TryParse(objParam.ToString(), out intSkip))
                    intSkip = 0;

                Retorno objRetorno;
                using (var objBLL = new ProdutoGrupos())
                {
                    objRetorno = objBLL.RetornaListaProdutoGrupo(strPgrCodigoPesquisa, strPgrNomePesquisa, intSkip, base.intQtdeRegPagina);
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
                    arrProdutoGrupoPesquisa = (List<tbProdutoGrupo>)objRetorno.objRetorno;
                    if (arrProdutoGrupoPesquisa.Count() == 0)
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
            return base.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao && objProdutoGrupo != null && objProdutoGrupo.pgr_codigo > 0;
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
                        objRetorno = objBll.RetornaListaAuditoria(objProdutoGrupo.pgr_codigo, objProdutoGrupo);
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

        private void AdicionaSubGrupo(object objParam)
        {
            tbProdutoSubGrupo objProdutoSubGrupo = new tbProdutoSubGrupo();
            ProdutoSubGrupoViewModel objProdutoSubGrupoViewModel = new ProdutoSubGrupoViewModel(objProdutoSubGrupo);
            objProdutoSubGrupoViewModel.blnPsgrNomeFocus = true;
            objProdutoSubGrupoViewModel.OnDispose += objProdutoSubGrupoViewModel_OnDispose;
            arrProdutoSubGrupoViewModel.Add(objProdutoSubGrupoViewModel);
        }

        #endregion Comandos



        #region Eventos

        void objProdutoSubGrupoViewModel_OnDispose(object sender, EventArgs e)
        {
            arrProdutoSubGrupoViewModel.Remove((ProdutoSubGrupoViewModel)sender);
        }

        #endregion Eventos



        #region Métodos



        #endregion Métodos
    }
}