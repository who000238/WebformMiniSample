﻿using AccountingNote.Auth;
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

            string account = this.Session["UserLoginInfo"] as string;
            var dr = UserInfoManger.GetUserInfoByAccount(account);

            if (dr == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            //read accounting data
            var dt = AccountingManager.GetAccountingList(dr["ID"].ToString());
            if(dt.Rows.Count > 0)
            {
            this.gvAccountingList.DataSource = dt;
            this.gvAccountingList.DataBind();
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if(row.RowType == DataControlRowType.DataRow)
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

                if(dr.Row.Field<int>("Amount") > 1500)
                {
                    lbl.ForeColor = Color.Red;
                }
            }
        }
    }
}