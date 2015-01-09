using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUtility; using MyBase.MyWeb;
using MySportMillion;
namespace MyAdmin.Admin
{
    public partial class Ad_Default : MyASPXBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Member.IsLogined())
            {
                Response.Redirect(MyConfig.URLLogin);
            }
        }
    }
}
