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
    public class MyLoadConfirm : MyLoadBase
    {
        public string para = string.Empty;
        public MyLoadConfirm(string para)
        {
            mTemplatePath = "~/Templates/Static/Confirm.htm";
            this.para = para;
            Init();
        }
        protected override string BuildHTML()
        {
            try
            {
                return mLoadTempLate.LoadTemplateByString(mTemplatePath, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}