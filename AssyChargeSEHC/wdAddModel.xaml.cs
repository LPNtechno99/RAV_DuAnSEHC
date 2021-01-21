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
            if (txtMaterialCode.Text.Trim() != "" && txtUnitCode.Text.Trim() !="" && txtSupplierCode.Text.Trim() != "" && txtCountryCode.Text.Trim() != ""
                && txtProductionLine.Text.Trim() != "" && txtInspecEquipNumber.Text.Trim() != "" && txtNumberOfInspecItem.Text.Trim() != ""
                && txtInspecItemStVol.Text.Trim() != "" && txtInspecItemChVol.Text.Trim() != "" && txtInspecItemChCur.Text.Trim() != ""
                && tbStVolMin.Text.Trim() != "" && tbStVolMax.Text.Trim() != "" && tbChVolMin.Text.Trim() != "" && tbChVolMax.Text.Trim() != "" && tbChCurMin.Text.Trim() != "" && tbChCurMax.Text.Trim() != "")
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
                        List<string> lst = new List<string>();

                        lst.Add(txtUnitCode.Text.Trim());
                        lst.Add(txtMaterialCode.Text.Trim());
                        lst.Add(txtSupplierCode.Text.Trim());
                        lst.Add(txtCountryCode.Text.Trim());
                        lst.Add(txtProductionLine.Text.Trim());
                        lst.Add(txtInspecEquipNumber.Text.Trim());
                        lst.Add(txtNumberOfInspecItem.Text.Trim());
                        lst.Add(txtInspecItemStVol.Text.Trim());
                        lst.Add(tbStVolMax.Text.Trim());
                        lst.Add(tbStVolMin.Text.Trim());
                        lst.Add(txtInspecItemChVol.Text.Trim());
                        lst.Add(tbChVolMax.Text.Trim());
                        lst.Add(tbChVolMin.Text.Trim());
                        lst.Add(txtInspecItemChCur.Text.Trim());
                        lst.Add(tbChCurMax.Text.Trim());
                        lst.Add(tbChCurMin.Text.Trim());
                        lst.Add(txtProject.Text.Trim());

                        db.AddModel(lst);
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
                        List<string> lst = new List<string>();

                        lst.Add(txtUnitCode.Text.Trim());
                        lst.Add(txtMaterialCode.Text.Trim());
                        lst.Add(txtSupplierCode.Text.Trim());
                        lst.Add(txtCountryCode.Text.Trim());
                        lst.Add(txtProductionLine.Text.Trim());
                        lst.Add(txtInspecEquipNumber.Text.Trim());
                        lst.Add(txtNumberOfInspecItem.Text.Trim());
                        lst.Add(txtInspecItemStVol.Text.Trim());
                        lst.Add(tbStVolMax.Text.Trim());
                        lst.Add(tbStVolMin.Text.Trim());
                        lst.Add(txtInspecItemChVol.Text.Trim());
                        lst.Add(tbChVolMax.Text.Trim());
                        lst.Add(tbChVolMin.Text.Trim());
                        lst.Add(txtInspecItemChCur.Text.Trim());
                        lst.Add(tbChCurMax.Text.Trim());
                        lst.Add(tbChCurMin.Text.Trim());
                        lst.Add(txtProject.Text.Trim());

                        db.EditModel(DefaultValues.Instance().MaterialCode, lst);
                        DefaultValues.Instance().MaterialCode = txtMaterialCode.Text.Trim();
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
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Mode == Mode.Edit)
            {
                txtUnitCode.Text = DefaultValues.Instance().UnitCode;
                txtMaterialCode.Text = DefaultValues.Instance().MaterialCode;
                txtSupplierCode.Text = DefaultValues.Instance().SupplierCode;
                txtCountryCode.Text = DefaultValues.Instance().CountryCode;
                txtProductionLine.Text = DefaultValues.Instance().ProductionLine;
                txtInspecEquipNumber.Text = DefaultValues.Instance().InspecEquipNumber;
                txtNumberOfInspecItem.Text = DefaultValues.Instance().NumberOfInspecItem;
                txtInspecItemStVol.Text = DefaultValues.Instance().InspecItem1;
                txtInspecItemChVol.Text = DefaultValues.Instance().InspecItem2;
                txtInspecItemChCur.Text = DefaultValues.Instance().InspecItem3;
                tbStVolMin.Text = DefaultValues.Instance().StandbyVoltageMin.ToString();
                tbStVolMax.Text = DefaultValues.Instance().StandbyVoltageMax.ToString();
                tbChVolMin.Text = DefaultValues.Instance().ChargingVoltageMin.ToString();
                tbChVolMax.Text = DefaultValues.Instance().ChargingVoltageMax.ToString();
                tbChCurMin.Text = DefaultValues.Instance().ChargingCurrentMin.ToString();
                tbChCurMax.Text = DefaultValues.Instance().ChargingCurrentMax.ToString();
                txtProject.Text = DefaultValues.Instance().Project;
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
