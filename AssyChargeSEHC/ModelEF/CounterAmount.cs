using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssyChargeSEHC.ModelEF
{
    public class CounterAmount
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int OK { get; set; }
        public int NG { get; set; }
        public int Total { get; set; }
    }
}
