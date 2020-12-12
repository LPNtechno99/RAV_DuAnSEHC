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
        public string StartTime { get; set; }

        private string _ModelName;
        public string ModelName
        {
            get { return _ModelName; }
            set
            {
                _ModelName = value;
                OnPropertyChanged("ModelName");
            }
        }

        private string _StVolMin;
        public string StandbyVoltageMin 
        {
            get { return _StVolMin; }
            set
            {
                _StVolMin = value;
                OnPropertyChanged("StandbyVoltageMin");
            }
        }
        private string _StVolMax;
        public string StandbyVoltageMax
        {
            get { return _StVolMax; }
            set
            {
                _StVolMax = value;
                OnPropertyChanged("StandbyVoltageMax");
            }
        }
        private string _ChVolMin;
        public string ChargingVoltageMin
        {
            get { return _ChVolMin; }
            set
            {
                _ChVolMin = value;
                OnPropertyChanged("ChargingVoltageMin");
            }
        }
        private string _ChVolMax;
        public string ChargingVoltageMax
        {
            get { return _ChVolMax; }
            set
            {
                _ChVolMax = value;
                OnPropertyChanged("ChargingVoltageMax");
            }
        }
        private string _ChCurMin;
        public string ChargingCurrentMin
        {
            get { return _ChCurMin; }
            set
            {
                _ChCurMin = value;
                OnPropertyChanged("ChargingCurrentMin");
            }
        }
        private string _ChCurMax;
        public string ChargingCurrentMax
        {
            get { return _ChCurMax; }
            set
            {
                _ChCurMax = value;
                OnPropertyChanged("ChargingCurrentMax");
            }
        }
        public string IRLeft { get; set; } = "L011X1";
        public string IRCenter { get; set; } = "L111XX";
        public string IRRight { get; set; } = "L0111X";
    }
}
