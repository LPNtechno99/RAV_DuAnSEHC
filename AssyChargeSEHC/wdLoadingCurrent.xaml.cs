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

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for wdLoadingCurrent.xaml
    /// </summary>
    public partial class wdLoadingCurrent : Window
    {
        //Command Set Voltage Current
        byte[] _cmdEnergyReset = new byte[10] { 255, 85, 17, 2, 1, 0, 0, 0, 0, 80 };
        byte[] _cmdReset = new byte[10] { 255, 85, 17, 2, 4, 0, 0, 0, 0, 83 };

        byte[] _cmdSetup = new byte[10] { 255, 85, 17, 2, 49, 0, 0, 0, 0, 0 };
        byte[] _cmdButtonONOFF = new byte[] { 255, 85, 17, 2, 50, 0, 0, 0, 0, 1 };
        byte[] _cmdButtonPlus = new byte[] { 255, 85, 17, 2, 51, 0, 0, 0, 0, 2 };
        byte[] _cmdButtonMinus = new byte[] { 255, 85, 17, 2, 52, 0, 0, 0, 0, 3 };

        const float _DefaultCurrent = 1.00f;
        public wdLoadingCurrent()
        {
            InitializeComponent();
        }
        async void SetValueCurrent()
        {
            float f1 = _DefaultCurrent;
            int number = 0;
            int[] arr = new int[3];
            int i = 0;
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
                    MainWindow.COM_MeasureVolCur.Write(_cmdButtonPlus, 0, _cmdButtonPlus.Length);
                    await Wait800MiliSecond();
                }
            }
            await Wait800MiliSecond();
            MainWindow.COM_MeasureVolCur.Write(_cmdSetup, 0, _cmdSetup.Length);
            if (arr[2] > 0)
            {
                await Wait800MiliSecond();
                for (int j = 0; j < arr[2]; j++)
                {
                    MainWindow.COM_MeasureVolCur.Write(_cmdButtonPlus, 0, _cmdButtonPlus.Length);
                    await Wait800MiliSecond();
                }
            }
            await Wait800MiliSecond();
            MainWindow.COM_MeasureVolCur.Write(_cmdSetup, 0, _cmdSetup.Length);
            await Wait800MiliSecond();
            MainWindow.COM_MeasureVolCur.Write(_cmdSetup, 0, _cmdSetup.Length);
            if (arr[0] > 0)
            {
                await Wait1Second();
                for (int j = 0; j < arr[0] + 1; j++)
                {
                    MainWindow.COM_MeasureVolCur.Write(_cmdButtonPlus, 0, _cmdButtonPlus.Length);
                    await Wait1Second();
                }
            }
            await Wait1Second();
            MainWindow.COM_MeasureVolCur.Write(_cmdSetup, 0, _cmdSetup.Length);
            await Wait1Second();
            await Wait1Second();
        }
        //Reset dong ho do vol Cur
        async void ResetDefault()
        {
            MainWindow.COM_MeasureVolCur.Write(_cmdSetup, 0, _cmdSetup.Length);
            await Wait1Second();
            for (int i = 0; i < 3; i++)
            {
                MainWindow.COM_MeasureVolCur.Write(_cmdButtonMinus, 0, _cmdButtonMinus.Length);
                await Wait800MiliSecond();
            }
            await Wait800MiliSecond();
            for (int j = 0; j < 3; j++)
            {
                MainWindow.COM_MeasureVolCur.Write(_cmdSetup, 0, _cmdSetup.Length);
                await Wait800MiliSecond();
            }
        }
        private async Task Wait2Second()
        {
            await Task.Delay(2000);
        }
        private async Task Wait1Second()
        {
            await Task.Delay(1000);
        }
        private async Task Wait1500MiliSecond()
        {
            await Task.Delay(1500);
        }
        private async Task Wait500MiliSecond()
        {
            await Task.Delay(500);
        }
        private async Task Wait800MiliSecond()
        {
            await Task.Delay(800);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.COM_MeasureVolCur.IsOpen)
                MainWindow.COM_MeasureVolCur.Open();
            ResetDefault();
            SetValueCurrent();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MainWindow.COM_MeasureVolCur.IsOpen)
                MainWindow.COM_MeasureVolCur.Close();
        }
    }
}
