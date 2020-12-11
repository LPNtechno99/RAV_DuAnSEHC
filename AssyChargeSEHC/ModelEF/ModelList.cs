using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssyChargeSEHC.ModelEF
{
    public class ModelList
    {
        public int ID { get; set; }
        public string ModelName { get; set; }
        public string StandbyVoltageMin { get; set; }
        public string StandbyVoltageMax { get; set; }
        public string ChargingVoltageMin { get; set; }
        public string ChargingVoltageMax { get; set; }
        public string ChargingCurrentMin { get; set; }
        public string ChargingCurrentMax { get; set; }
    }
}
