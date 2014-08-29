using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;


namespace MyLoad_Wap.LoadStatic
{
    public class MyLoadNote : MyLoadBase
    {
        public string Message = string.Empty;
        public MyLoadNote(string Message)
        {
            mTemplatePath = "~/Templates/Static/Note.htm";
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