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
    public partial class Ad_CheckInfo : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        MOLog mMOLog = new MOLog();
        ChargeLog mChargeLog = new ChargeLog();

        string MSISDN = string.Empty;

        /// <summary>
        /// = 2: Lịch sử Đăng ký/Hủy/PushTin
        /// = 3: Lịch sử trừ tiền
        /// </summary>
        int HistoryType
        {
            get
            {
                if (ViewState["HistoryType"] == null)
                    return 1;
                else
                    return (int)ViewState["HistoryType"];
            }
            set
            {
                ViewState["HistoryType"] = value;
            }
        }

        string KeyName
        {
            get
            {
               if (HistoryType == 2)
                {
                    return "SortBy2";
                }
                else
                {
                    return "SortBy3";
                }
            }
        }

        string SortBy
        {
            get
            {
                if (ViewState[KeyName] == null)
                    return "";
                else
                    return ViewState[KeyName].ToString();
            }
            set
            {
                ViewState[KeyName] = value;
            }
        }

        private void BindData()
        {
             if (HistoryType == 2)
            {
                Admin_Paging2.ResetLoadData();
            }
            else
            {
                Admin_Paging3.ResetLoadData();
            }
        }

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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.CheckInfo, Member.MemberGroupID());
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

        private void ChangeLog(int HisType)
        {
            try
            {
                
                btn_MOlog.ForeColor = System.Drawing.Color.Black;
                btn_ChargeLog.ForeColor = System.Drawing.Color.Black;
                div_2.Visible = false;
                div_3.Visible = false;

                Admin_Paging2.Visible = false;
                Admin_Paging3.Visible = false;


                if (HistoryType == 2)
                {
                    btn_MOlog.ForeColor = System.Drawing.Color.Red;
                    div_2.Visible = true;
                    Admin_Paging2.Visible = true;
                }
                else
                {
                    btn_ChargeLog.ForeColor = System.Drawing.Color.Red;
                    div_3.Visible = true;
                    Admin_Paging3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
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
                    HistoryType = 2;
                    ChangeLog(HistoryType);                    

                    tbx_FromDate.Value = MyConfig.StartDayOfMonth.ToString(MyConfig.ShortDateFormat);
                    tbx_ToDate.Value = DateTime.Now.ToString(MyConfig.ShortDateFormat);
                }

                if (!string.IsNullOrEmpty(MSISDN))
                {
                    MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                    MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                    if (mTelco != MyConfig.Telco.Vinaphone)
                    {
                        MyMessage.ShowError("Số điện thoại không hợp lệ, xin vui lòng kiểm tra lại.");
                        return;
                    }
                }
              
                Admin_Paging2.rpt_Data = rpt_Data_2;
                Admin_Paging2.GetData_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetData_Callback(Admin_Paging2_GetData_Callback_Change);
                Admin_Paging2.GetTotalPage_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetTotalPage_Callback(Admin_Paging2_GetTotalPage_Callback_Change);

                Admin_Paging3.rpt_Data = rpt_Data_3;
                Admin_Paging3.GetData_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetData_Callback(Admin_Paging3_GetData_Callback_Change);
                Admin_Paging3.GetTotalPage_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetTotalPage_Callback(Admin_Paging3_GetTotalPage_Callback_Change);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        private void BindDataSub()
        {
            try
            {
                if (string.IsNullOrEmpty(MSISDN))
                    return;
              
                int PID = MyPID.GetPIDByPhoneNumber(MSISDN,MySetting.AdminSetting.MaxPID);

                Subscriber mSub = new Subscriber();
                DataTable mTable = mSub.Select(2, PID.ToString(), MSISDN);
                DataColumn mCol_1 = new DataColumn("ChannelTypeName", typeof(string));
                mTable.Columns.Add(mCol_1);
                foreach (DataRow mRow in mTable.Rows)
                {
                    mRow["ChannelTypeName"] = ((MyConfig.ChannelType)(int)mRow["ChannelTypeID"]).ToString();
                }
                rpt_Sub.DataSource = mTable;
                rpt_Sub.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        int Admin_Paging2_GetTotalPage_Callback_Change()
        {
            try
            {
                if (HistoryType != 2)
                    return 0;

                int SearchType = 0;              
                string SearchContent = MSISDN;

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
                PID = MyPID.GetPIDByPhoneNumber(SearchContent,MySetting.AdminSetting.MaxPID);

             
                return mMOLog.TotalRow(SearchType, SearchContent, PID, 0, 0, BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging2_GetData_Callback_Change()
        {
            try
            {
                if (HistoryType != 2)
                    return new DataTable();

                int SearchType = 0;             
                string SearchContent = MSISDN;

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
               
                PageIndex = (Admin_Paging2.mPaging.CurrentPageIndex - 1) * Admin_Paging2.mPaging.PageSize + 1;

                return mMOLog.Search(SearchType, Admin_Paging2.mPaging.BeginRow, Admin_Paging2.mPaging.EndRow, SearchContent, PID, 0, 0, BeginDate, EndDate, SortBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int Admin_Paging3_GetTotalPage_Callback_Change()
        {
            try
            {
                if (HistoryType != 3)
                    return 0;
                int SearchType = 0;

               string SearchContent = MSISDN;

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
               

                return mChargeLog.TotalRow(SearchType, SearchContent, PID, 0, 0, 0, BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging3_GetData_Callback_Change()
        {
            try
            {
                if (HistoryType != 3)
                    return new DataTable();

                int SearchType = 0;

               string SearchContent = MSISDN;

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

               
                PageIndex = (Admin_Paging3.mPaging.CurrentPageIndex - 1) * Admin_Paging3.mPaging.PageSize + 1;

                return mChargeLog.Search(SearchType, Admin_Paging3.mPaging.BeginRow, Admin_Paging3.mPaging.EndRow, SearchContent, PID, 0, 0, 0, BeginDate, EndDate, SortBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btn_MOlog_Click(object sender, EventArgs e)
        {
            HistoryType = 2;
            ChangeLog(HistoryType);
            BindData();
        }

        protected void btn_ChargeLog_Click(object sender, EventArgs e)
        {
            HistoryType = 3;
            ChangeLog(HistoryType);
            BindData();
        }

        protected void lbtn_Sort_Click(object sender, EventArgs e)
        {
            try
            {
                //lbtn_Sort_1.CssClass = "Sort";
                lbtn_Sort_2.CssClass = "Sort";
                lbtn_Sort_3.CssClass = "Sort";
                //lbtn_Sort_4.CssClass = "Sort";
                //lbtn_Sort_5.CssClass = "Sort";
                //lbtn_Sort_6.CssClass = "Sort";
                //lbtn_Sort_7.CssClass = "Sort";

                LinkButton mLinkButton = (LinkButton)sender;
                SortBy = mLinkButton.CommandArgument;

                if (mLinkButton.CommandArgument.IndexOf(" ASC") >= 0)
                {
                    mLinkButton.CssClass = "SortActive_Up";
                    mLinkButton.CommandArgument = mLinkButton.CommandArgument.Replace(" ASC", " DESC");
                }
                else
                {
                    mLinkButton.CssClass = "SortActive_Down";
                    mLinkButton.CommandArgument = mLinkButton.CommandArgument.Replace(" DESC", " ASC");
                }

                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SortError, "Chilinh");
            }
        }

        protected void tbx_Search_Click(object sender, EventArgs e)
        {
            try
            {
                BindDataSub();
                HistoryType = 2;
                ChangeLog(HistoryType);
                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SortError, "Chilinh");
            }
        }
       
    }
}