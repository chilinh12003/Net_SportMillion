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

namespace MyCCare.Admin_CCare
{
    public partial class Ad_RegDereg : System.Web.UI.Page
    {
        public int PageIndex = 1;
        Subscriber mSub = new Subscriber();
        UnSubscriber mUnSub = new UnSubscriber();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                MyCCare.MasterPages.Admin mMaster = (MyCCare.MasterPages.Admin)Page.Master;
                mMaster.Title = "GUI - Cài đặt dịch vụ";

                if (!IsPostBack)
                {
                    ViewState["SortBy"] = string.Empty;
                    tbx_MSISDN.Value = MySetting.AdminSetting.MSISDN;
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {

                string MSISDN = tbx_MSISDN.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại");
                    return;
                }
                tbx_MSISDN.Value = MSISDN;
                MySetting.AdminSetting.MSISDN = MSISDN;
                int PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID);
                DataTable mTable_Sub = mSub.Select(2, PID.ToString(), MSISDN);
                rpt_Data_Sub.DataSource = mTable_Sub;
                rpt_Data_Sub.DataBind();

                if (mTable_Sub.Rows.Count < 1)
                {
                    DataTable mTable_UnSub = mUnSub.Select(2, PID.ToString(), MSISDN);
                    if (mTable_UnSub == null || mTable_UnSub.Rows.Count < 1)
                    {
                        DataRow mRow = mTable_UnSub.NewRow();
                        mRow["MSISDN"] = MSISDN;
                        mRow["PID"] = PID;
                        mTable_UnSub.Rows.Add(mRow);
                    }
                    rpt_Data_UnSub.DataSource = mTable_UnSub;
                    rpt_Data_UnSub.DataBind();
                }
                else
                {
                    rpt_Data_UnSub.DataSource = new DataTable();
                    rpt_Data_UnSub.DataBind();
                }
                ViewState["MSISDN"] = MSISDN;
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
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
                MSISDN = ViewState["MSISDN"] == null ? string.Empty : ViewState["MSISDN"].ToString();
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone");
                    return;
                }

                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                Signature = MSISDN + "|CMS|" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "|TRIEUPHUTT";
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;
                //Result = mClient.Dereg((int)MyConfig.ChannelType.CSKH, Signature, CommandCode);
                Result = mClient.DeReg_VNP(Login1.GetUserName(), MyCurrent.GetRequestIP, Signature, WS_SportMillion.ChannelType.CSKH);
                string[] Arr_Result = Result.Split('|');
                
                ErrorCode = Arr_Result[0];
                ErrorDesc = Arr_Result[1];

                if (ErrorCode.Equals("1"))
                {
                    MyMessage.ShowError("Thông tin Hủy đăng ký dịch vụ (" + ServiceName + ") của số điện thoại (" + MSISDN + ") đã được gửi đến hệ thống chờ xử lý.");
                }
                else
                {
                    MyMessage.ShowError("Xin lỗi, đăng ký/hủy đăng ký không thành công, xin vui lòng thử lại sau");
                }

                System.Threading.Thread.Sleep(3000);
                
                btn_Search_Click(null, null);
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
                MSISDN = ViewState["MSISDN"] == null ? string.Empty : ViewState["MSISDN"].ToString();
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone");
                    return;
                }

                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                Signature = MSISDN + "|CMS|" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "|TRIEUPHUTT";
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;
                
                Result = mClient.Reg_VNP(Login1.GetUserName(), MyCurrent.GetRequestIP, Signature, WS_SportMillion.ChannelType.CSKH);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = Arr_Result[1];

                if (ErrorCode.Equals("1"))
                {
                    MyMessage.ShowError("Thông tin Đăng ký dịch vụ (" + ServiceName + ") của số điện thoại (" + MSISDN + ") đã được gửi đến hệ thống chờ xử lý.");
                }
                else
                {
                    MyMessage.ShowError("Xin lỗi, Đăng ký không thành công, xin vui lòng thử lại sau");

                }
                System.Threading.Thread.Sleep(3000);
                btn_Search_Click(null, null);
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

        protected void rpt_Data_UnSub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
             if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
             {
                 var lbtn_Reg = e.Item.FindControl("lbtn_Reg");
                 if(lbtn_Reg != null)
                 {
                     ((LinkButton)lbtn_Reg).Enabled = Login1.IsAdmin();
                     if (!Login1.IsAdmin())
                         ((LinkButton)lbtn_Reg).OnClientClick = string.Empty;
                 }

             }
        }

        protected void rpt_Data_Sub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbtn_Dereg = e.Item.FindControl("lbtn_Dereg");
                if (lbtn_Dereg != null)
                {
                    ((LinkButton)lbtn_Dereg).Enabled = Login1.IsAdmin();
                    if (!Login1.IsAdmin())
                        ((LinkButton)lbtn_Dereg).OnClientClick = string.Empty;
                }
                var lbtn_Reset = e.Item.FindControl("lbtn_Reset");
                if (lbtn_Reset != null)
                {
                    ((LinkButton)lbtn_Reset).Enabled = Login1.IsAdmin();
                    if (!Login1.IsAdmin())
                        ((LinkButton)lbtn_Reset).OnClientClick = string.Empty;
                }
            }
        }

    }
}