using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySportMillion.Service;
using MyUtility;
using System.Data;
namespace MyWeb.Page
{
    public partial class Login : System.Web.UI.Page
    {
        SubOTP mSubOTP = new SubOTP();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    Session["MSISDN"] = string.Empty;
                }

                
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error",ex, true, MyNotice.EndUserError.LoadDataError, "Chilinh");
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty( tbx_MSISDN.Value))
                {
                    MyMessage.ShowError("Xin vui lòng nhập số điện thoại.");
                    return;
                }
                string MSISDN = tbx_MSISDN.Value.Trim();
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại");
                    return;
                }

                if (string.IsNullOrEmpty(tbx_Password.Value))
                {
                    MyMessage.ShowError("Xin vui lòng nhập mật khẩu.");
                    return;
                }
                  int PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.WebSetting.MaxPID);
                  DataTable mTable = mSubOTP.Select(2,PID.ToString(), MSISDN, tbx_Password.Value);

                  if (mTable != null && mTable.Rows.Count > 0)
                  {
                      Session["MSISDN"] = mTable.Rows[0]["MSISDN"].ToString();
                      //MySetting.WebSetting.SetMSISDN(mTable.Rows[0]["MSISDN"].ToString());
                      Response.Redirect(MyConfig.Domain + "/Page/ListCode.aspx", false);
                      return;
                  }
                  else
                  {
                      //MySetting.WebSetting.SetMSISDN(string.Empty);
                      Session["MSISDN"] = string.Empty;
                      MyMessage.ShowError("Số điện thoại hoặc Mật khẩu không chính xác, xin vui lòng kiểm tra lại.");
                      return;
                  }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, true, MyNotice.EndUserError.LoadDataError, "Chilinh");
            }
        }
    }
}