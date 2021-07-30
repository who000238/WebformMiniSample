using AccountingNote.Auth;
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
                if (!AuthManager.IsLogined())                //如果尚未登入，導至登入頁
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                var currentUser = AuthManager.GetCurrentUser();


                if (currentUser == null)                                         //如果帳號不存在，導至登入頁
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = currentUser.Account;
                this.ltname.Text = currentUser.Name;
                this.ltEmail.Text = currentUser.Email;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManager.Logout();
            Response.Redirect("/Login.aspx");
        }
    }
}