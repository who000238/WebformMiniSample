using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ucAddControl : System.Web.UI.UserControl
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (this.Session["ControlList"] != null)
            //{
            //    Label lbl = new Label();
            //    lbl.ID = "lbl1";
            //    lbl.Text = "Text";
            //    TextBox txt = new TextBox();
            //    txt.ID = "txt1";
            //    txt.Text = "Text";

            //    Button btn = new Button();
            //    btn.ID = "Btn1";
            //    btn.Text = "Click";

            //    btn.Click += Btn_Click;

            //    this.Controls.Add(lbl);
            //    this.Controls.Add(txt);
            //    this.Controls.Add(btn);
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.Session["ControlList"] != null)
            {
                Label lbl = new Label();
                lbl.ID = "lbl1";
                lbl.Text = "Text";
                TextBox txt = new TextBox();
                txt.ID = "txt1";
                txt.Text = "Text";

                Button btn = new Button();
                btn.ID = "Btn1";
                btn.Text = "Click";

                btn.Click += Btn_Click;

                this.Controls.Add(lbl);
                this.Controls.Add(txt);
                this.Controls.Add(btn);

            }

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            //this.txt1.Text = "btn.Click";
            var txt = this.FindControl("txt1") as TextBox;
            Response.Write(txt.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Label lbl = new Label();
            //lbl.ID = "lbl1";
            //lbl.Text = "Text";
            //TextBox txt = new TextBox();
            //txt.ID = "txt1";
            //txt.Text = "Text";

            //Button btn = new Button();
            //btn.ID = "Btn1";
            //btn.Text = "Click";

            //btn.Click += Btn_Click;

            //this.Controls.Add(lbl);
            //this.Controls.Add(txt);
            //this.Controls.Add(btn);

            this.Session["ControlList"] = new string[] { "lbl1", "txt1", "Btn1" };
        }
    }
}