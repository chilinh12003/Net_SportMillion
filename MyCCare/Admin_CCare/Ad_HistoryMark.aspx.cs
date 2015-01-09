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

namespace MyCCare.Admin_CCare
{
    public partial class Ad_HistoryMark : MyASPXBase
    {
        public int PageIndex = 1;
        AnswerLog mAnswerLog = new AnswerLog();

        public int WeekMark
        {
            get
            {
                if(ViewState["WeekMark"]  == null)
                {
                    ViewState["WeekMark"] = 0;
                }

                return (int)ViewState["WeekMark"];
            }
            set
            {
                ViewState["WeekMark"] = value;
            }
        }
        public int ChargeMark
        {
            get
            {
                if (ViewState["ChargeMark"] == null)
                {
                    ViewState["ChargeMark"] = 0;
                }

                return (int)ViewState["ChargeMark"];
            }
            set
            {
                ViewState["ChargeMark"] = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MyCCare.MasterPages.Admin mMaster = (MyCCare.MasterPages.Admin)Page.Master;
                mMaster.Title = "GUI - Tra cứu điểm";

                if (!IsPostBack)
                {

                    ViewState["SortBy"] = string.Empty;
                    tbx_MSISDN.Value = MySetting.AdminSetting.MSISDN;

                    tbx_FromDate.Value = MySetting.AdminSetting.BeginDate;
                    tbx_ToDate.Value = MySetting.AdminSetting.EndDate;

                    GetSubInfo();
                }
                else
                {
                    MySetting.AdminSetting.BeginDate = tbx_FromDate.Value;
                    MySetting.AdminSetting.EndDate = tbx_ToDate.Value;
                }

                Admin_Paging1.rpt_Data = rpt_Data;
                Admin_Paging1.GetData_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetData_Callback(Admin_Paging1_GetData_Callback_Change);
                Admin_Paging1.GetTotalPage_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetTotalPage_Callback(Admin_Paging1_GetTotalPage_Callback_Change);
            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.LoadDataError, true, ex);
            }
        }

        int Admin_Paging1_GetTotalPage_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SortBy = ViewState["SortBy"].ToString();
                string SearchContent = tbx_MSISDN.Value;

                int PID = 0;

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                EndDate = EndDate.AddDays(1);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                return mAnswerLog.TotalRow(SearchType, SearchContent, PID, 0, BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging1_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SortBy = ViewState["SortBy"].ToString();
                string SearchContent = tbx_MSISDN.Value;

                int PID = 0;

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                EndDate = EndDate.AddDays(1);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                DataTable mTable = mAnswerLog.Search(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, SearchContent, PID, 0, BeginDate, EndDate, SortBy);
            
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GetSubInfo()
        {
            try
            {
                string MSISDN = tbx_MSISDN.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return;
                }
                tbx_MSISDN.Value = MSISDN;
                MySetting.AdminSetting.MSISDN = MSISDN;

                Subscriber mSub = new Subscriber();
                DataTable mTable_Sub = mSub.Select(2, MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID).ToString(), MSISDN);

                if (mTable_Sub != null && mTable_Sub.Rows.Count > 0)
                {
                    ChargeMark = (int)mTable_Sub.Rows[0]["ChargeMark"];
                    WeekMark = (int)mTable_Sub.Rows[0]["WeekMark"];
                }
                else
                {
                    ChargeMark = 0;
                    WeekMark = 0;
                }

            }
            catch(Exception ex)
            {
                mLog.Error(MyNotice.AdminError.SeachError, true, ex);
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

                GetSubInfo();

                Admin_Paging1.ResetLoadData();
            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.SeachError, true, ex);
            }
        }

    }
}