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
        public enum Mode { Add, Edit }
        public Mode _Mode
        {
            get; set;
        }

        public delegate void dlgDone();
        public event dlgDone EvAddEditDone;
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
                if (_Mode == Mode.Add)
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
                else if (_Mode == Mode.Edit)
                {
                    if (CheckNull())
                    {
                        string[] temp = new string[7] { tbModelName.Text.Trim(), tbStVolMin.Text.Trim(), tbStVolMax.Text.Trim(), tbChVolMin.Text.Trim()
                            , tbChVolMax.Text.Trim(), tbChCurMin.Text.Trim(), tbChCurMax.Text.Trim()};
                        db.EditModel(DefaultValues.Instance().ModelName, temp);
                        DefaultValues.Instance().ModelName = tbModelName.Text.Trim();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There must be no empty fields");
                    }
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Mode == Mode.Edit)
            {
                tbModelName.Text = DefaultValues.Instance().ModelName;
                tbStVolMin.Text = DefaultValues.Instance().StandbyVoltageMin.ToString();
                tbStVolMax.Text = DefaultValues.Instance().StandbyVoltageMax.ToString();
                tbChVolMin.Text = DefaultValues.Instance().ChargingVoltageMin.ToString();
                tbChVolMax.Text = DefaultValues.Instance().ChargingVoltageMax.ToString();
                tbChCurMin.Text = DefaultValues.Instance().ChargingCurrentMin.ToString();
                tbChCurMax.Text = DefaultValues.Instance().ChargingCurrentMax.ToString();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (EvAddEditDone != null)
            {
                EvAddEditDone.Invoke();
            }
        }
    }
}
