using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;

namespace MyWap.Page
{
    /// <summary>
    /// Summary description for GetMSISDN
    /// </summary>
    public class GetMSISDN : MyWapBase
    {
        public override void WriteHTML()
        {
            string GetMSISDN_Password = string.Empty;
            string GetMSISDN_ToURL = string.Empty;
            string GetMSISDN_URL = string.Empty;
            string Para = string.Empty;
            string Para_Encrypt = string.Empty;
            try
            {
              string GetMSISDN_UserName = MyConfig.GetKeyInConfigFile("GetMSISDN_UserName");

                GetMSISDN_Password = MyConfig.GetKeyInConfigFile("GetMSISDN_Password");
                GetMSISDN_ToURL = MyConfig.Domain + @"/page/checkcode.ashx";

                GetMSISDN_URL = MyConfig.GetKeyInConfigFile("GetMSISDN_URL") + "?para=";

                Para = GetMSISDN_UserName + "|" + GetMSISDN_Password + "|" + GetMSISDN_ToURL;
                Para_Encrypt = MySecurity.AES.Encrypt(Para, "HBDeCRYpT");

            }
            catch (Exception ex)
            {
                mLog.Error(ex);
            }
            finally
            {
                mLog.Debug("CHECK_CODE_GET_MSISDN", "GetMSISDN_ToURL:" + GetMSISDN_ToURL);
            }
            Response.Redirect(GetMSISDN_URL + HttpUtility.UrlEncode(Para_Encrypt), false);
        }
    }
}