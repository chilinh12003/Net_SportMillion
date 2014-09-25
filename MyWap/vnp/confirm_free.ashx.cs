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
    /// Summary description for confirm_free
    /// </summary>
    public class confirm_free : MyWapBase
    {
        public override void WriteHTML()
        {
            string Keyword = string.Empty;
            int PartnerID = 0;
            try
            {
                MyLoadConfirm_Free mLoadFree = new MyLoadConfirm_Free(MSISDN, Keyword, PartnerID);
                Write(mLoadFree.GetHTML());
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
           
        }
    }
}