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

namespace MyWap.Reg
{
    /// <summary>
    /// Summary description for Deny
    /// </summary>
    public class Deny : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                MyLoadHeader mHeader = new MyLoadHeader();
                Write(mHeader.GetHTML());

                MyLoadNote mNote = new MyLoadNote("Bạn đã không đồng ý đăng ký dịch vụ Triệu Phút Thể Thao, chân thành cảm ơn.");
                Write(mNote.GetHTML());

            }
            catch (Exception ex)
            {
                
                mLog.Error(ex);
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());

            }

        }
    }
}