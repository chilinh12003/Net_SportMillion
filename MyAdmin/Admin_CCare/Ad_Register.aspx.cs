using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyUtility;
using MySportMillion;
using MySportMillion.Service;
using MySportMillion.Sub;
using System.Net;
using System.IO;
using MySportMillion.Gateway;

namespace MyAdmin.Admin_CCare
{
    public partial class Ad_Register : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;

        Subscriber mSub = new Subscriber();
        sms_receive_queue mQuere = new sms_receive_queue(MySetting.AdminSetting.MySQLConnection_Gateway);

        private bool CheckPermission()
        {
            try
            {
                if (mGetRole.ViewRole == false)
                {
                    Response.Redirect(mGetRole.URLNotView, false);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.CheckPermissionError, "Chilinh");
                return false;
            }
            return true;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            bool IsRedirect = false;
            try
            {
                //Phân quyền
                if (ViewState["Role"] == null)
                {
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.Register, Member.MemberGroupID());
                }
                else
                {
                    mGetRole = (GetRole)ViewState["Role"];
                }

                if (!CheckPermission())
                {
                    IsRedirect = true;
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
            if (IsRedirect)
            {
                Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MyAdmin.MasterPages.Admin mMaster = (MyAdmin.MasterPages.Admin)Page.Master;
                mMaster.str_PageTitle = mGetRole.PageName;

                if (!IsPostBack)
                {
                    ViewState["SortBy"] = string.Empty;
                }

                 
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        protected void tbx_Dereg_Click(object sender, EventArgs e)
        {
            string MSISDN = string.Empty;
            string CommandCode = "HUYFULL";
            string ServiceName = "Triệu phú thể thao";
            string Result = string.Empty;
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string Signature = string.Empty;
            try
            {
                Button mButton = (Button)sender;

                MSISDN = ViewState["MSISDN"] == null ? string.Empty : ViewState["MSISDN"].ToString();
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone");
                    return;
                }     
        
                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                Signature = MSISDN + "|CMS|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;
                Result = mClient.Dereg((int)MyConfig.ChannelType.CSKH, Signature, CommandCode);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = Arr_Result[1];

                if(ErrorCode.Equals("1"))
                {
                    MyMessage.ShowError("Thông tin Hủy đăng ký dịch vụ (" + ServiceName + ") của số điện thoại (" + MSISDN + ") đã được gửi đến hệ thống chờ xử lý.");
                }
                else
                {
                    MyMessage.ShowError("Xin lỗi, đăng ký/hủy đăng ký không thành công, xin vui lòng thử lại sau");
                }

                System.Threading.Thread.Sleep(3000);
                btn_Execute_Click(null, null);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SaveDataError, "Chilinh");
            }
            finally
            {
                MyLogfile.WriteLogData("Register", "CommandCode:" + CommandCode + "|MSISDN:" + MSISDN + "|Signature:" + Signature + "|Result:" + Result);
            }
        }

        protected void tbx_Reg_Click(object sender, EventArgs e)
        {
            string MSISDN = string.Empty;
            string CommandCode = "DK";
            string ServiceName = "Triệu phú thể thao";
            string Result = string.Empty;
             string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string Signature = string.Empty;
            try
            {
                Button mButton = (Button)sender;

                MSISDN = ViewState["MSISDN"] == null ? string.Empty : ViewState["MSISDN"].ToString();
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone");
                    return;
                }
               
                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                Signature = MSISDN + "|CMS|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;
                Result = mClient.Reg((int)MyConfig.ChannelType.CSKH, Signature, CommandCode);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = Arr_Result[1];

                if(ErrorCode.Equals("1"))
                {
                    MyMessage.ShowError("Thông tin Đăng ký dịch vụ (" + ServiceName + ") của số điện thoại (" + MSISDN + ") đã được gửi đến hệ thống chờ xử lý.");
                }
                else
                {
                    MyMessage.ShowError("Xin lỗi, Đăng ký không thành công, xin vui lòng thử lại sau");
                
                }
                System.Threading.Thread.Sleep(3000);
                btn_Execute_Click(null, null);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SaveDataError, "Chilinh");
            }
            finally
            {
                MyLogfile.WriteLogData("Register", "CommandCode:" + CommandCode + "|MSISDN:" + MSISDN + "|Signature:" + Signature + "|Result:" + Result);
            }
        }

        protected void btn_Execute_Click(object sender, EventArgs e)
        {
            string MSISDN = string.Empty;          
            int PID = 0;
            try
            {

                MSISDN = tbx_MSISDN.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone");
                    return;
                }

                PID = MyPID.GetPIDByPhoneNumber(MSISDN,MySetting.AdminSetting.MaxPID);

                DataTable mTable_Sub = mSub.Select(2,PID.ToString(),MSISDN);

                DataTable mTable_NotSub = mTable_Sub.Clone();
                mTable_NotSub.Clear();

                if (mTable_Sub.Rows.Count < 1)
                {
                    DataRow mRow = mTable_NotSub.NewRow();
                    mRow["MSISDN"] = MSISDN;
                    mTable_NotSub.Rows.Add(mRow);
                }

                rpt_Data_Reg.DataSource = mTable_Sub;
                rpt_Data_Reg.DataBind();

                rpt_Data_NotReg.DataSource = mTable_NotSub;
                rpt_Data_NotReg.DataBind();

                ViewState["MSISDN"] = MSISDN;
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
            finally
            {
                //MyLogfile.WriteLogData("Register", "CommandCode:" + CommandCode + "|MSISDN:" + MSISDN + "|RequestID:" + RequestID + "|Result:" + Result);
            }
        }
    }
}