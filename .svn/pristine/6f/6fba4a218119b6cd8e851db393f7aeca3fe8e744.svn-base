using BSFood.View;
using BSFood.Apoio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;
using BSFoodFramework.Models;
using BSFoodFramework.Apoio;

namespace BSFood.ViewModel
{
    public class PrincipalViewModel : ViewModelBase
    {
        public ICommand AbreTelaLoginCommand { get; set; }
        public ICommand AbreTelaCommand { get; set; }

        public PrincipalViewModel()
        {
            AbreTelaLoginCommand = new DelegateCommand(AbreTelaLogin);
            AbreTelaCommand = new DelegateCommand(AbreTela);
            arrViewModel = new ObservableCollection<TelaViewModel>();
        }

        #region Propriedades

        private List<tbMenu> _arrMenu;
        public List<tbMenu> arrMenu 
        {
            get { return _arrMenu; }
            set
            {
                _arrMenu = value;
                RaisePropertyChanged();
            }
        }

        private List<tbMenu> _arrToolBar;
        public List<tbMenu> arrToolBar 
        {
            get { return _arrToolBar; }
            set
            {
                _arrToolBar = value;
                RaisePropertyChanged();
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

        private string _strVersao;
        public string strVersao 
        {
            get { return _strVersao; }
            set
            {
                _strVersao = value;
                RaisePropertyChanged();
            }
        }

        private string _strEmpresa;
        public string strEmpresa
        {
            get { return _strEmpresa; }
            set
            {
                _strEmpresa = value;
                RaisePropertyChanged();
            }
        }

        private string _strData;
        public string strData
        {
            get { return _strData; }
            set
            {
                _strData = value;
                RaisePropertyChanged();
            }
        }

        private TelaViewModel _objViewModel;
        public TelaViewModel objViewModel 
        {
            get { return _objViewModel; }
            set
            {
                _objViewModel = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TelaViewModel> arrViewModel { get; set; }

        #endregion Propriedades



        #region Comandos

        private void AbreTelaLogin(object objParam)
        {
            winLogin objTelaLogin = new winLogin();
            objTelaLogin.Owner = (Window)objParam;
            objTelaLogin.Closed += (sen, eve) =>
            {
                if (FrameworkUtil.objConfigStorage != null)
                    PreparaTelaPrincipal();
                else
                    Util.FecharSistema();
            };
            objTelaLogin.ShowDialog();
        }

        private void AbreTela(object objParam)
        {
            if(objParam.ToString() == "SairViewModel")
            {
                string strMensagem = string.Empty;
                if(arrViewModel.Count() > 0)
                    strMensagem = "Existem telas aberta, dados podem ser perdidos, deseja realmente sair?";
                else
                    strMensagem = "Tem certeza que deseja sair?";
                if (MessageBox.Show(strMensagem, "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                    Environment.Exit(0);
                    //Application.Current.Windows[0].Close();
                }
            }
            else
            {
                var objConteudoTelaExistente = arrViewModel.Where(vm => vm.strNomeControle == objParam.ToString()).FirstOrDefault();
                if (objConteudoTelaExistente == null)
                {
                    Type t = Type.GetType(objParam.ToString());
                    if (t != null)
                    {
                        TelaViewModel objConteudoTela = (TelaViewModel)Activator.CreateInstance(t);
                        objConteudoTela.OnDispose += objConteudoTela_OnDispose;
                        arrViewModel.Add(objConteudoTela);
                        objViewModel = objConteudoTela;
                    }
                    else
                        MessageBox.Show("Recurso não implementado", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                    objViewModel = objConteudoTelaExistente;
            }
        }

        #endregion Comandos



        #region Eventos

        void objConteudoTela_OnDispose(object sender, EventArgs e)
        {
            arrViewModel.Remove((TelaViewModel)sender);
        }

        #endregion Eventos

        

        #region Métodos

        public void PreparaTelaPrincipal()
        {
            //Construindo o Menu
            List<tbMenu> arrMenuAux = new List<tbMenu>();
            foreach (tbMenu objMenu in FrameworkUtil.objConfigStorage.objPerfilAcesso.tbPerfilAcessoMenu
                .Where(pam => pam.pam_permiteVisualizacao == true && pam.tbMenu.men_codigoPai == null)
                .Select(pam => pam.tbMenu).OrderBy(men => men.men_ordem))
            {
                tbMenu objMenuItem = new tbMenu() { men_imagem = objMenu.men_imagem, men_cabecalho = objMenu.men_cabecalho, men_nomeControle = objMenu.men_nomeControle };
                AgrupaMenuItem(ref objMenuItem, objMenu.men_codigo);
                arrMenuAux.Add(objMenuItem);
            }
            arrMenu = new List<tbMenu>(arrMenuAux);

            //Construindo a ToolBar
            List<tbMenu> arrToolBarAux = new List<tbMenu>();
            foreach (tbMenu objToolBar in FrameworkUtil.objConfigStorage.objPerfilAcesso.tbPerfilAcessoMenu
                .Where(pam => pam.pam_toolBar == true
                && pam.tbMenu.men_nomeControle != null
                && pam.pam_permiteVisualizacao == true).Select(per => per.tbMenu))
            {
                arrToolBarAux.Add(new tbMenu() { men_imagem = objToolBar.men_imagem, men_cabecalho = objToolBar.men_cabecalho, men_nomeControle = objToolBar.men_nomeControle });
            }
            //Adicionaodo o botão para sair
            arrToolBarAux.Add(new tbMenu() { men_imagem = "../Imagens/Menu/50.png", men_cabecalho = "Sair", men_nomeControle = "SairViewModel" });

            arrToolBar = new List<tbMenu>(arrToolBarAux);

            //Construindo a StatusBar
            strFuncionario = FrameworkUtil.objConfigStorage.objFuncionario.fun_nome;
            strVersao = FrameworkUtil.RetornaVersao();
            strEmpresa = FrameworkUtil.objConfigStorage.objEmpresa.emp_nomeFantasia;
            strData = DateTime.Now.ToString("dd/MM/yyyy");

            //Verificando a existencia dos arquivos da logo
            if(FrameworkUtil.objConfigStorage.objEmpresa.emp_logoFormato != null)
            {
                string[] arrCaminhoArquivo = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "logo.*", SearchOption.AllDirectories).ToArray();
                for (int i = 0; i < arrCaminhoArquivo.Length; i++)
                {
                    File.Delete(arrCaminhoArquivo[i]);
                }
                if(FrameworkUtil.objConfigStorage.objEmpresa.emp_logo != null)
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "logo" + FrameworkUtil.objConfigStorage.objEmpresa.emp_logoFormato, FrameworkUtil.objConfigStorage.objEmpresa.emp_logo);
            }
        }
        
        private void AgrupaMenuItem(ref tbMenu objMenuItem, int intCodigoMenu)
        {
            foreach (tbMenu objMenu in FrameworkUtil.objConfigStorage.objPerfilAcesso.tbPerfilAcessoMenu
                .Where(pam => pam.tbMenu.men_codigoPai == intCodigoMenu)
                .Select(pam => pam.tbMenu).OrderBy(men => men.men_ordem))
            {
                tbMenu objMenuAux = new tbMenu() { men_imagem = objMenu.men_imagem, men_cabecalho = objMenu.men_cabecalho, men_nomeControle = objMenu.men_nomeControle };
                AgrupaMenuItem(ref objMenuAux, objMenu.men_codigo);
                if (objMenuItem.tbMenuFilho == null)
                    objMenuItem.tbMenuFilho = new List<tbMenu>();
                objMenuItem.tbMenuFilho.Add(objMenuAux);
            }
        }
       
        #endregion Métodos
    }
}
