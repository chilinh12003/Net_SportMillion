using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;


namespace MyWap.Page
{
    /// <summary>
    /// Summary description for faq
    /// </summary>
    public class faq : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                MyLoadHeader mHeader = new MyLoadHeader();
                Write(mHeader.GetHTML());

                MyLoadFAQ mFAQ = new MyLoadFAQ();
                Write(mFAQ.GetHTML());

                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }

        }
    }
}
