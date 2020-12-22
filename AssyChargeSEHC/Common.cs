using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssyChargeSEHC
{
    public class Common : INotifyPropertyChanged
    {

        public static Common _instance;
        public static Common Instance()
        {
            if (_instance == null)
                _instance = new Common();
            return _instance;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _ProductID;
        public string ProductID
        {
            set
            {
                _ProductID = value;
            }
            get
            {
                return _ProductID;
            }
        }

        private int _countpass = 0;
        private int _countng = 0;
        private int _counttotal = 0;

        public int RoleID { get; set; }

        public int CountOK
        {
            get { return _countpass; }
            set
            {
                _countpass = value;
                OnPropertyChanged("CountOK");
            }
        }
        public int CountNG
        {
            get { return _countng; }
            set
            {
                _countng = value;
                OnPropertyChanged("CountNG");
            }
        }
        public int CountTotal
        {
            get { return _counttotal; }
            set
            {
                _counttotal = value;
                OnPropertyChanged("CountTotal");
            }
        }

        public string QRCodeString(string paraStandbyVol, string paraChargerVol, string paraChargerCur)
        {
            string _s = "";
            _s = _Strings + "/"
                + _standbyVol + "-" + paraStandbyVol + "-"  + DefaultValues.Instance().StandbyVoltageMax.ToString("000.0") + "-"  + DefaultValues.Instance().StandbyVoltageMin.ToString("000.0") + "/"
                + _chargerVol + "-" + paraChargerVol + "-" + DefaultValues.Instance().ChargingVoltageMax.ToString("000.0") + "-" + DefaultValues.Instance().ChargingVoltageMin.ToString("000.0") + "/"
                + _chargerCur + "-" + paraChargerCur + "-" + DefaultValues.Instance().ChargingCurrentMax.ToString("00.00") + "-" + DefaultValues.Instance().ChargingCurrentMin.ToString("00.00") + "/";
            return _s;
        }
        public string _QRCode { get; set; }
        public string _MaterialCode { get; set; }
        public string _Strings { get; set; }
        public string _Time { get; set; }
        public const string _standbyVol = "A042";
        public const string _chargerVol = "A027";
        public const string _chargerCur = "A026";
        public const string Spec_IRLeft = "IRL";
        public const string Spec_IRCenter = "IRC";
        public const string Spec_IRRight = "IRR";
    }
}
