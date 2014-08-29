using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;


namespace MyLoad_Wap.LoadVNP
{
    public class MyLoadConfirm_Money : MyLoadBase
    {
        public string MSISDN = string.Empty;
        public string Keyword = string.Empty;
        public int PartnerID = 0;
        public MyLoadConfirm_Money(string MSISDN, string Keyword, int PartnerID)
        {
            if (GetCurrentDeviceType == MyConfig.DeviceType.Tablet)
            {
                mTemplatePath = "~/Templates/LandingPage_2/Confirm_Money.htm";
            }
            else
            {
                mTemplatePath = "~/Templates/LandingPage/Confirm_Money.htm";
            }
            
            this.MSISDN = MSISDN;
            this.Keyword = Keyword;
            this.PartnerID = PartnerID;
            Init();
        }
        protected override string BuildHTML()
        {
            try
            {
                string ConfirmPara = MSISDN + "|" + PartnerID.ToString() + "|" + Keyword;
                string ConfirmPara_Encode = MySecurity.AES.Encrypt(ConfirmPara, MyConfig.GetKeyInConfigFile("GetMSISDN_Password"));

                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, new string[] { MSISDN, HttpUtility.UrlEncode(ConfirmPara_Encode) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}