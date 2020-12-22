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
    /// Interaction logic for wdEditCoordinateQRCode.xaml
    /// </summary>
    public partial class wdEditCoordinateQRCode : Window
    {
        public delegate void dlgHandle(ref int xcoorQR, ref int ycoorQR, ref int xcoorMaterial, ref int ycoorMaterial);
        public event dlgHandle EventSaveAndExit;
        public wdEditCoordinateQRCode()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtXCoorQR.Text = Properties.Settings.Default.XCoorQR.ToString();
            txtYCoorQR.Text = Properties.Settings.Default.YCoorQR.ToString();

            txtXCoorMaterialCode.Text = Properties.Settings.Default.XCoorMaterial.ToString();
            txtYCoorMaterialCode.Text = Properties.Settings.Default.YCoorMaterial.ToString();
        }

        private void Event_PushEsc(object sender, ExecutedRoutedEventArgs e)
        {
            int x1 = int.Parse(txtXCoorQR.Text.Trim());
            int y1 = int.Parse(txtYCoorQR.Text.Trim());
            int x2 = int.Parse(txtXCoorMaterialCode.Text.Trim());
            int y2 = int.Parse(txtYCoorMaterialCode.Text.Trim());
            Properties.Settings.Default.XCoorQR = x1;
            Properties.Settings.Default.YCoorQR = y1;
            Properties.Settings.Default.XCoorMaterial = x2;
            Properties.Settings.Default.YCoorMaterial = y2;
            Properties.Settings.Default.Save();
            if (EventSaveAndExit != null)
                EventSaveAndExit.Invoke(ref x1, ref y1, ref x2, ref y2);
            this.Close();
        }
    }
}
