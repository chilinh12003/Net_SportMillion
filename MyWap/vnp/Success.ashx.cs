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

namespace MyWap.vnp
{
    /// <summary>
    /// Summary description for Success
    /// </summary>
    public class Success : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {

                MyLoadSuccess mLoadSuccess = new MyLoadSuccess(this.MSISDN);
                Write(mLoadSuccess.GetHTML());
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
          
        }
    }
}