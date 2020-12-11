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
        private int _countpass = 0;
        private int _countng = 0;
        private int _counttotal = 0;

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

        public string QRCodeString(string paraIRLeft, string paraIRCenter, string paraIRRight, string paraStandbyVol, string paraChargerVol, string paraChargerCur)
        {
            string _s = "";
            _s = _ModelCode + _Strings + "/" + _Time + "/"
                + _IRLeft + "-" + paraIRLeft + "/"
                + _IRCenter + "-" + paraIRCenter + "/"
                + _IRRight + "-" + paraIRRight + "/"
                + _standbyVol + "-" + paraStandbyVol + "-" + DefaultValues.Instance().StandbyVoltageMax + "-" + DefaultValues.Instance().StandbyVoltageMin + "/"
                + _chargerVol + "-" + paraChargerVol + "-" + DefaultValues.Instance().ChargingVoltageMax + "-" + DefaultValues.Instance().ChargingVoltageMin + "/"
                + _chargerCur + "-" + paraChargerCur + "-" + DefaultValues.Instance().ChargingCurrentMax + "-" + DefaultValues.Instance().ChargingCurrentMin + "/";
            return _s;
        }

        public string _ModelCode { get; set; }
        public string _Strings { get; set; }
        public string _Time { get; set; }
        public const string _standbyVol = "A042";
        public const string _chargerVol = "A027";
        public const string _chargerCur = "A026";
        public const string _IRLeft = "A001";
        public const string _IRCenter = "A002";
        public const string _IRRight = "A003";
    }
}
