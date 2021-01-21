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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for wdRegister.xaml
    /// </summary>
    public partial class wdRegister : Window
    {
        public string _ProductID = "";
        public wdRegister()
        {
            InitializeComponent();

        }
        const int ProductCode = 1;
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            KeyManager km = new KeyManager(_ProductID);
            string KeyActive = txtKeyActive.Text.Trim();
            //Check valid
            if (km.ValidKey(ref KeyActive))
            {
                KeyValuesClass kv = new KeyValuesClass();
                //Decrypt license key
                if (km.DisassembleKey(KeyActive, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = KeyActive;
                    lic.FullName = "SAMSUNG";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }
                    //Save license key to file
                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Environment.CurrentDirectory), lic);
                    System.Windows.Forms.MessageBox.Show("You have been successfully registered.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("Your product key is invalid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ProductID = Common.Instance().ProductID;
        }
    }
}
