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

        }
        bool CheckNull()
        {
            if (tbModelName.Text.Trim() != "" && tbStVolMin.Text.Trim() != "" && tbStVolMax.Text.Trim() != "" && tbChVolMin.Text.Trim() != "" &&
                tbChVolMax.Text.Trim() != "" && tbChCurMin.Text.Trim() != "" && tbChCurMax.Text.Trim() != "")
                return true;
            else
            {
                return false;
            }
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new UserDAO())
            {
                if (CheckNull())
                {
                    db.AddModel(tbModelName.Text.Trim(), tbStVolMin.Text.Trim(), tbStVolMax.Text.Trim(), tbChVolMin.Text.Trim()
                        , tbChVolMax.Text.Trim(), tbChCurMin.Text.Trim(), tbChCurMax.Text.Trim());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There must be no empty fields");
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
