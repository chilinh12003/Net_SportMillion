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

using DotNetCasClient;
using DotNetCasClient.Utils;
using DotNetCasClient.Validation;

namespace MyWap
{
    public partial class VNPLogin : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    Response.Redirect(MyConfig.Domain + "/page/home.ashx", false);
        //}

        // Local specific CAS host       
        private const string CASHOST = "https://vinaphone.com.vn/auth/";
        // After the page has been loaded, this routine is called.
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MyLogfile.WriteLogData("_Step", "Step 1");
                // Look for the "ticket=" after the "?" in the URL
                string tkt = Request["ticket"];
                if (string.IsNullOrEmpty(tkt))
                    tkt = Request.QueryString["ticket"];

                MyLogfile.WriteLogData("_Step", "Step 2");
                // This page is the CAS service=, but discard any query string residue
                //string service = Request.Url.GetLeftPart(UriPartial.Path);
                string service = MyConfig.Domain + "/VNPLogin.aspx";
                // First time through there is no ticket=, so redirect to CAS login


                if (Session["countlogin"] == null)
                {
                    Session["countlogin"] = 1;
                    MyLogfile.WriteLogData("_Step", "Step 3");
                }
                else
                {
                    MyLogfile.WriteLogData("_Step", "Step 4");
                    Session["countlogin"] = (int)Session["countlogin"] + 1;
                }


                if ((int)Session["countlogin"] > 3)
                {
                    MyLogfile.WriteLogData("_Step", "Step 5");
                    Response.Write("so lan lap lon hon 1 countlogin:" + Session["countlogin"].ToString());
                    return;
                }

                MyLogfile.WriteLogData("_Step", "Step 6");
                if (tkt == null || tkt.Length == 0)
                {
                    MyLogfile.WriteLogData("_Step", "Step 7");
                    string redir = CASHOST + "login?" +
                      "service=" + service;
                    Response.Redirect(redir);
                    return;
                }

                MyLogfile.WriteLogData("_Step", "Step 8");
                MyUtility.MyLogfile.WriteLogData("SSO", "ticket:" + tkt);

                // Second time (back from CAS) there is a ticket= to validate
                string validateurl = CASHOST + "serviceValidate?" +
                  "ticket=" + tkt + "&" +
                  "service=" + service;
                StreamReader Reader = new StreamReader(new WebClient().OpenRead(validateurl));
                string resp = Reader.ReadToEnd();
                Label2.Text = resp;

                MyLogfile.WriteLogData("_Step", "Step 9");
                MyUtility.MyLogfile.WriteLogData("SSO", "resp:" + resp);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }
    }
}