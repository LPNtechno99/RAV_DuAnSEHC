using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AssyChargeSEHC
{
    public class MeasurementValues : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static MeasurementValues _instance;
        public static MeasurementValues Instance()
        {
            if (_instance == null)
                _instance = new MeasurementValues();
            return _instance;
        }
        public enum Judge { None, OK, NG }

        private float _voltage = (float)0.0;
        private float _current = (float)0.0;
        private string _IRLeft;
        private string _IRCenter;
        private string _IRRight;
        private Judge _judgeVoltage = Judge.None;
        private Judge _judgeCurrent = Judge.None;

        public float Voltage
        {
            get { return _voltage; }
            set
            {
                _voltage = value;
                OnPropertyChanged("Voltage");
            }
        }
        public float Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged("Current");
            }
        }

        public Judge JudgeVoltage
        {
            get { return _judgeVoltage; }
            set
            {
                _judgeVoltage = value;
                OnPropertyChanged("JudgeVoltage");
            }
        }
        public Judge JudgeCurrent
        {
            get { return _judgeCurrent; }
            set
            {
                _judgeCurrent = value;
                OnPropertyChanged("JudgeCurrent");
            }
        }
    }
}
