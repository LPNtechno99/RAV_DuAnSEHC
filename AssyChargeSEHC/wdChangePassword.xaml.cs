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
using AssyChargeSEHC.ModelEF;
using AssyChargeSEHC.DAO;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for wdChangePassword.xaml
    /// </summary>
    public partial class wdChangePassword : Window
    {
        public delegate void dlgChangePassword();
        public event dlgChangePassword EventChangePassword;
        public wdChangePassword()
        {
            InitializeComponent();

            using (var dao = new UserDAO())
            {
                cbbRole.ItemsSource = dao.GetRole();
                cbbRole.SelectedIndex = 0;
            }
        }

        private void btChange_Click(object sender, RoutedEventArgs e)
        {
            if (cbbRole.SelectedItem != null)
            {
                using (var dao = new UserDAO())
                {
                    dao.EditPassword(cbbRole.SelectedItem.ToString(), pbPassword.Password.Trim());
                    dao.AddNewAction(DateTime.Now.ToString("ddMMyyyyHHmmssfff"), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "Change Password");
                    EventChangePassword?.Invoke();
                }
                this.Close();
            }
        }
        private void pbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btChange_Click(null, null);
            }
        }
    }
}
