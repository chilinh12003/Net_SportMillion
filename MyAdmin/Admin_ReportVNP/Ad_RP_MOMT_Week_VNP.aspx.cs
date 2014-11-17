using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI.DataVisualization.Charting;
using MyUtility;
using MySportMillion;
using MySportMillion.Service;
using MySportMillion.Sub;
using MySportMillion.Report;

namespace MyAdmin.Admin_ReportVNP
{
    public partial class Ad_RP_MOMT_Week_VNP : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        RP_MO mRP_MO = new RP_MO();
        public DateTime ReportDate_Save = DateTime.MinValue;
        public DateTime ReportDate_Save_Total = DateTime.MinValue;

        bool IsWhite
        {
            get
            {
                if (ViewState["IsWhite"] == null)
                {
                    ViewState["IsWhite"] = true;
                }
                return (bool)ViewState["IsWhite"];
            }
            set
            {
                ViewState["IsWhite"] = value;
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

        private void BindData()
        {
            Admin_Paging1.ResetLoadData();
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

        public string GetDay(int year, int week)
        {
            string Result = string.Empty;
            DateTime FirtOfWeek = MyConvert.GetFirstDayOfWeek(year, week);
            DateTime LastOfWeek = MyConvert.GetLastDayOfWeek(year, week);

            Result = FirtOfWeek.ToString(MyConfig.ShortDateFormat) + "-" + LastOfWeek.ToString(MyConfig.ShortDateFormat);
            return Result;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            bool IsRedirect = false;
            try
            {
                //Phân quyền
                if (ViewState["Role"] == null)
                {
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.RP_WeekMOMT_VNP, Member.MemberGroupID());
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

                    tbx_FromDate.Value = MyConfig.StartDayOfMonth.ToString(MyConfig.ShortDateFormat);
                    tbx_ToDate.Value = DateTime.Now.ToString(MyConfig.ShortDateFormat);
                }

                Admin_Paging1.rpt_Data = rpt_Data;
                Admin_Paging1.GetData_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetData_Callback(Admin_Paging1_GetData_Callback_Change);
                Admin_Paging1.GetTotalPage_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetTotalPage_Callback(Admin_Paging1_GetTotalPage_Callback_Change);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        private bool ModifyDate(ref DateTime BeginDate, ref DateTime EndDate)
        {
            try
            {
                DateTime Current = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                int Week_Begin = MyConvert.GetWeekOfYear(BeginDate);
                int Week_End = MyConvert.GetWeekOfYear(EndDate);
                int Week_Current = MyConvert.GetWeekOfYear(Current);

                DateTime FirstDate_Begin = MyConvert.GetFirstDayOfWeek(BeginDate.Year, Week_Begin);
                DateTime LastDate_End = MyConvert.GetLastDayOfWeek(EndDate.Year, Week_End);
                DateTime LastDate_Current = MyConvert.GetLastDayOfWeek(Current.AddDays(-7).Year, Week_Current - 1);

                if (LastDate_End >= Current)
                {
                    LastDate_End = LastDate_Current;
                }

                BeginDate = FirstDate_Begin;
                EndDate = LastDate_End;

                if (BeginDate > EndDate)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int Admin_Paging1_GetTotalPage_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SortBy = ViewState["SortBy"].ToString();

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;

                if (!ModifyDate(ref BeginDate, ref EndDate))
                {
                    MyMessage.ShowError("Ngày tháng không hợp lệ, xin vui lòng kiểm tra lại.");
                    return 0;
                }
                return mRP_MO.TotalRow_Week_VNP(SearchType, BeginDate, EndDate);
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

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;

                if (!ModifyDate(ref BeginDate, ref EndDate))
                {
                    MyMessage.ShowError("Ngày tháng không hợp lệ, xin vui lòng kiểm tra lại.");
                    return new DataTable();
                }

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                DataTable mTable = mRP_MO.Search_Week_VNP(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, BeginDate, EndDate, SortBy);

                List<string> List_ReportDay = new List<string>();
                List<double> List_SubNew = new List<double>();
                List<double> List_UnsubNew = new List<double>();

                List<double> List_MOTotal = new List<double>();
                List<double> List_MOSuccess = new List<double>();

                double min = 100000000, max = 0;
                for (int i = mTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow mRow = mTable.Rows[i];
                    List_ReportDay.Add(mRow["ReportWeek"].ToString() + "/" + mRow["ReportYear"].ToString());

                    List_MOTotal.Add((double)mRow["MOTotal"]);
                    List_MOSuccess.Add((double)mRow["MOSuccess"]);

                    if ((double)mRow["MOTotal"] > max)
                        max = (double)mRow["MOTotal"];

                    if ((double)mRow["MOTotal"] < min)
                        min = (double)mRow["MOTotal"];

                }

                chart_Reg.Series["Series_Total"].Points.DataBindXY(List_ReportDay, List_MOTotal);
                chart_Reg.Series["Series_Total"].IsValueShownAsLabel = true;

                chart_Reg.Series["Series_Success"].Points.DataBindXY(List_ReportDay, List_MOSuccess);
                chart_Reg.Series["Series_Success"].IsValueShownAsLabel = true;

                chart_Reg.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart_Reg.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

                //chart_Reg.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
                chart_Reg.ChartAreas[0].AxisX.Interval = 1;
                chart_Reg.ChartAreas[0].AxisX.IsLabelAutoFit = true;
                chart_Reg.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                chart_Reg.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.StaggeredLabels;

                chart_Reg.Width = mTable.Rows.Count * 130;

                chart_Reg.ChartAreas[0].AxisY.Maximum = chart_Reg.ChartAreas[0].AxisY.Maximum + 10000;


                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtn_Sort_Click(object sender, EventArgs e)
        {
            try
            {
                //lbtn_Sort_1.CssClass = "Sort";
                //lbtn_Sort_2.CssClass = "Sort";
                //lbtn_Sort_3.CssClass = "Sort";
                //lbtn_Sort_4.CssClass = "Sort";
                //lbtn_Sort_5.CssClass = "Sort";
                //lbtn_Sort_6.CssClass = "Sort";
                //lbtn_Sort_7.CssClass = "Sort";

                LinkButton mLinkButton = (LinkButton)sender;
                ViewState["SortBy"] = mLinkButton.CommandArgument;

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

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }

    }
}