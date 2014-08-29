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
    public class MyLoadRegistered : MyLoadBase
    {
        public string MSISDN = string.Empty;
        public MyLoadRegistered(string MSISDN)
        {

            if (GetCurrentDeviceType == MyConfig.DeviceType.Tablet)
            {
                mTemplatePath = "~/Templates/LandingPage_2/Registered.htm";
            }
            else
            {
                mTemplatePath = "~/Templates/LandingPage/Registered.htm";
            }

            this.MSISDN = MSISDN;
            Init();
        }
        protected override string BuildHTML()
        {
            try
            {
                return mLoadTempLate.LoadTemplateByString(mTemplatePath, MSISDN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}