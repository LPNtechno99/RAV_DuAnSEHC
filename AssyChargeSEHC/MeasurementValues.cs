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

        private float _voltageStandby = (float)0.0;

        private float _voltage = (float)0.0;
        private float _current = (float)0.00;
        private string _IRLeft = "Null";
        private string _IRCenter = "Null";
        private string _IRRight = "Null";

        private Judge _judgeVoltageStandby = Judge.None;
        private Judge _judgeVoltage = Judge.None;
        private Judge _judgeCurrent = Judge.None;
        private Judge _judgeIRLeft = Judge.None;
        private Judge _judgeIRCenter = Judge.None;
        private Judge _judgeIRRight = Judge.None;
        private Judge _judgeFinal = Judge.None;

        public float VoltageStandby
        {
            get { return _voltageStandby; }
            set
            {
                _voltageStandby = value;
                OnPropertyChanged("VoltageStandby");
            }
        }
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
        public string IRLeft
        {
            get { return _IRLeft; }
            set
            {
                _IRLeft = value;
                OnPropertyChanged("IRLeft");
            }
        }
        public string IRCenter
        {
            get { return _IRCenter; }
            set
            {
                _IRCenter = value;
                OnPropertyChanged("IRCenter");
            }
        }
        public string IRRight
        {
            get { return _IRRight; }
            set
            {
                _IRRight = value;
                OnPropertyChanged("IRRight");
            }
        }

        public Judge JudgeVoltageStandby
        {
            get { return _judgeVoltageStandby; }
            set
            {
                _judgeVoltageStandby = value;
                OnPropertyChanged("JudgeVoltageStandby");
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
        public Judge JudgeIRLeft
        {
            get { return _judgeIRLeft; }
            set
            {
                _judgeIRLeft = value;
                OnPropertyChanged("JudgeIRLeft");
            }
        }
        public Judge JudgeIRCenter
        {
            get { return _judgeIRCenter; }
            set
            {
                _judgeIRCenter = value;
                OnPropertyChanged("JudgeIRCenter");
            }
        }
        public Judge JudgeIRRight
        {
            get { return _judgeIRRight; }
            set
            {
                _judgeIRRight = value;
                OnPropertyChanged("JudgeIRRight");
            }
        }
        public Judge JudgeFinal
        {
            get { return _judgeFinal; }
            set
            {
                _judgeFinal = value;
                OnPropertyChanged("JudgeFinal");
            }
        }
        public bool FinalJudgement()
        {
            if (_judgeVoltageStandby == Judge.OK && _judgeIRLeft == Judge.OK && _judgeIRCenter == Judge.OK && _judgeIRRight == Judge.OK && _judgeVoltage == Judge.OK && _judgeCurrent == Judge.OK)
                return true;
            else
                return false;
        }
    }
}
