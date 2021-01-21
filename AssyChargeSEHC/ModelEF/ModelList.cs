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
        public string UnitCode { get; set; }
        public string MaterialCode { get; set; }
        public string SupplierCode { get; set; }
        public string CountryCode { get; set; }
        public string ProductionLine { get; set; }
        public string NumberOfInspecItem { get; set; }
        public string InspecEquipNumber { get; set; }
        public string InspecItem1 { get; set; }
        public string StandbyVoltageMax { get; set; }
        public string StandbyVoltageMin { get; set; }
        public string InspecItem2 { get; set; }
        public string ChargingVoltageMax { get; set; }
        public string ChargingVoltageMin { get; set; }
        public string InspecItem3 { get; set; }
        public string ChargingCurrentMax { get; set; }
        public string ChargingCurrentMin { get; set; }
        public string Project { get; set; }
    }
}
