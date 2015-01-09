using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyUtility; using MyBase.MyWeb;
using MySportMillion;
using MySportMillion.Service;
using MySportMillion.Sub;
using System.Net;
using System.IO;
using MySportMillion.Gateway;
namespace MyAdmin.Admin_CCare
{
    public partial class Ad_ResendMT : MyASPXBase
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        

        Subscriber mSub = new Subscriber();
        ems_send_queue mQueue = new ems_send_queue(MySetting.AdminSetting.MySQLConnection_Gateway);

        private void BindCombo(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                      
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                mLog.Error(MyNotice.AdminError.CheckPermissionError, true, ex);
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.ResendMT, Member.MemberGroupID());
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
                mLog.Error(MyNotice.AdminError.LoadDataError, true, ex);
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
                    BindCombo(1);


                }
            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.LoadDataError, true, ex);
            }
        }

      
        protected void btn_SendMT_Click(object sender, EventArgs e)
        {
            try
            {
                string MSISDN = tbx_MSISDN.Value;
                string MTContent = tbx_MT.Value.TrimEnd().TrimStart();
                string RegKeyword = string.Empty;
                

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại");
                    return;
                }
                tbx_MSISDN.Value = MSISDN;

              

                if (string.IsNullOrEmpty(MTContent))
                {
                    MyMessage.ShowError("Xin hãy nhập  nội dung MT cần gửi.");
                    return;
                }

                int PID = MyPID.GetPIDByPhoneNumber(MSISDN,MySetting.AdminSetting.MaxPID);

                DataTable mTable = mSub.Select(2, PID.ToString(), MSISDN);

                if (mTelco == null || mTable.Rows.Count < 1)
                {
                    MyMessage.ShowError("Số điện thoại chưa đăng ký dịch vụ này, nên không thể gửi tin nhắn.");
                    return;
                }
               
                if (SendMT(RegKeyword, MSISDN, MTContent))
                {
                    UpdateMOLog( MSISDN,DefineMT.MTType.Default, string.Empty, MTContent);
                    MyMessage.ShowMessage("Gửi MT thành công.");
                }
                else
                {
                    MyMessage.ShowMessage("Gửi MT KHÔNG thành công.");
                }
            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.SeachError, true, ex);
            }
        }

        private void UpdateMOLog(string MSISDN, DefineMT.MTType mMTType, string LogContent, string MT)
        {
            try
            {

                MOLog mMOLog = new MOLog();

                DataSet mSet = mMOLog.CreateDataSet();
                DataRow mRow = mSet.Tables[0].NewRow();
                mRow["MSISDN"] = MSISDN;
                mRow["LogDate"] = DateTime.Now;
                mRow["ChannelTypeID"] = (int)MyConfig.ChannelType.CSKH;
                mRow["ChannelTypeName"] = MyConfig.ChannelType.CSKH.ToString();
                 mRow["MTTypeID"] = (int)mMTType;
                mRow["MTTypeName"] = mMTType.ToString();
                mRow["LogContent"] = LogContent;
                mRow["MT"] = MT;
                mRow["PID"] = MyPID.GetPIDByPhoneNumber(MSISDN,MySetting.AdminSetting.MaxPID);
            
                mSet.Tables[0].Rows.Add(mRow);
                MyConvert.ConvertDateColumnToStringColumn(ref mSet);

                mMOLog.Insert(0, mSet.GetXml());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SendMT(string COMMAND_CODE, string USER_ID, string MTContent)
        {
            string SERVICE_ID = MySetting.AdminSetting.ShoreCode;
            string REQUEST_ID = MySecurity.CreateCode(9);
            bool Result = false;
            try
            {
                Result = mQueue.Insert(USER_ID, SERVICE_ID, COMMAND_CODE, MTContent, REQUEST_ID);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mLog.Debug("_Resend_MT", "UserID:" + Member.MemberID().ToString() + "|USER_ID:" + USER_ID + "|COMMAND_CODE:" + COMMAND_CODE + "|REQUEST_ID:" + REQUEST_ID + "|INFO:" + MTContent + "|Result:" + Result.ToString());
            }
        }

    }
}