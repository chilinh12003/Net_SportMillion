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
    public class MyLoadSuccess : MyLoadBase
    {
        public string MSISDN = string.Empty;
        public MyLoadSuccess(string MSISDN)
        {
            if (GetCurrentDeviceType == MyConfig.DeviceType.Tablet)
            {
                mTemplatePath = "~/Templates/LandingPage_2/Success.htm";
            }
            else
            {
                mTemplatePath = "~/Templates/LandingPage/Success.htm";
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