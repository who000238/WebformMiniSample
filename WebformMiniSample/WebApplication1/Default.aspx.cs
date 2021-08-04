using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Site1 mainMaster = this.Master as Site1; // 必須轉型 沒轉不能存取
            mainMaster.MyTitle = "預設頁";
            mainMaster.SetPageCaption("預設頁面");
            //this.ucCoverImage1.SetText("第一個 UC");
            //this.ucCoverImage1.SetText("第二個 UC");
            //this.ucCoverImage2.SetText("第三個 UC");
        }

        
    }
}