using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BSFood.ViewModel
{
    public class ViewModelBase : INotifyDataErrorInfo, INotifyPropertyChanged, IDisposable
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null, bool blnValidar = false)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
            if (blnValidar)
                ValidateAsync(propertyName);
        }

        #endregion INotifyPropertyChanged



        #region INotifyDataErrorInfo

        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged(string propertyName)
        {
            var handler = ErrorsChanged;
            if (handler != null)
                handler(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForName = null;
            if (propertyName != null)
                _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        public Task ValidateAsync(string propertyName = null)
        {
            return Task.Run(() => Validate(propertyName));
        }

        private object _lock = new object();
        public void Validate(string propertyName = null)
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.Where(k => k.Key == propertyName || propertyName == null).ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outLi;
                        _errors.TryRemove(kv.Key, out outLi);
                        OnErrorsChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q.Where(k => k.Key == propertyName || propertyName == null))
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }

        public Task ClearAllErrorsAsync()
        {
            return Task.Run(() => ClearAllErrors());
        }

        public void ClearAllErrors()
        {
            lock (_lock)
            {
                foreach (var kv in _errors.ToList())
                {
                    List<string> outLi;
                    _errors.TryRemove(kv.Key, out outLi);
                    OnErrorsChanged(kv.Key);
                }
            }
        }

        #endregion INotifyDataErrorInfo



        #region IDisposable

        public event EventHandler OnDispose;

        public void Dispose()
        {
            PropertyChanged = null;
            ErrorsChanged = null;
            var handler = OnDispose;
            if (handler != null)
                handler(this, null);
        }

        #endregion IDisposable
    }
}
//using BSFood.Apoio;
//using BSFood.Models;
//using System;
//using System.Collections;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;

//namespace BSFood.ViewModel
//{
//    public class ViewModelBase : INotifyDataErrorInfo, INotifyPropertyChanged, IDisposable
//    {
//        public ICommand FecharCommand { get; set; }

//        public ViewModelBase()
//        {
//            //Recupero definições da viewmodel, como nome do controle, titulo da tela, imagem que a representa e permissões
//            if(FrameworkUtil.objConfigStorage != null && FrameworkUtil.objConfigStorage.objPerfilAcesso != null)
//            {
//                this.strNomeControle = "BSFood.ViewModel." + this.GetType().Name;
//                tbPerfilAcessoMenu objPerfilAcessoMenuAux = FrameworkUtil.objConfigStorage.objPerfilAcesso.tbPerfilAcessoMenu.Where(pam => pam.tbMenu.men_nomeControle == strNomeControle).FirstOrDefault();
//                if (objPerfilAcessoMenuAux != null)
//                {
//                    this.strImagemTela = objPerfilAcessoMenuAux.tbMenu.men_imagem;
//                    this.strNomeTela = objPerfilAcessoMenuAux.tbMenu.men_cabecalho;
//                    this.blnPermiteInclusaoRegistro = objPerfilAcessoMenuAux.pam_permiteInclusao;
//                    this.blnPermiteAlteracaoRegistro = objPerfilAcessoMenuAux.pam_permiteAlteracao;
//                    this.blnPermiteExclusaoRegistro = objPerfilAcessoMenuAux.pam_permiteExclusao;
//                }
//            }
//            FecharCommand = new DelegateCommand(Fechar, CanFechar);
//            this.enStatusTelaAtual = enStatusTela.Padrao;
//        }

//        #region Propriedades Comuns

//        public string strNomeControle { get; set; }

//        public string strImagemTela { get; set; }

//        public string strNomeTela { get; set; }

//        public enStatusTela enStatusTelaAtual { get; set; }

//        public bool blnPermiteInclusaoRegistro { get; set; }

//        public bool blnPermiteAlteracaoRegistro { get; set; }

//        public bool blnPermiteExclusaoRegistro { get; set; }

//        public List<tbAuditoria> arrAuditoria { get; set; }

//        private int _intSelectedIndexGrid;
//        public int intSelectedIndexGrid
//        {
//            get { return _intSelectedIndexGrid; }
//            set 
//            {
//                this._intSelectedIndexGrid = value;
//                RaisePropertyChanged("intSelectedIndexGrid", false);
//            }
//        }

//        #endregion



//        #region Comandos de primeiro nível

//        private bool CanFechar(object objParam)
//        {
//            return true;
//        }
//        private void Fechar(object objParam)
//        {
//            if (this.enStatusTelaAtual == enStatusTela.EmInclusaoOuAlteracao)
//            {
//                if (MessageBox.Show("Todas as alterações serão perdidas, deseja fechar a tela?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
//                {
//                    this.Dispose();
//                }
//            }
//            else
//            {
//                this.Dispose();
//            }
//        }

//        #endregion



//        #region INotifyPropertyChanged

//        public event PropertyChangedEventHandler PropertyChanged;

//        public void RaisePropertyChanged([CallerMemberName] string propertyName = null, bool blnValidar = true)
//        {
//            var handler = PropertyChanged;
//            if (handler != null)
//                handler(this, new PropertyChangedEventArgs(propertyName));
//            if(blnValidar)
//                ValidateAsync(propertyName);
//        }

//        #endregion INotifyPropertyChanged



//        #region INotifyDataErrorInfo

//        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

//        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

//        public void OnErrorsChanged(string propertyName)
//        {
//            var handler = ErrorsChanged;
//            if (handler != null)
//                handler(this, new DataErrorsChangedEventArgs(propertyName));
//        }

//        public IEnumerable GetErrors(string propertyName)
//        {
//            List<string> errorsForName = null;
//            if (propertyName != null)
//                _errors.TryGetValue(propertyName, out errorsForName);
//            return errorsForName;
//        }

//        public bool HasErrors
//        {
//            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
//        }

//        public Task ValidateAsync(string propertyName = null)
//        {
//            return Task.Run(() => Validate(propertyName));
//        }

//        private object _lock = new object();
//        public void Validate(string propertyName = null)
//        {
//            lock (_lock)
//            {
//                var validationContext = new ValidationContext(this, null, null);
//                var validationResults = new List<ValidationResult>();
//                Validator.TryValidateObject(this, validationContext, validationResults, true);

//                foreach (var kv in _errors.Where(k => k.Key == propertyName || propertyName == null).ToList())
//                {
//                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
//                    {
//                        List<string> outLi;
//                        _errors.TryRemove(kv.Key, out outLi);
//                        OnErrorsChanged(kv.Key);
//                    }
//                }

//                var q = from r in validationResults
//                        from m in r.MemberNames
//                        group r by m into g
//                        select g;

//                foreach (var prop in q.Where(k => k.Key == propertyName || propertyName == null))
//                {
//                    var messages = prop.Select(r => r.ErrorMessage).ToList();

//                    if (_errors.ContainsKey(prop.Key))
//                    {
//                        List<string> outLi;
//                        _errors.TryRemove(prop.Key, out outLi);
//                    }
//                    _errors.TryAdd(prop.Key, messages);
//                    OnErrorsChanged(prop.Key);                    
//                }
//            }
//        }

//        public Task ClearAllErrorsAsync()
//        {
//            return Task.Run(() => ClearAllErrors());
//        }

//        public void ClearAllErrors()
//        {
//            lock (_lock)
//            {
//                foreach (var kv in _errors.ToList())
//                {
//                    List<string> outLi;
//                    _errors.TryRemove(kv.Key, out outLi);
//                    OnErrorsChanged(kv.Key);
//                }
//            }        
//        }

//        #endregion INotifyDataErrorInfo



//        #region IDisposable

//        public event EventHandler OnDispose;

//        public void Dispose()
//        {
//            PropertyChanged = null;
//            ErrorsChanged = null;
//            FecharCommand = null;
//            var handler = OnDispose;
//            if (handler != null)
//                handler(this, null);
//        }

//        #endregion IDisposable
//    }
//}