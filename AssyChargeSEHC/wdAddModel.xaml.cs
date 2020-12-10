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
    /// Interaction logic for wdAddModel.xaml
    /// </summary>
    public partial class wdAddModel : Window
    {
        public wdAddModel()
        {
            InitializeComponent();

            tbAddModel.Focus();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (var db = new UserDAO())
                {
                    db.AddModel(tbAddModel.Text.Trim());
                }
                this.Close();
            }
        }
    }
}
