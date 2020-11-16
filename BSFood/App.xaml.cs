using BSFoodFramework.Apoio;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BSFood
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));
            EventManager.RegisterClassHandler(typeof(CheckBox), CheckBox.KeyDownEvent, new KeyEventHandler(CheckBox_KeyDown));
            EventManager.RegisterClassHandler(typeof(ComboBox), ComboBox.KeyDownEvent, new KeyEventHandler(ComboBox_KeyDown));
            EventManager.RegisterClassHandler(typeof(RadioButton), RadioButton.KeyDownEvent, new KeyEventHandler(RadioButton_KeyDown));
            EventManager.RegisterClassHandler(typeof(PasswordBox), PasswordBox.KeyDownEvent, new KeyEventHandler(PasswordBox_KeyDown));
            //EventManager.RegisterClassHandler(typeof(TabItem), TabItem.KeyDownEvent, new KeyEventHandler(TabItem_KeyDown));
            FrameworkUtil.CarregarConfiguracao();
            FrameworkUtil.objGerenciaCupom = new GerenciaCupom();
            this.StartupUri = new System.Uri("View/winPrincipal.xaml", System.UriKind.Relative);
        }

        #region Eventos

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (sender as TextBox).AcceptsReturn == false && (sender as TextBox).Tag == null)
            {
                MoveToNextUIElement(e);
            }
        }

        void CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (sender as CheckBox).Tag == null)
                MoveToNextUIElement(e);
        }

        void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !(sender as ComboBox).IsDropDownOpen)
                MoveToNextUIElement(e);
        }

        void RadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (sender as RadioButton).Tag == null)
                MoveToNextUIElement(e);
        }

        void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                MoveToNextUIElement(e);
        }

        //void TabItem_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //        MoveToNextUIElement(e);
        //}

        void MoveToNextUIElement(KeyEventArgs e)
        {
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

            // MoveFocus takes a TraveralReqest as its argument.
            TraversalRequest request = new TraversalRequest(focusDirection);

            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                if (elementWithFocus.MoveFocus(request)) e.Handled = true;
            }
        }

        #endregion Eventos
    }
}
