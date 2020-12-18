using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for wdSetCurrent.xaml
    /// </summary>
    public partial class wdSetCurrent : Window
    {
        SerialPort COM_IR;

        //Command Set Voltage Current
        byte[] _cmdReset = new byte[10] { 255, 85, 17, 2, 1, 0, 0, 0, 0, 80 };

        byte[] _cmdSetup = new byte[10] { 255, 85, 17, 2, 49, 0, 0, 0, 0, 0 };
        byte[] _cmdButtonONOFF = new byte[] { 255, 85, 17, 2, 50, 0, 0, 0, 0, 1 };
        byte[] _cmdButtonPlus = new byte[] { 255, 85, 17, 2, 51, 0, 0, 0, 0, 2 };
        byte[] _cmdButtonMinus = new byte[] { 255, 85, 17, 2, 52, 0, 0, 0, 0, 3 };
        public wdSetCurrent()
        {
            InitializeComponent();

            if (!COM_IR.IsOpen)
                COM_IR.Open();
            Thread.Sleep(5);
            COM_IR.Write(_cmdReset, 0, _cmdReset.Length);

            tbCurrent.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurrent.Text == "")
            {
                return;
            }
            float f1 = float.Parse(tbCurrent.Text.Trim());
            int number = 0;
            int[] arr = new int[3];
            int i = 0;
            if (f1 < 5.0f)
                number = (int)(f1 * 100f);
            while (number > 0)
            {
                arr[i] = number % 10;
                i++;
                number /= 10;
            }
            if (arr[1] > 0)
            {
                for (int j = 0; j < arr[1]; j++)
                {
                    COM_IR.Write(_cmdButtonPlus, 0, _cmdButtonPlus.Length);
                }
            }
            if (arr[2] > 0)
            {
                COM_IR.Write(_cmdSetup, 0, _cmdSetup.Length);
                for (int j = 0; j < arr[2]; j++)
                {
                    COM_IR.Write(_cmdButtonPlus, 0, _cmdButtonPlus.Length);
                }
            }
            if(arr[0] > 0)
            {
                COM_IR.Write(_cmdSetup, 0, _cmdSetup.Length);
                Thread.Sleep(5);
                COM_IR.Write(_cmdSetup, 0, _cmdSetup.Length);
                for (int j = 0; j < arr[0]; j++)
                {
                    COM_IR.Write(_cmdButtonPlus, 0, _cmdButtonPlus.Length);
                }
            }
            COM_IR.Write(_cmdSetup, 0, _cmdSetup.Length);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            COM_IR.Close();
        }
    }
}
