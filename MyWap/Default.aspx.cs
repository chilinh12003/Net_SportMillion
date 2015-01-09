using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUtility;
using System.IO;
using System.Net;
using System.Web.Security;
using System.Xml;

namespace MyWap
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(MyConfig.Domain + "/page/home.ashx", false);
        }
    }
}