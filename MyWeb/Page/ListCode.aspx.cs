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
    public partial class ListCode1 : System.Web.UI.Page
    {
        public int PageIndex = 1;
        AnswerLog mAnswerLog = new AnswerLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                if (Session["MSISDN"] == null || string.IsNullOrEmpty( Session["MSISDN"].ToString()))
                {
                    Response.Redirect(MyConfig.Domain + "/Page/Login.aspx", false);
                    return;
                }

                Admin_Paging1.rpt_Data = rpt_Data;
                Admin_Paging1.GetData_Callback_Change += new Admin_Paging.GetData_Callback(Admin_Paging1_GetData_Callback_Change);
                Admin_Paging1.GetTotalPage_Callback_Change += new Admin_Paging.GetTotalPage_Callback(Admin_Paging1_GetTotalPage_Callback_Change);
            }
            catch(Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, true, MyNotice.EndUserError.LoadDataError, "Chilinh");
            }
        }

        int Admin_Paging1_GetTotalPage_Callback_Change()
        {
            try
            {
                int SearchType = 0;

                string MSISDN = Session["MSISDN"].ToString();

                int PID = 0;

                DateTime BeginDate = MyConfig.StartDayOfMonth;
                DateTime EndDate = DateTime.Now;

                PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.WebSetting.MaxPID);


                return mAnswerLog.TotalRow(SearchType, MSISDN, PID, 0, BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        System.Data.DataTable Admin_Paging1_GetData_Callback_Change()
        {
            try
            {
                string OrderBy = " CodeDate DESC ";
                int SearchType = 0;

                string MSISDN = Session["MSISDN"].ToString();

                int PID = 0;

                DateTime BeginDate = MyConfig.StartDayOfMonth;
                DateTime EndDate = DateTime.Now;

                PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.WebSetting.MaxPID);

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                DataTable mTable = mAnswerLog.Search(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, MSISDN, PID, 0, BeginDate, EndDate, OrderBy);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                Admin_Paging1.ResetLoadData();

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, true, MyNotice.EndUserError.LoadDataError, "Chilinh");
            }
        }
        protected void lbtn_LogOut_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MSISDN"] = string.Empty;
                Response.Redirect(MyConfig.Domain + "/Page/Login.aspx", false);
                return;
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, true, MyNotice.EndUserError.LoadDataError, "Chilinh");
            }
        }
    }
}