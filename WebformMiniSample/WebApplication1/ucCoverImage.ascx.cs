using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ucCoverImage : System.Web.UI.UserControl
    {
        public string MyTitle { get; set; }

        public enum BColr
        {
            Blue,
            Red,
            Green
        }
        public BColr BackColor { get; set; } = BColr.Green;

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write($"{this.ID}_Page_Init <br/>");
        }
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write($"{this.ID}_Page_PreRender <br/>");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write($"{this.ID}_Page_Load <br/>");

            if (!string.IsNullOrWhiteSpace(this.MyTitle))
            {
                this.ltlTitle.Text = this.MyTitle;
                this.imgCover.Alt = this.MyTitle;
            }
            this.divMain.Style.Add("background-color", this.BackColor.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write($"{this.ID}_Button1_Click <br/>");

            this.ltlTitle.Text = "ucCoverImage_Click";
            this.imgCover.Alt = "ucCoverImage_Click";

        }

        public void SetText(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                this.ltlTitle.Text = title;
                this.imgCover.Alt = title;
            }
        }
    }
}