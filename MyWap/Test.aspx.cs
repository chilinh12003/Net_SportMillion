using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUtility;
namespace MyWap
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string Para = MySecurity.AES.Encrypt("84919967855|1e2swhmst3xbsi45do3zg345", MyConfig.GetKeyInConfigFile("GetMSISDN_Password"));
            //Response.Redirect(MyConfig.Domain + "/Reg/SportMillion.ashx?kid=1&para=" +HttpUtility.UrlEncode( Para), false);
            Response.Redirect(MyConfig.Domain + "/vnp/success.ashx", false);
        }
    }
}