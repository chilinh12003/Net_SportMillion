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
    public partial class Ad_SubInfo : System.Web.UI.Page
    {

        MOLog mMOLog = new MOLog();
        ChargeLog mChargeLog = new ChargeLog();
        public int PageIndex_1 = 1;
        public int PageIndex_2 = 1;
        public int PageIndex_3 = 1;

        public string MSISDN = string.Empty;
        public string StatusName = string.Empty;
        public string ServiceName = "";
        public string EffectiveDate = string.Empty;
        public string ExpiryDate = string.Empty;

        DateTime BeginDate_StartServcie = new DateTime(2013, 08, 01);
        protected void Page_Load(object sender, EventArgs e)
        {
            MSISDN = Request.QueryString["msisdn"];

            BindDataSub();

            Admin_Paging_VNP1.rpt_Data = rpt_Reg;
            Admin_Paging_VNP1.GetData_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetData_Callback(Admin_Paging_VNP1_GetTotalPage_Callback_Change);
            Admin_Paging_VNP1.GetTotalPage_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetTotalPage_Callback(Admin_Paging_VNP1_GetData_Callback_Change);

            Admin_Paging_VNP2.rpt_Data = rpt_Renew;
            Admin_Paging_VNP2.GetData_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetData_Callback(Admin_Paging_VNP2_GetTotalPage_Callback_Change);
            Admin_Paging_VNP2.GetTotalPage_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetTotalPage_Callback(Admin_Paging_VNP2_GetData_Callback_Change);

            Admin_Paging_VNP3.rpt_Data = rpt_MOLog;
            Admin_Paging_VNP3.GetData_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetData_Callback(Admin_Paging_VNP3_GetTotalPage_Callback_Change);
            Admin_Paging_VNP3.GetTotalPage_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetTotalPage_Callback(Admin_Paging_VNP3_GetData_Callback_Change);
        }

        private void BindDataSub()
        {
            try
            {
                if (string.IsNullOrEmpty(MSISDN))
                    return;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return;
                }
                int PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID);

                Subscriber mSub = new Subscriber();
                DataTable mTable = mSub.Select(2, PID.ToString(), MSISDN);


                if (mTable == null || mTable.Rows.Count < 1)
                {
                    ServiceName = " TRIEUPHU_DAILY";
                    StatusName = "Không hoạt động";
                    return;
                }
                ServiceName = " TRIEUPHU_DAILY";
                DataRow mRow = mTable.Rows[0];
                EffectiveDate = ((DateTime)mRow["EffectiveDate"]).ToString(MyConfig.LongDateFormat);
                ExpiryDate = ((DateTime)mRow["ExpiryDate"]).ToString(MyConfig.LongDateFormat);
                StatusName = MyEnum.StringValueOf(((Subscriber.Status)(int)mRow["StatusID"]));
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        int Admin_Paging_VNP1_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;

                string SearchContent = MSISDN;

                int PID = 0;

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);


                return mChargeLog.TotalRow_SelectType(SearchType, SearchContent, PID, 0, 0, 0, BeginDate, EndDate,1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging_VNP1_GetTotalPage_Callback_Change()
        {
            try
            {
                string SortBy = "ChargeLogID DESC";
                int SearchType = 0;

                string SearchContent = MSISDN;

                int PID = 0;

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                
                PageIndex_1 = (Admin_Paging_VNP1.mPaging.CurrentPageIndex - 1) * Admin_Paging_VNP1.mPaging.PageSize + 1;

                DataTable mTable=  mChargeLog.Search_SelectType(SearchType, Admin_Paging_VNP1.mPaging.BeginRow, Admin_Paging_VNP1.mPaging.EndRow, SearchContent, PID, 0, 0, 0, BeginDate, EndDate,1, SortBy);
                DataColumn mCol_1 = new DataColumn("ActionName", typeof(string));
                mTable.Columns.Add(mCol_1);

                foreach (DataRow mRow in mTable.Rows)
                {
                    if ((int)mRow["ChargeTypeID"] == (int)ChargeLog.ChargeType.REG_DAILY)
                    {
                        mRow["ActionName"] = "Đăng ký dịch vụ";
                    }
                    else
                    {
                        mRow["ActionName"] = "Hủy dịch vụ";
                    }
                    if ((int)mRow["ChargeStatusID"] == 0)
                    {
                        mRow["ChargeStatusName"] = "Thành công";
                    }
                    else
                    {
                        mRow["ChargeStatusName"] = "Không thành công";
                    }
                }               
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        int Admin_Paging_VNP2_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;

                string SearchContent = MSISDN;

                int PID = 0;

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);


                return mChargeLog.TotalRow_SelectType(SearchType, SearchContent, PID, 0, 0, 0, BeginDate, EndDate, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging_VNP2_GetTotalPage_Callback_Change()
        {
            try
            {
                string SortBy = "ChargeLogID DESC";
                int SearchType = 0;

                string SearchContent = MSISDN;

                int PID = 0;

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);


                PageIndex_2 = (Admin_Paging_VNP2.mPaging.CurrentPageIndex - 1) * Admin_Paging_VNP2.mPaging.PageSize + 1;

                DataTable mTable = mChargeLog.Search_SelectType(SearchType, Admin_Paging_VNP2.mPaging.BeginRow, Admin_Paging_VNP1.mPaging.EndRow, SearchContent, PID, 0, 0, 0, BeginDate, EndDate, 2, SortBy);

                foreach (DataRow mRow in mTable.Rows)
                {
                    if ((int)mRow["ChargeStatusID"] == 0)
                    {
                        mRow["ChargeStatusName"] = "Thành công";
                    }
                    else
                    {
                        mRow["ChargeStatusName"] = "Không thành công";
                    }

                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        int Admin_Paging_VNP3_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SearchContent = MSISDN;
                int PID = 0;

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);


                return mMOLog.TotalRow(SearchType, SearchContent, PID, 0, 0, BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging_VNP3_GetTotalPage_Callback_Change()
        {
            try
            {
                string SortBy = "LogID DESC";
                int SearchType = 0;

                string SearchContent = MSISDN;

                int PID = 0;

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                PageIndex_3 = (Admin_Paging_VNP3.mPaging.CurrentPageIndex - 1) * Admin_Paging_VNP3.mPaging.PageSize + 1;

                DataTable mTable = mMOLog.Search(SearchType, Admin_Paging_VNP3.mPaging.BeginRow, Admin_Paging_VNP3.mPaging.EndRow, SearchContent, PID, 0, 0, BeginDate, EndDate, SortBy);
                              
                DataColumn mCol_2 = new DataColumn("ReceiveDate", typeof(DateTime));

              
                if (!mTable.Columns.Contains("ReceiveDate"))
                {
                    mTable.Columns.Add(mCol_2);
                }
                foreach (DataRow mRow in mTable.Rows)
                {
                   
                    DateTime mDate_Receive = (DateTime)mRow["LogDate"];
                    DateTime mDate_SendDate = new DateTime(mDate_Receive.Year, mDate_Receive.Month, mDate_Receive.Day, mDate_Receive.Hour, mDate_Receive.Minute, mDate_Receive.Second);

                    Random mRandom = new Random();
                    int Delay = 5;
                   mDate_Receive= mDate_Receive.AddSeconds(-Delay);

                    mRow["LogDate"] = mDate_SendDate;
                    mRow["ReceiveDate"] = mDate_Receive;

                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}