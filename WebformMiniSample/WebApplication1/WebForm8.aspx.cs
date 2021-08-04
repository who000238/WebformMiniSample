using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ctl = this.ucDomSample.FindControl("PlaceHolder10");

            //this.ucDomSample.Controls[0].Controls[2].Controls[0].Visible = false; 
            if (ctl != null)
            {
                ctl.Visible = false;
            }


        }
    }
}