using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check is logined
            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }
         

            var currentUser = AuthManager.GetCurrentUser();


            if (currentUser == null)                                         //如果帳號不存在，導至登入頁
            {
                this.Session["UserLoginInfo"] = null;        //避免無限迴圈 手動session清除
                Response.Redirect("/Login.aspx");
                return;
            }


            //read accounting data
            var dt = AccountingManager.GetAccountingList(currentUser.ID);
            if (dt.Rows.Count > 0) //check is empty data
            {
                var dtPaged = this.GetPagedDataTable(dt);

                this.gvAccountingList.DataSource = dtPaged;
                this.gvAccountingList.DataBind();

                this.ucPager.TotalSize = dt.Rows.Count;
                this.ucPager.Bind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
            }
        }

        

        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }

        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();                     //複製 DataTable dt 的結構給 dtPaged
            int startIndex = (this.GetCurrentPage() - 1) * 10;  //設定分頁控制項分別頁面的起點、終點
            int endIndex = (this.GetCurrentPage()) * 10;
            if (endIndex > dt.Rows.Count)                       //讓最後一頁的終點可以跟資料筆數相同、否則會有超過資料筆數無法讀取的問題 out of index 的錯誤訊息
                endIndex = dt.Rows.Count;
            for ( var i = startIndex; i < endIndex;i++)         // 設定一個for迴圈 起點為第一筆資料 終點為最後一筆資料
            {
                DataRow dr = dt.Rows[i];                        //dt.Row的第i筆資料放到 dr資料列裡面
                var drNew = dtPaged.NewRow();                   //新資料列drNew等同於dtPaged的資料列

                foreach (DataColumn dc in dt.Columns)           //
                {                                               
                    drNew[dc.ColumnName] = dr[dc];              //
                }                                               
                                                                
                dtPaged.Rows.Add(drNew);                        //
            }

            return dtPaged;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                //var dr = e.Row.DataItem as DataRow;
                //int actType = (int)dr["ActType"];

                //Literal ltl = row.FindControl("ltActType") as Literal;
                Label lbl = row.FindControl("lblActType") as Label;

                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                if (actType == 0)
                {
                    //ltl.Text = "支出";
                    lbl.Text = "支出";
                }
                else
                {
                    //ltl.Text = "收入";
                    lbl.Text = "收入";
                }

                if (dr.Row.Field<int>("Amount") > 1500)
                {
                    lbl.ForeColor = Color.Red;
                }
            }
        }
    }
}