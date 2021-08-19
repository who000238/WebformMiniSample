﻿using AccountingNote.ORM.DBModels;
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
    public class UserInfoManger
    {
       

        //public static DataRow GetUserInfoByAccount(string account)
        //{
        //    string connectionString = DBHelper.GetConnectionString();
        //    string dbCommandString =
        //        @"SELECT [ID], [Account],[PWD],[Name],[Email]
        //            FROM UserInfo
        //            WHERE [Account] = @account
        //        ";

        //    List<SqlParameter> list = new List<SqlParameter>();
        //    list.Add(new SqlParameter("@account", account));

        //    try
        //    {
        //        return DBHelper.ReadDataRow(connectionString, dbCommandString, list);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(ex);
        //        return null;
        //    }

        //}

        public static UserInfo GetUserInfoByAccount(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var obj = query.FirstOrDefault(); 
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }

        }
    }
}
