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

        /// <summary>
        /// lấy thông số mặc định
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Thêm mới một dữ liệu mặc định
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="StVolMin"></param>
        /// <param name="StVolMax"></param>
        /// <param name="ChVolMin"></param>
        /// <param name="ChVolMax"></param>
        /// <param name="ChCurMin"></param>
        /// <param name="ChCurMax"></param>
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
        public void EditModel(string modelName, string[] arrModel)
        {
            var existing = db.ModelList.Where(x => x.ModelName == modelName).ToArray();
            existing[0].ModelName = arrModel[0];
            existing[0].StandbyVoltageMin = arrModel[1];
            existing[0].StandbyVoltageMax = arrModel[2];
            existing[0].ChargingVoltageMin = arrModel[3];
            existing[0].ChargingVoltageMax = arrModel[4];
            existing[0].ChargingCurrentMin = arrModel[5];
            existing[0].ChargingCurrentMax = arrModel[6];
            db.SaveChanges();
        }
    }
}
