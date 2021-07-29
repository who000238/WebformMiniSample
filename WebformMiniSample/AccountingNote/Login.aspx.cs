﻿using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.Session["UserLoginInfo"] != null)
            {
                this.plcLogin.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.plcLogin.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;
            
            //check empty
            if(string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.ltlMsg.Text = "帳號及密碼為必填";
                return;
            }

            var dr = UserInfoManger.GetUserInfoByAccount(inp_Account);
           
            //check null
            if (dr == null)
            {
                this.ltlMsg.Text = "此帳號不存在";
                return;
            }

            //check account / pwd
            if(string.Compare(dr["Account"].ToString(),inp_Account,true) == 0 && 
               string.Compare(dr["PWD"].ToString(), inp_PWD, false) == 0)
            {
                this.Session["UserLoginInfo"] = dr["Account"].ToString();
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.ltlMsg.Text = "登入失敗，請檢查帳號及密碼";
                return;
            }
        }
    }
}