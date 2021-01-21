using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssyChargeSEHC
{
    public class DefaultValues : INotifyPropertyChanged
    {
        public static DefaultValues _instance;
        public static DefaultValues Instance()
        {
            if (_instance == null)
                _instance = new DefaultValues();
            return _instance;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int ID { get; set; }
        private string _StartTime = "--:--:--";
        public string StartTime
        {
            get { return _StartTime; }
            set
            {
                _StartTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        private string _EndTime = "--:--:--";
        public string EndTime
        {
            get { return _EndTime; }
            set
            {
                _EndTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        public string UnitCode { get; set; }
        private string _MaterialCode;
        public string MaterialCode
        {
            get { return _MaterialCode; }
            set
            {
                _MaterialCode = value;
                OnPropertyChanged("ModelName");
            }
        }
        public string SupplierCode { get; set; }
        public string CountryCode { get; set; }
        public string ProductionLine { get; set; }
        public string InspecEquipNumber { get; set; }
        public string NumberOfInspecItem { get; set; }
        public string InspecItem1 { get; set; }
        private float _StVolMin;
        public float StandbyVoltageMin
        {
            get { return _StVolMin; }
            set
            {
                _StVolMin = value;
                OnPropertyChanged("StandbyVoltageMin");
            }
        }
        private float _StVolMax;
        public float StandbyVoltageMax
        {
            get { return _StVolMax; }
            set
            {
                _StVolMax = value;
                OnPropertyChanged("StandbyVoltageMax");
            }
        }
        public string InspecItem2 { get; set; }
        private float _ChVolMin;
        public float ChargingVoltageMin
        {
            get { return _ChVolMin; }
            set
            {
                _ChVolMin = value;
                OnPropertyChanged("ChargingVoltageMin");
            }
        }
        private float _ChVolMax;
        public float ChargingVoltageMax
        {
            get { return _ChVolMax; }
            set
            {
                _ChVolMax = value;
                OnPropertyChanged("ChargingVoltageMax");
            }
        }
        public string InspecItem3 { get; set; }
        private float _ChCurMin;
        public float ChargingCurrentMin
        {
            get { return _ChCurMin; }
            set
            {
                _ChCurMin = value;
                OnPropertyChanged("ChargingCurrentMin");
            }
        }
        private float _ChCurMax;
        public float ChargingCurrentMax
        {
            get { return _ChCurMax; }
            set
            {
                _ChCurMax = value;
                OnPropertyChanged("ChargingCurrentMax");
            }
        }
        public string Project { get; set; }
        public string IRLeft { get; set; } = "L0111X";
        public string IRCenter { get; set; } = "L111XX";
        public string IRRight { get; set; } = "L011X1";
    }
}
