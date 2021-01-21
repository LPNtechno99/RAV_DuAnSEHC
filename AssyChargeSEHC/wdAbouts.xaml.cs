using FoxLearn.License;
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
    /// Interaction logic for wdAbouts.xaml
    /// </summary>
    public partial class wdAbouts : Window
    {
        string _ProductID = String.Empty;
        public wdAbouts()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        const int ProductCode = 1;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ProductID = Common.Instance().ProductID;
            KeyManager km = new KeyManager(_ProductID);
            LicenseInfo lic = new LicenseInfo();
            //Get license information from license file
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Environment.CurrentDirectory), ref lic);
            string productKey = lic.ProductKey;
            //Check valid
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                //Decrypt license key
                if (km.DisassembleKey(productKey, ref kv))
                {
                    lblProductName.Content = "SAMSUNG";
                    lblProductKey.Content = productKey;
                    lblExpirationDay.Content = kv.Expiration.ToString("dd/MM/yyyy");
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lblRemainDay.Content = string.Format("{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                        lblLicenseType.Content = "TRIAL";
                    }
                    else
                        lblLicenseType.Content = "FULL";
                }
            }
        }
    }
}
