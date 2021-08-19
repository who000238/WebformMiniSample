using AccountingNote.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class AccountingManager
    {

        /// <summary>
        /// 查詢流水帳清單
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetAccountingList(string userID)            //查詢清單
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                      ID,
                      Caption,
                      Amount,
                      ActType,
                      CreateDate
                    FROM Accounting
                    WHERE UserID = @userID;
                ";
            //ORDER BY CreateDate DESC 如果想要查詢出來的資料依照最新日期由上至下排列 上方SQL語法中加入這段

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));
            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 查詢流水帳清單 EF版本
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Accounting> GetAccountingList(Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID
                         select item);
                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 查詢流水帳 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT 
                      ID,
                      Caption,
                      Amount,
                      ActType,
                      CreateDate,
                      Body
                    FROM Accounting
                    WHERE id = @id AND UserID = @userID
                ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userID", userID));
            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary>
        /// 查詢流水帳 EF版本
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Accounting GetAccounting(int id, Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID && item.ID == id
                         select item);
                    var obj = query.FirstOrDefault(); //與查詢清單不同的是因為只需要一筆 且回傳資料是單筆的資料，並非list
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }

        }

        /// <summary>
        /// 建立流水帳
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static bool CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            //check input
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");
            }

            if (actType < 0 || actType > 1)
            {
                throw new ArgumentException("ActType must be 0 or 1 .");
            }

            //if (body == null)
            //    body = string.Empty; // 偷懶的寫法

            string bodyColumnSQL = "";
            string bodyValueSQL = "";
            if (!string.IsNullOrWhiteSpace(body))
            {
                bodyColumnSQL = ", Body";
                bodyValueSQL = ", @Body";
            }


            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                    INSERT INTO [dbo].[Accounting]
                    (
                               UserID
                               ,Caption
                               ,Amount
                               ,ActType
                               ,CreateDate
                                {bodyColumnSQL}
                    )
                    VALUES
                    (
                                @userID
                                ,@caption
                                ,@amount
                                ,@actType
                                ,@createDate
                                {bodyValueSQL} 
                    ) ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));

            if (!string.IsNullOrWhiteSpace(body))
                paramList.Add(new SqlParameter("@body", body));



            // connect db & execute
            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static void CreateAccounting(Accounting accounting)
        {
            // check input
            if (accounting.Amount < 0 || accounting.Amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");
            }

            if (accounting.ActType < 0 || accounting.ActType > 1)
            {
                throw new ArgumentException("ActType must be 0 or 1 .");
            }
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    accounting.CreateDate = DateTime.Now;
                    context.Accountings.Add(accounting);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }


        }

        /// <summary>
        /// 修改流水帳
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            //check input
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");
            }

            if (actType < 0 || actType > 1)
            {
                throw new ArgumentException("ActType must be 0 or 1 .");
            }


            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                    UPDATE [Accounting]
                    SET
                               UserID          =   @userID
                               ,Caption        =   @caption
                               ,Amount         =   @amount
                               ,ActType        =   @actType
                               ,CreateDate     =   @createDate
                               ,Body           =   @body 
                    WHERE
                        ID = @id
                     ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));
            paramList.Add(new SqlParameter("@id", ID));
            // connect db & execute

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        /// <summary>
        /// 修改流水帳 EF
        /// </summary>
        /// <param name="accounting"></param>
        /// <returns></returns>
        public static bool UpdateAccounting(Accounting accounting)
        {

            // check input
            if (accounting.Amount < 0 || accounting.Amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");
            }

            if (accounting.ActType < 0 || accounting.ActType > 1)
            {
                throw new ArgumentException("ActType must be 0 or 1 .");
            }

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject =
                          context.Accountings.Where(obj => obj.ID == accounting.ID).FirstOrDefault();
                    if (dbObject != null)
                    {
                        dbObject.Caption = accounting.Caption;
                        dbObject.Body = accounting.Body;
                        dbObject.Amount = accounting.Amount;
                        dbObject.ActType = accounting.ActType;

                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }

        }

        /// <summary>
        /// 刪除流水帳
        /// </summary>
        /// <param name="ID"></param>
        public static void DeleteAccounting(int ID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"
                    DELETE [Accounting]
                    WHERE
                        ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@id", ID));
            // connect db & execute
            try
            {
                DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        /// <summary>
        /// 刪除流水帳 EF版
        /// </summary>
        /// <param name="ID"></param>
        public static void DeleteAccounting_ORM(int ID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject =
                          context.Accountings.Where(obj => obj.ID == ID).FirstOrDefault();
                    if (dbObject != null)
                    {
                        context.Accountings.Remove(dbObject);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }



    }
}
