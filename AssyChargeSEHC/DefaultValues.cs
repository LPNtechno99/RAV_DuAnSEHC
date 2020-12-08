using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssyChargeSEHC
{
    public class DefaultValues
    {
        public static DefaultValues _instance;
        public static DefaultValues Instance()
        {
            if (_instance == null)
                _instance = new DefaultValues();
            return _instance;
        }
        public int ID { get; set; }
        public string StartTime { get; set; }
        public string StandbyVoltageMin { get; set; } = "7.0";
        public string StandbyVoltageMax { get; set; } = "9.0";
        public string ChargingVoltageMin { get; set; } = "24.0";
        public string ChargingVoltageMax { get; set; } = "25.2";
        public string ChargingCurrentMin { get; set; } = "1.45";
        public string ChargingCurrentMax { get; set; } = "1.55";
        public string IRLeft { get; set; } = "L011X1";
        public string IRCenter { get; set; } = "L111XX";
        public string IRRight { get; set; } = "L0111X";
    }
}
