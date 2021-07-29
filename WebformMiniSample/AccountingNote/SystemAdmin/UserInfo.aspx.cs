using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)                                            // 可能是按鈕跳回本頁，所以判斷postback
            {
                if (this.Session["UserLoginInfo"] == null)              //如果尚未登入，導至登入頁
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }
                string account = this.Session["UserLoginInfo"] as string;
                DataRow dr = UserInfoManger.GetUserInfoByAccount(account);
                    
                if (dr == null)                                         //如果帳號不存在，導至登入頁
                {
                    this.Session["UserLoginInfo"] = null;        //避免無限迴圈 手動session清除
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = dr["Account"].ToString();
                this.ltname.Text = dr["Name"].ToString();
                this.ltEmail.Text = dr["Email"].ToString();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this.Session["UserLoginInfo"] = null;                       //清除帳號資訊，導至登入頁
            Response.Redirect("/Login.aspx");
        }
    }
}