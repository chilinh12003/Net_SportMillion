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

namespace MyAdmin.Admin_CCare
{
    public class SubInfo
    {
        public string MSISDN = "NULL";
        public string FirstDate = "NULL";
        public string EffectiveDate = "NULL";
        public string ExpiryDate = "NULL";
        public string RetryChargeDate = "NULL";
        public string RetryChargeCount = "NULL";
        public string ChargeDate = "NULL";
        public string RenewChargeDate = "NULL";
        public string ChannelTypeName = "NULL";
        public string StatusName = "NULL";
        public string PID = "NULL";
        public string MOByDay = "NULL";
        public string MarkByDay = "NULL";
        public string TotalMark = "NULL";
        public string CodeByDay = "NULL";
        public string TotalCode = "NULL";
        public string MatchID = "NULL";
        public string AnswerKQ = "NULL";
        public string AnswerBT = "NULL";
        public string AnswerGB = "NULL";
        public string AnswerTS = "NULL";
        public string AnswerTV = "NULL";
        public string LastUpdate = "NULL";
        public string IsNotify = "NULL";
        public string CofirmDeregDate = "NULL";
        public string NotifyDate = "NULL";
        public string AppID = "NULL";
        public string AppName = "NULL";
        public string UserName = "NULL";
        public string IP = "NULL";
        public string PartnerID = "NULL";
        public string DeregDate = "NULL";

        public SubInfo()
        {

        }
        public SubInfo(DataTable mTable)
        {
            if (mTable == null || mTable.Rows.Count < 1)
                return;
            DataRow mRow = mTable.Rows[0];
            MSISDN = mRow["MSISDN"].ToString();
           
            FirstDate = mRow["FirstDate"] != DBNull.Value ?((DateTime)mRow["FirstDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            EffectiveDate =  mRow["EffectiveDate"] != DBNull.Value ?((DateTime)mRow["EffectiveDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            ExpiryDate =  mRow["ExpiryDate"] != DBNull.Value ?((DateTime)mRow["ExpiryDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            RetryChargeDate =  mRow["RetryChargeDate"] != DBNull.Value ? ((DateTime)mRow["RetryChargeDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            RetryChargeCount = mRow["RetryChargeCount"] != DBNull.Value ?((int)mRow["RetryChargeCount"]).ToString(MyConfig.IntFormat) : "NULL";
            ChargeDate = mRow["ChargeDate"] != DBNull.Value ?((DateTime)mRow["ChargeDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            RenewChargeDate = mRow["RenewChargeDate"] != DBNull.Value ?((DateTime)mRow["RenewChargeDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            
            ChannelTypeName = mRow["ChannelTypeID"] != null ?((MyConfig.ChannelType)(int)mRow["ChannelTypeID"]).ToString() : "NULL";
            StatusName = mRow["StatusID"] != DBNull.Value ?MyEnum.StringValueOf((Subscriber.Status)(int)mRow["StatusID"]) : "NULL";

            MOByDay = mRow["MOByDay"] != DBNull.Value ?((int)mRow["MOByDay"]).ToString(MyConfig.IntFormat) : "NULL";
            MarkByDay = mRow["MarkByDay"] != DBNull.Value ?((int)mRow["MarkByDay"]).ToString(MyConfig.IntFormat) : "NULL";
            TotalMark = mRow["TotalMark"] != DBNull.Value ?((int)mRow["TotalMark"]).ToString(MyConfig.IntFormat) : "NULL";
            CodeByDay = mRow["CodeByDay"] != DBNull.Value ?((int)mRow["CodeByDay"]).ToString(MyConfig.IntFormat) : "NULL";
            TotalCode = mRow["TotalCode"] != DBNull.Value ?((int)mRow["TotalCode"]).ToString(MyConfig.IntFormat) : "NULL";
           
            AnswerKQ = mRow["AnswerKQ"] != DBNull.Value ?mRow["AnswerKQ"].ToString() : "NULL";
            AnswerBT =  mRow["AnswerBT"] != DBNull.Value ?mRow["AnswerBT"].ToString() : "NULL";
            AnswerGB =  mRow["AnswerGB"] != DBNull.Value ?mRow["AnswerGB"].ToString() : "NULL";
            AnswerTS =  mRow["AnswerTS"] != DBNull.Value ?mRow["AnswerTS"].ToString() : "NULL";
            AnswerTV =  mRow["AnswerTV"] != DBNull.Value ?mRow["AnswerTV"].ToString() : "NULL";

            LastUpdate = mRow["LastUpdate"] != DBNull.Value ?((DateTime)mRow["LastUpdate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            CofirmDeregDate = mRow["CofirmDeregDate"] != DBNull.Value ?((DateTime)mRow["CofirmDeregDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            NotifyDate =  mRow["NotifyDate"] != DBNull.Value ?((DateTime)mRow["NotifyDate"]).ToString(MyConfig.LongDateFormat) : "NULL";

            AppName = mRow["AppName"] != DBNull.Value ?mRow["AppName"].ToString() : "NULL";
            UserName = mRow["UserName"] != DBNull.Value ? mRow["UserName"].ToString() : "NULL";
            IP = mRow["IP"] != DBNull.Value ?mRow["IP"].ToString() : "NULL";

            if (mTable.Columns.Contains("DeregDate"))
            {
                DeregDate = mRow["DeregDate"] != DBNull.Value ?((DateTime)mRow["DeregDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
                StatusName = MyEnum.StringValueOf(Subscriber.Status.Deactive);
            }
        }
    }
    /// <summary>
    /// Lấy tất cả thông tin của thuê bao đang sử dụng dịch vụ
    /// </summary>
    public partial class Ad_CheckSubInfo : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        MOLog mMOLog = new MOLog();
        ChargeLog mChargeLog = new ChargeLog();

        string MSISDN = string.Empty;

       public SubInfo mSubInfo = new SubInfo();
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.CheckDetailInfo, Member.MemberGroupID());
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

                MSISDN = tbx_MSISDN.Value;

                if (!IsPostBack)
                {
                  
                }
             
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        protected void tbx_Search_Click(object sender, EventArgs e)
        {
            try
            {
                MSISDN = tbx_MSISDN.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (string.IsNullOrEmpty(MSISDN) || !MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại.");
                    return;
                }                

                int PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID);

                Subscriber mSub = new Subscriber();
                UnSubscriber mUnSub = new UnSubscriber();
                DataTable mTable = mSub.Select(2, PID.ToString(), MSISDN);

                if(mTable.Rows.Count < 1)
                    mTable = mUnSub.Select(2, PID.ToString(), MSISDN);

                mSubInfo = new SubInfo(mTable);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }
    }
}