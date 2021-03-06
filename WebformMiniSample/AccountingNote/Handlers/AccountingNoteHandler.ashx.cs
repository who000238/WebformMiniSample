using AccountingNote.DBSource;
using AccountingNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AccountingNote.ORM.DBModels;
using AccountingNote.Extensions;

namespace AccountingNote.Handlers
{
    /// <summary>
    /// AccountingNoteHandler 的摘要描述
    /// </summary>
    public class AccountingNoteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context) //只是一個進入點 依照參數決定執行增加修改或刪除的功能
        {
            string actionName =
                context.Request.QueryString["ActionName"];    //字串 指令名 為 網址上的query指令
            if (string.IsNullOrEmpty(actionName))               //若沒有指令或為空 回報400代碼
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ActionName is required");
                context.Response.End();
            }

            if (actionName == "create")                                             //若指令為創造
            {
                string caption = context.Request.Form["Caption"];           //字串組 為表單送出的資料
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["actType"];
                string body = context.Request.Form["Body"];

                // ID of Admin
                string id = "B7E4FFFB-BE01-4B3D-B79A-608F9F1599FB";

                //必填檢查
                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(amountText) ||
                    string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption,amount,actType is required.");
                    context.Response.End();
                    return;
                }

                //轉型
                int tempAmount, tempActType;                                    //有可能user輸入的資料為非整數 這邊使用try.parse來測試轉型 若失敗則回傳msg和return程式
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "Amount,ActType should be integer.");
                    return;
                }
                try                                                      //使用try catch若成功則創建 若失敗則回報503代碼
                {
                    //建立流水帳
                    Accounting accounting = new Accounting()
                    {
                        UserID = id.ToGuid(),
                        Caption = caption,
                        Body = body,
                        Amount = tempAmount,
                        ActType = tempActType
                    };
                    AccountingManager.CreateAccounting(accounting);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("OK");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error");
                }

            }
            else if (actionName == "update")                                                    //修改
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["actType"];
                string body = context.Request.Form["Body"];

                // ID of Admin
                string userID = "B7E4FFFB-BE01-4B3D-B79A-608F9F1599FB";

                //必填檢查
                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(amountText) ||
                    string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption,amount,actType is required.");
                    context.Response.End();
                    return;
                }

                //轉型
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "Amount,ActType should be integer.");
                    return;
                }
                try
                {
                    //修改流水帳
                    AccountingManager.UpdateAccounting(id, userID, caption, tempAmount, tempActType, body);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("OK");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error");
                }
            }
            else if (actionName == "delete")                                                        //刪除
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["actType"];
                string body = context.Request.Form["Body"];

                // ID of Admin
                string userID = "B7E4FFFB-BE01-4B3D-B79A-608F9F1599FB";

                //必填檢查
                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(amountText) ||
                    string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption,amount,actType is required.");
                    context.Response.End();
                    return;
                }

                //轉型
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "Amount,ActType should be integer.");
                    return;
                }
                try
                {
                    //刪除流水帳
                    AccountingManager.DeleteAccounting(id);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("OK");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error");
                }
            }
            else if (actionName == "list")
            {
                Guid userGUID = new Guid("B7E4FFFB-BE01-4B3D-B79A-608F9F1599FB");

                List<Accounting> sourceList = AccountingManager.GetAccountingList(userGUID);
                List<AccountingNoteViewModel> list =
                        sourceList.Select(obj => new AccountingNoteViewModel()
                        {
                            ID = obj.ID,
                            Caption = obj.Caption,
                            Amount = obj.Amount,
                            ActType =
                                    (obj.ActType == 0) ? "支出" : "收入",
                            CreateDate = obj.CreateDate.ToString("yyyy-MM-dd")
                        }).ToList();


                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            else if (actionName == "query")
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);
                string userID = "B7E4FFFB-BE01-4B3D-B79A-608F9F1599FB";
                Guid userGUID = new Guid("B7E4FFFB-BE01-4B3D-B79A-608F9F1599FB");

                var accounting = AccountingManager.GetAccounting(id, userGUID);

                if (accounting == null)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No data" + idText);
                    context.Response.End();
                    return;
                }

                AccountingNoteViewModel model = new AccountingNoteViewModel()
                {
                    ID = accounting.ID,
                    Caption = accounting.Caption,
                    Body = accounting.Body,
                    Amount = accounting.Amount,
                    ActType = (accounting.ActType == 0) ? "支出" : "收入",
                    CreateDate = accounting.CreateDate.ToString("yyyy-MM-dd")
                };

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);

            }


        }

        private void ProcessError(HttpContext context, string msg)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
            context.Response.End();
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