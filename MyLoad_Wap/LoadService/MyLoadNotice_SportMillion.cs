using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;

namespace MyLoad_Wap.LoadService
{
    public class MyLoadNotice_SportMillion : MyLoadBase
    {
        public string Message = string.Empty;
        public MyLoadNotice_SportMillion(string Message)
        {
            mTemplatePath = "~/Templates/Static/Notice_SportMillion.htm";
            this.Message = Message;
            Init();
        }
        protected override string BuildHTML()
        {
            try
            {
                return mLoadTempLate.LoadTemplateByString(mTemplatePath, Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}