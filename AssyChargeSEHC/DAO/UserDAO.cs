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
            return db.ModelList.Select(x => x.Model).ToList();
        }
        /// <summary>
        /// get Result List
        /// </summary>
        /// <returns></returns>
        public List<ResultList> GetResultList()
        {
            return db.ResultList.ToList();
        }

    }
}
