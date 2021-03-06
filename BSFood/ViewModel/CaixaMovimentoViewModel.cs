﻿using BSFood.Apoio;
using BSFood.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BSFoodFramework.Models;
using BSFoodFramework.DataTransfer;
using BSFoodFramework.BusinessLogic;
using BSFoodFramework.Apoio;
using System;

namespace BSFood.ViewModel
{
    public class CaixaMovimentoViewModel : ViewModelBase
    {
        public ICommand SalvarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand FormaPagamentoCommand { get; set; }
        public ICommand RemoveCaixaMovimentoCommand { get; set; }

        public CaixaMovimentoViewModel(tbCaixaMovimento _objCaixaMovimento)
        {
            SalvarCommand = new DelegateCommand(Salvar, CanSalvar);
            CancelarCommand = new DelegateCommand(Cancelar);
            FormaPagamentoCommand = new DelegateCommand(FormaPagamento);
            RemoveCaixaMovimentoCommand = new DelegateCommand(RemoveCaixaMovimento);
            this._objCaixaMovimento = _objCaixaMovimento;
        }


        #region Propriedades

        [Required(ErrorMessage = "Forma de pagamento obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Forma de pagamento obrigatória")]
        public int? fpg_codigo
        {
            get
            {
                if (objCaixaMovimento == null || objCaixaMovimento.fpg_codigo == 0)
                    return null;
                else
                    return objCaixaMovimento.fpg_codigo;
            }
            set
            {
                if (objCaixaMovimento.fpg_codigo != value)
                {
                    objCaixaMovimento.fpg_codigo = value == null ? 0 : (int)value;
                    FormaPagamento(objCaixaMovimento.fpg_codigo);
                }
            }
        }

        public string fpg_descricao
        {
            get { return objCaixaMovimento == null ? string.Empty : objCaixaMovimento.tbFormaPagamento.fpg_descricao; }
            set
            {
                if (objCaixaMovimento.tbFormaPagamento.fpg_descricao != value)
                {
                    objCaixaMovimento.tbFormaPagamento.fpg_descricao = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(ErrorMessage = "Operação obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Operação obrigatória")]
        public int caio_codigo
        {
            get { return objCaixaMovimento == null ? 0 : objCaixaMovimento.caio_codigo; }
            set
            {
                if (objCaixaMovimento.caio_codigo != value)
                {
                    objCaixaMovimento.caio_codigo = value;
                    RaisePropertyChanged();
                }
            }
        }

        [StringLength(250, ErrorMessage = "É permitido apenas 250 caracteres")]
        public string caim_observacao
        {
            get { return objCaixaMovimento == null ? string.Empty : objCaixaMovimento.caim_observacao; }
            set
            {
                if (objCaixaMovimento.caim_observacao != value)
                {
                    objCaixaMovimento.caim_observacao = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal caim_valor
        {
            get { return objCaixaMovimento == null ? 0 : objCaixaMovimento.caim_valor; }
            set
            {
                if (objCaixaMovimento.caim_valor != value)
                {
                    objCaixaMovimento.caim_valor = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _strFuncionario;
        public string strFuncionario
        {
            get { return _strFuncionario; }
            set
            {
                _strFuncionario = value;
                RaisePropertyChanged();
            }
        }

        private tbCaixaMovimento _objCaixaMovimento;
        public tbCaixaMovimento objCaixaMovimento
        {
            get { return _objCaixaMovimento; }
            set
            {
                _objCaixaMovimento = value;
                RaisePropertyChanged(null);
            }
        }

        private List<tbCaixaOperacao> _arrCaixaOperacao;
        public List<tbCaixaOperacao> arrCaixaOperacao
        {
            get { return _arrCaixaOperacao; }
            set
            {
                _arrCaixaOperacao = value;
                RaisePropertyChanged();
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

        private bool _blnValorFocus;
        public bool blnValorFocus
        {
            get { return _blnValorFocus; }
            set
            {
                _blnValorFocus = value;
                RaisePropertyChanged();
            }
        }

        #endregion Propriedades



        #region Comandos

        private bool CanSalvar(object objParam)
        {
            return _objCaixaMovimento != null;
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
                using (var objBLL = new Caixa())
                {
                    objRetorno = objBLL.LancarMovimentoCaixa(objCaixaMovimento, FrameworkUtil.objConfigStorage.objFuncionario.fun_codigo);
                }
                if (objRetorno.intCodigoErro == 0)
                {
                    objCaixaMovimento = null;
                    ClearAllErrorsAsync();
                    Dispose();
                }
                else
                    MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
            }
        }

        private void Cancelar(object objParam)
        {
            if (MessageBox.Show("Todas as alterações serão perdidas, deseja cancelar a edição?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                objCaixaMovimento = null;
                ClearAllErrorsAsync();
                Dispose();
            }
        }

        private void FormaPagamento(object objParam)
        {
            int intCodigo;
            if (objParam != null)
            {
                blnValorFocus = false;
                if (objParam.GetType() == typeof(tbFormaPagamento))
                {
                    if (((tbFormaPagamento)objParam).fpg_codigo > 0)
                    {
                        objCaixaMovimento.fpg_codigo = ((tbFormaPagamento)objParam).fpg_codigo;
                        objCaixaMovimento.tbFormaPagamento.fpg_descricao = ((tbFormaPagamento)objParam).fpg_descricao;
                        _blnValorFocus = true;
                    }
                    else
                    {
                        objCaixaMovimento.fpg_codigo = 0;
                        objCaixaMovimento.tbFormaPagamento.fpg_descricao = string.Empty;
                    }
                    RaisePropertyChanged("fpg_codigo");
                    RaisePropertyChanged("fpg_descricao");
                    RaisePropertyChanged("blnValorFocus");
                }
                else if (objParam.ToString() == "Pesquisar")
                {
                    winCadastro objTelaCadastro = new winCadastro();
                    FormaPagamentoViewModel objFormaPagamentoViewModel = new FormaPagamentoViewModel();
                    objFormaPagamentoViewModel.OnDispose += (sen1, eve1) => { objTelaCadastro.Close(); };
                    objFormaPagamentoViewModel.blnJanela = true;
                    objTelaCadastro.Title = "Cadastro - " + objFormaPagamentoViewModel.strNomeTela;
                    objTelaCadastro.DataContext = objFormaPagamentoViewModel;
                    objTelaCadastro.Owner = (Window)Application.Current.MainWindow;
                    objTelaCadastro.Closed += (sen, eve) =>
                    {
                        FormaPagamento(objFormaPagamentoViewModel.objFormaPagamento);
                        objFormaPagamentoViewModel = null;
                        objTelaCadastro = null;
                    };
                    objTelaCadastro.ShowDialog();
                }
                else if (int.TryParse(objParam.ToString(), out intCodigo))
                {
                    Retorno objRetorno;
                    using (var objBLL = new FormaPagamento())
                    {
                        objRetorno = objBLL.RetornaFormaPagamento(intCodigo, null);
                    }
                    if (objRetorno.intCodigoErro == 0)
                    {
                        FormaPagamento((tbFormaPagamento)objRetorno.objRetorno);
                    }
                    else
                    {
                        MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
                        FormaPagamento(new tbFormaPagamento());
                    }
                }
                else
                    FormaPagamento("Pesquisar");
            }
        }

        private void RemoveCaixaMovimento(object objParam)
        {
            Dispose();
        }


        #endregion Comandos



        #region Métodos

        public void CarregaComboOperacao()
        {
            Retorno objRetorno;
            using (var objBLL = new Caixa())
            {
                objRetorno = objBLL.RetornaListaCaixaOperacao();
            }
            if (objRetorno.intCodigoErro == 0)
                arrCaixaOperacao = (List<tbCaixaOperacao>)objRetorno.objRetorno;
            else
                MessageBox.Show(objRetorno.strMsgErro, "Atenção", MessageBoxButton.OK, Util.GetMessageImage(objRetorno.intCodigoErro));
        }

        #endregion Métodos
    }
}
