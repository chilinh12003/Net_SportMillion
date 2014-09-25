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
using System.IO;
using System.Net;
namespace MyWap.vnp
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : MyWapBase
    {

        Keyword mKeyword = new Keyword("strConnection_SportMillion");

        public override void WriteHTML()
        {
            try
            {
               
                string service = Request.Url.GetLeftPart(UriPartial.Path);
                string MSISDN_Temp = MyContext.User.Identity.Name;

                MyConfig.Telco mTelco = new MyConfig.Telco();
               
                Write("<br/>MSISDN_Temp=" + MSISDN_Temp);
                if (MyCheck.CheckPhoneNumber(ref MSISDN_Temp, ref mTelco, "84"))
                {
                    if ((int)mTelco == (int)MyConfig.Telco.Vinaphone)
                    {
                        MSISDN = MSISDN_Temp;
                        return;
                    }
                }                        

                if (Session["countlogin"] == null)
                {
                    Session["countlogin"] = 1;
                }
                else
                {
                    Session["countlogin"] = (int)Session["countlogin"] + 1;
                }

                if ((int)Session["countlogin"] > 2)
                {
                    Response.Write("so lan lap lon hon 1 countlogin:" + Session["countlogin"].ToString());
                    return;
                }

                CasAuthentication.SingleSignOut();
                //CasAuthentication.RedirectToLoginPage();
                CasAuthentication.ProxyRedirect(MyConfig.Domain + "/login.ashx");
                //MyLoadNote mNote_1 = new MyLoadNote(ErrorDesc);
                //Write(mNote_1.GetHTML());

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
        }

        private string GetPassword(string clearPassResponse)
        {
            string retVal = "";          

            return retVal;
        } 
    }
}