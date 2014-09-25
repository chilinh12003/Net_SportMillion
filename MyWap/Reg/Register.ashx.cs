using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;

namespace MyWap.Reg
{
    /// <summary>
    /// Summary description for Register
    /// </summary>
    public class Register : MyWapBase
    {
        public override void WriteHTML()
        {
            int KeywordID = 0;
            string GetMSISDN_Password = string.Empty;
            string GetMSISDN_ToURL = string.Empty;
            string GetMSISDN_URL = string.Empty;
            string Para = string.Empty;
            string Para_Encrypt = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    int.TryParse(Request.QueryString["kid"].TrimEnd().TrimStart(), out KeywordID);
                }

                string GetMSISDN_UserName = MyConfig.GetKeyInConfigFile("GetMSISDN_UserName");

                GetMSISDN_Password = MyConfig.GetKeyInConfigFile("GetMSISDN_Password");
                GetMSISDN_ToURL = MyConfig.Domain + @"/Reg/SportMillion.ashx?kid=" + KeywordID.ToString();

                GetMSISDN_URL = MyConfig.GetKeyInConfigFile("GetMSISDN_URL") + "?para=";

                Para = GetMSISDN_UserName + "|" + GetMSISDN_Password + "|" + GetMSISDN_ToURL;
                Para_Encrypt = MySecurity.AES.Encrypt(Para, "HBDeCRYpT");

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
            finally
            {
                MyLogfile.WriteLogData("REGIETER_GET_MSISDN", "GetMSISDN_ToURL:" + GetMSISDN_ToURL + "|KeywordID:" + KeywordID.ToString());
            }
            Response.Redirect(GetMSISDN_URL + HttpUtility.UrlEncode(Para_Encrypt), false);
        }
    }
}