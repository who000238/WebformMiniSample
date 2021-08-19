﻿using AccountingNote.DBSource;
using AccountingNote.Extensions;
using AccountingNote.Model;
using AccountingNote.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingNote.Handlers
{
    /// <summary>
    /// AccountingNoteList 的摘要描述
    /// </summary>
    public class AccountingNoteList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)                     //泛型處理常式
        {
            string account = context.Request.QueryString["Account"];   //不使用this 使用context

            if (string.IsNullOrWhiteSpace(account))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var dr = UserInfoManger.GetUserInfoByAccount(account);

            if (dr == null)
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            string userID = dr["ID"].ToString();
            Guid userGUID = userID.ToGuid();
            List<Accounting> sourceList = AccountingManager.GetAccountingList(userGUID);

            //DataTable dataTable = AccountingManager.GetAccountingList(userID);

            //List<AccountingNoteViewModel> list = new List<AccountingNoteViewModel>();
            //foreach (DataRow drAccounting in dataTable.Rows)
            //{
            //    AccountingNoteViewModel model = new AccountingNoteViewModel()
            //    {
            //        ID = drAccounting["ID"].ToString(),
            //        Caption = drAccounting["Caption"].ToString(),
            //        Amount = drAccounting.Field<int>("Amount"),
            //        ActType = (drAccounting.Field<int>("ActType") == 0) ? "支出" : "收入",
            //        CreateDate = drAccounting.Field<DateTime>("CreateDate").ToString("yyyy-MM-dd")
            //    };

            //    list.Add(model); 
            //}
            List<AccountingNoteViewModel> list =
                    sourceList.Select(obj => new AccountingNoteViewModel()
                    {
                        ID = obj.ID.ToString(),
                        Caption = obj.Caption,
                        Amount = obj.Amount,
                        ActType =
                                (obj.ActType == 0) ? "支出" : "收入",
                        CreateDate = obj.CreateDate.ToString("yyyy-MM-dd")
                    }).ToList();

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}