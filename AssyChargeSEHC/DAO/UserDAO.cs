using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssyChargeSEHC.ModelEF;

namespace AssyChargeSEHC.DAO
{
    public class UserDAO : IDisposable
    {
        DbSEHCContext db = null;
        public UserDAO()
        {
            db = new DbSEHCContext();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// get Model List
        /// </summary>
        /// <returns></returns>
        public List<string> GetModelList()
        {
            return db.ModelList.Select(x => x.ModelName).ToList();
        }
        public List<ModelList> GetDefaultValues(string modelName)
        {
            return db.ModelList.Where(x => x.ModelName == modelName).ToList();
        }
        /// <summary>
        /// get Result List
        /// </summary>
        /// <returns></returns>
        public List<ResultList> GetResultList()
        {
            return db.ResultList.ToList();
        }
        public void AddModel(string modelName, string StVolMin, string StVolMax, string ChVolMin, string ChVolMax, string ChCurMin, string ChCurMax)
        {
            var md = new ModelList()
            {
                ModelName = modelName,
                StandbyVoltageMin = StVolMin,
                StandbyVoltageMax = StVolMax,
                ChargingVoltageMin = ChVolMin,
                ChargingVoltageMax = ChVolMax,
                ChargingCurrentMin = ChCurMin,
                ChargingCurrentMax = ChCurMax
            };
            db.Add(md);
            db.SaveChanges();
        }
    }
}
