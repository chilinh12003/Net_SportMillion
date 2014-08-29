using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;
using MyLoad_Wap.LoadVNP;
using DotNetCasClient;
using DotNetCasClient.Utils;
using DotNetCasClient.Validation;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Security;
using System.Web.SessionState;

namespace MyWap.vnp
{
    /// <summary>
    /// Summary description for Logout
    /// </summary>
    public class Logout : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                MSISDN = string.Empty;

                CasAuthentication.SingleSignOut();
                MSISDN =MyContext.User.Identity.Name;
                string ToUrl = HttpUtility.UrlEncode(MyConfig.Domain + "/page/home.ashx");

                Response.Redirect("https://www.vinaphone.com.vn/auth/logout?service=" + ToUrl, false);
           
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
        }
    }
}