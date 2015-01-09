using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyUtility; using MyBase.MyWeb;
namespace MyAdmin
{
    public partial class _Default : MyASPXBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Ad_Default.aspx");
        }

       
    }
}
