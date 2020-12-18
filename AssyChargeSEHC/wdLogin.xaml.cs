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
using AssyChargeSEHC.DAO;
using AssyChargeSEHC.ModelEF;

namespace AssyChargeSEHC
{

    public enum LoginState { Success, Fail, Null }
    /// <summary>
    /// Interaction logic for wdLogin.xaml
    /// </summary>
    public partial class wdLogin : Window
    {
        public delegate void dlgLogin(LoginState logSt, int roleId);
        public event dlgLogin EventLogin;
        public wdLogin()
        {
            InitializeComponent();

            txtRole.Focus();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var dao = new UserDAO())
            {
                int roleID = dao.GetRoleID(txtRole.Text.Trim(), pbPassword.Password.Trim());
                Common.Instance().RoleID = roleID;
                switch (roleID)
                {
                    case 0:
                        if (EventLogin != null)
                        {
                            EventLogin.Invoke(LoginState.Fail, roleID);
                            dao.AddNewAction(DateTime.Now.ToString("ddMMyyyyHHmmssfff"), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "Sign In Fail");
                        }
                        break;
                    case 1:
                        if (EventLogin != null)
                        {
                            EventLogin.Invoke(LoginState.Success, roleID);
                            dao.AddNewAction(DateTime.Now.ToString("ddMMyyyyHHmmssfff"), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "Sign In with Admin Role");
                            this.Close();
                        }
                        break;
                    case 2:
                        if (EventLogin != null)
                        {
                            EventLogin.Invoke(LoginState.Success, roleID);
                            dao.AddNewAction(DateTime.Now.ToString("ddMMyyyyHHmmssfff"), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "Sign In with Worker Role");
                            this.Close();
                        }
                        break;
                    default:
                        if (EventLogin != null)
                        {
                            EventLogin.Invoke(LoginState.Null, roleID);
                        }
                        break;
                }
            }
        }

        private void pbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btLogin_Click(null, null);
            }
        }
    }
}
