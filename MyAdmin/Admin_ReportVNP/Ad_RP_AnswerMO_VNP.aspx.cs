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
    public partial class Ad_RP_AnswerMO_VNP : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        RP_MO mRP_MO = new RP_MO();
        public DateTime ReportDate_Save = DateTime.MinValue;
        public DateTime ReportDate_Save_Total = DateTime.MinValue;
        public string LinkExportExcel()
        {
            try
            {
                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                string FileName = MySetting.AdminSetting.GenFileNameChartImage();

                chart_Reg.SaveImage(MyFile.GetFullPathFile("~/u/" + FileName), ChartImageFormat.Png);
                ExportExcelObject mEPObject = new ExportExcelObject(ExportExcelObject.ExportType.MODuDoan_Ngay, BeginDate, EndDate, DateTime.Now,FileName);

                string Para = mEPObject.Encrypt();
                return MyConfig.Domain + "/Admin_ReportVNP/ExportExcel.ashx?para=" + HttpUtility.UrlEncode(Para);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                return "#";
            }
        }
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

        protected void Page_Init(object sender, EventArgs e)
        {
            bool IsRedirect = false;
            try
            {
                //Phân quyền
                if (ViewState["Role"] == null)
                {
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.RP_DayMOAnswer_VNP, Member.MemberGroupID());
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

                    BindChart();
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

        int Admin_Paging1_GetTotalPage_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SortBy = ViewState["SortBy"].ToString();

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;

                return mRP_MO.TotalRow_VNP(SearchType, BeginDate, EndDate);
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

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                DataTable mTable = mRP_MO.Search_VNP(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, BeginDate, EndDate, SortBy);

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
                BindChart();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }

        private void BindChart()
        {
            try
            {
                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;

                DataTable mTable = mRP_MO.Search_VNP(0, 0, 10000, BeginDate, EndDate, string.Empty);

                List<string> List_ReportDay = new List<string>();
                List<double> List_SubNew = new List<double>();
                List<double> List_UnsubNew = new List<double>();

                List<double> List_RenewTotal = new List<double>();
                List<double> List_RenewSuccess = new List<double>();

                double min = 100000000, max = 0;
                for (int i = mTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow mRow = mTable.Rows[i];
                    List_ReportDay.Add(((DateTime)mRow["ReportDay"]).ToString("dd/MM"));

                    List_RenewTotal.Add((double)mRow["MOAnswerTotal"]);
                    List_RenewSuccess.Add((double)mRow["MOAnswerSuccess"]);

                    if ((double)mRow["MOAnswerTotal"] > max)
                        max = (double)mRow["MOAnswerTotal"];

                    if ((double)mRow["MOAnswerTotal"] < min)
                        min = (double)mRow["MOAnswerTotal"];

                }

                chart_Reg.Series["Series_Total"].Points.DataBindXY(List_ReportDay, List_RenewTotal);
                chart_Reg.Series["Series_Total"].IsValueShownAsLabel = true;

                chart_Reg.Series["Series_Success"].Points.DataBindXY(List_ReportDay, List_RenewSuccess);
                chart_Reg.Series["Series_Success"].IsValueShownAsLabel = true;

                chart_Reg.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                chart_Reg.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

                //chart_Reg.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
                chart_Reg.ChartAreas[0].AxisX.Interval = 1;
                chart_Reg.ChartAreas[0].AxisX.IsLabelAutoFit = true;
                chart_Reg.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                chart_Reg.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.StaggeredLabels;

                chart_Reg.Width = mTable.Rows.Count * 80;

                chart_Reg.ChartAreas[0].AxisY.Maximum = max + 1000;

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }
    }
}