using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp36;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for wdCheckQRCode.xaml
    /// </summary>
    public partial class wdCheckQRCode : Window
    {
        LowLevelKeyboardListener _keyListener;
        public wdCheckQRCode()
        {
            InitializeComponent();

            //_keyListener = new LowLevelKeyboardListener();
            //_keyListener.OnKeyPressed += _keyListener_OnKeyPressed;
            //_keyListener.HookKeyboard();

            textboxData.Focus();
        }

        private void Event_PushEsc(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void textboxData_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(textboxData.Text.Trim().CompareTo(Common.Instance()._QRCode) == 0)
                {
                    lbResult.Content = "OK";
                    lbResult.Background = Brushes.Green;
                }
                else
                {
                    lbResult.Content = "NG";
                    lbResult.Background = Brushes.Red;
                }
            }
        }
    }
}
