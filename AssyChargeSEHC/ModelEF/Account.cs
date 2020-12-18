using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssyChargeSEHC.ModelEF
{
    public class Account
    {
        public int ID { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}
