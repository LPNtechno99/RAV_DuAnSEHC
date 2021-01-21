using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            return db.ModelList.Select(x => x.MaterialCode).ToList();
        }

        /// <summary>
        /// lấy thông số mặc định
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public List<ModelList> GetDefaultValues(string modelName)
        {
            return db.ModelList.Where(x => x.MaterialCode == modelName).ToList();
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
        public void AddModel(List<string> lstData)
        {
            try
            {
                var md = new ModelList()
                {
                    UnitCode = lstData[0],
                    MaterialCode = lstData[1],
                    SupplierCode = lstData[2],
                    CountryCode = lstData[3],
                    ProductionLine = lstData[4],
                    InspecEquipNumber = lstData[5],
                    NumberOfInspecItem = lstData[6],
                    InspecItem1 = lstData[7],
                    StandbyVoltageMax = lstData[8],
                    StandbyVoltageMin = lstData[9],
                    InspecItem2 = lstData[10],
                    ChargingVoltageMax = lstData[11],
                    ChargingVoltageMin = lstData[12],
                    InspecItem3 = lstData[13],
                    ChargingCurrentMax = lstData[14],
                    ChargingCurrentMin = lstData[15],
                    Project = lstData[16]
                };
                db.Add(md);
                db.SaveChanges();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// Chinh sua Model
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="arrModel"></param>
        public void EditModel(string modelName, List<string> lstModel)
        {
            try
            {
                var existing = db.ModelList.Where(x => x.MaterialCode == modelName).ToArray();

                existing[0].UnitCode = lstModel[0];
                existing[0].MaterialCode = lstModel[1];
                existing[0].SupplierCode = lstModel[2];
                existing[0].CountryCode = lstModel[3];
                existing[0].ProductionLine = lstModel[4];
                existing[0].InspecEquipNumber = lstModel[5];
                existing[0].NumberOfInspecItem = lstModel[6];
                existing[0].InspecItem1 = lstModel[7];
                existing[0].StandbyVoltageMax = lstModel[8];
                existing[0].StandbyVoltageMin = lstModel[9];
                existing[0].InspecItem2 = lstModel[10];
                existing[0].ChargingVoltageMax = lstModel[11];
                existing[0].ChargingVoltageMin = lstModel[12];
                existing[0].InspecItem3 = lstModel[13];
                existing[0].ChargingCurrentMax = lstModel[14];
                existing[0].ChargingCurrentMin = lstModel[15];
                existing[0].Project = lstModel[16];

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Lay RoleID de xac dinh quyen dang nhap
        /// </summary>
        /// <param name="role"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public int GetRoleID(string role, string pass)
        {
            try
            {
                var existing = db.Account.Where(x => x.Role == role && x.Password == pass).ToArray();
                if (existing.Length >= 1)
                    return existing[0].RoleID;
                else
                    return 0;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); return 0; }
        }
        /// <summary>
        /// Lay role name hien tai
        /// </summary>
        /// <returns></returns>
        public List<string> GetRole()
        {
            return db.Account.Select(x => x.Role).ToList();
        }
        /// <summary>
        /// edit password
        /// </summary>
        /// <param name="role"></param>
        /// <param name="passNew"></param>
        public void EditPassword(string role, string passNew)
        {
            try
            {
                var existing = db.Account.Where(x => x.Role == role).ToArray();
                existing[0].Password = passNew;
                db.SaveChanges();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// them bo dem so luong
        /// </summary>
        /// <param name="date"></param>
        /// <param name="OK"></param>
        /// <param name="NG"></param>
        /// <param name="Total"></param>
        public void AddCounterAmount(string date, int OK, int NG, int Total)
        {
            try
            {
                var ca = new CounterAmount()
                {
                    Date = date,
                    OK = OK,
                    NG = NG,
                    Total = Total
                };
                db.Add(ca);
                db.SaveChanges();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// sua bo dem so luong
        /// </summary>
        /// <param name="date"></param>
        /// <param name="OK"></param>
        /// <param name="NG"></param>
        /// <param name="Total"></param>
        public void EditCounterAmount(string date, int OK, int NG, int Total)
        {
            var existing = db.CounterAmount.Where(x => x.Date == date).ToArray();
            existing[0].OK = OK;
            existing[0].NG = NG;
            existing[0].Total = Total;
            db.SaveChanges();
        }
        /// <summary>
        /// Kiem tra su ton tai cua 1 dong trong table
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool CheckExist(string date)
        {
            var existing = db.CounterAmount.Where(x => x.Date == date).ToArray();
            if (existing.Length < 1)
                return false;
            else
                return true;
        }
        public int[] GetCounterAmount(string date)
        {
            var existing = db.CounterAmount.Where(x => x.Date == date).ToArray();
            int[] arr = new int[3];
            arr[0] = existing[0].OK;
            arr[1] = existing[0].NG;
            arr[2] = existing[0].Total;
            return arr;
        }
        /// <summary>
        /// Ghi nhật kí hệ thống
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="date"></param>
        /// <param name="action"></param>
        public void AddNewAction(string timeStamp, string date, string action)
        {
            try
            {
                var log = new SystemLogs()
                {
                    TimeStamp = timeStamp,
                    Date = date,
                    Action = action
                };
                db.Add(log);
                db.SaveChanges();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public List<string> GetSystemLogs()
        {
            List<string> lst = new List<string>();
            List<SystemLogs> lstLog = db.SystemLogs.Select(x => x).ToList();
            foreach (SystemLogs item in lstLog)
            {
                lst.Add(item.TimeStamp + "-" + item.Date + ": " + item.Action);
            }
            return lst;
        }
    }
}
