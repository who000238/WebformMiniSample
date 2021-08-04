using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        public string MyTitle { get; set; } = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("Master_Page_Init <br/>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Master_Page_Load <br/>");

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("Master_Page_PreRender <br/>");

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("Master_Button1_Click <br/>");
            this.TextBox1.Text = this.TextBox1.Text;
        }

        public void SetPageCaption(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                this.ltlCaption.Text = title;
            }
        }
    }
}