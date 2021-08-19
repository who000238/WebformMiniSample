﻿using AccountingNote.Auth;
using AccountingNote.DBSource;
using AccountingNote.Extensions;
using AccountingNote.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check is logined
            if (!AuthManager.IsLogined())   //檢查是否登入，否則跳轉到登入頁
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string account = this.Session["UserLoginInfo"] as string; //登入資訊放入帳號字串
            var drUserInfo = UserInfoManger.GetUserInfoByAccount(account); //用帳號去取出資料(單筆)
            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null)                                         //如果帳號不存在，導至登入頁
            {
                this.Session["UserLoginInfo"] = null;        //避免無限迴圈 手動session清除
                Response.Redirect("/Login.aspx");
                return;
            }

            if (!this.IsPostBack)
            {
                //check is create mode or edit mode
                if (this.Request.QueryString["ID"] == null)
                {
                    this.btnDelete.Visible = false;
                }
                else
                {
                    this.btnDelete.Visible = true;
                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        //var drAccounting = AccountingManager.GetAccounting(id,drUserInfo["ID"].ToString());
                        var accounting = AccountingManager.GetAccounting(id, currentUser.ID);
                        if (accounting == null)
                        {
                            this.ltMsg.Text = "Data doesn't exist";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            this.ddlActType.SelectedValue = accounting.ActType.ToString();
                            this.txtAmount.Text = accounting.Amount.ToString();
                            this.txtCaption.Text = accounting.Caption;
                            this.txtDesc.Text = accounting.Body;
                        }
                    }
                    else
                    {
                        this.ltMsg.Text = "ID is required";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }


            string account = this.Session["UserLoginInfo"] as string;
            var dr = UserInfoManger.GetUserInfoByAccount(account);

            if (dr == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string actTypeText = this.ddlActType.SelectedValue;
            string amountText = this.txtAmount.Text;

            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);


            string idText = this.Request.QueryString["ID"];
            Accounting accounting = new Accounting()
            {
                UserID = currentUser.ID,
            ActType = actType,
                Amount = amount,
                Body = this.txtDesc.Text,
                Caption = this.txtCaption.Text
            };
            if (string.IsNullOrWhiteSpace(idText))
            {
                //excute 'insert into db'
                AccountingManager.CreateAccounting(accounting);
            }
            else
            {
                int id;
                if (int.TryParse(idText, out id))
                {
                    //Excute 'update db'
                    accounting.ID = id;
                    AccountingManager.UpdateAccounting(accounting);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            //Type
            if (this.ddlActType.SelectedValue != "0" && this.ddlActType.SelectedValue != "1")
            {
                msgList.Add("Tpye must be 0 or 1.");
            }
            //Amount
            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
            {
                msgList.Add("Amount must be a number.");
            }
            else
            {
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount must be a number.");
                }

                if (tempInt < 0 || tempInt > 1000000)
                {
                    msgList.Add("Amount must between 0 and 1,000,000 .");
                }
            }

            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];

            if (string.IsNullOrWhiteSpace(idText))

                return;
            int id;
            if (int.TryParse(idText, out id))
            {
                //Excute 'deldete db'
                AccountingManager.DeleteAccounting_ORM(id);
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}