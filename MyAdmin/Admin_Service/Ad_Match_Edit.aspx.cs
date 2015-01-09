using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using MyUtility; using MyBase.MyWeb;
using MySportMillion;

using MySportMillion.Service;

namespace MyAdmin.Admin_Service
{
    public partial class Ad_Match_Edit : MyASPXBase
    {
        public GetRole mGetRole;
        Match mMatch = new Match();
        int EditID = 0;
        public int Content_MaxLength = 480;

        public string ParentPath = "../Admin_Service/Ad_Match.aspx";


        private void BindCombo(int type)
        {
            try
            {
                Category mCate = new Category();

                switch (type)
                {

                    case 1:
                        sel_Status.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(Match.Status), true);
                        sel_Status.DataTextField = "Text";
                        sel_Status.DataValueField = "ID";
                        sel_Status.DataBind();
                        break;
                    case 2: // Bind dữ liệu về giờ
                        sel_PlayHour.DataSource = MyEnum.GetDataFromTime(3, string.Empty, string.Empty);
                        sel_PlayHour.DataValueField = "ID";
                        sel_PlayHour.DataTextField = "Text";
                        sel_PlayHour.DataBind();
                        sel_PlayHour.Items.Insert(0, new ListItem("--Giờ--", "-1"));

                        sel_BeginHour.DataSource = MyEnum.GetDataFromTime(3, string.Empty, string.Empty);
                        sel_BeginHour.DataValueField = "ID";
                        sel_BeginHour.DataTextField = "Text";
                        sel_BeginHour.DataBind();
                        sel_EndHour.Items.Insert(0, new ListItem("--Giờ--", "-1"));

                        sel_EndHour.DataSource = MyEnum.GetDataFromTime(3, string.Empty, string.Empty);
                        sel_EndHour.DataValueField = "ID";
                        sel_EndHour.DataTextField = "Text";
                        sel_EndHour.DataBind();
                        sel_EndHour.Items.Insert(0, new ListItem("--Giờ--", "-1"));
                        break;
                    case 3: // Bind dữ liệu về Phút
                        sel_PlayMinute.DataSource = MyEnum.GetDataFromTime(4, string.Empty, string.Empty);
                        sel_PlayMinute.DataValueField = "ID";
                        sel_PlayMinute.DataTextField = "Text";
                        sel_PlayMinute.DataBind();
                        sel_PlayMinute.Items.Insert(0, new ListItem("--Phút--", "0"));

                        sel_BeginMinute.DataSource = MyEnum.GetDataFromTime(4, string.Empty, string.Empty);
                        sel_BeginMinute.DataValueField = "ID";
                        sel_BeginMinute.DataTextField = "Text";
                        sel_BeginMinute.DataBind();
                        sel_BeginMinute.Items.Insert(0, new ListItem("--Phút--", "0"));

                        sel_EndMinute.DataSource = MyEnum.GetDataFromTime(4, string.Empty, string.Empty);
                        sel_EndMinute.DataValueField = "ID";
                        sel_EndMinute.DataTextField = "Text";
                        sel_EndMinute.DataBind();
                        sel_EndMinute.Items.Insert(0, new ListItem("--Phút--", "0"));
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
                if (EditID > 0)
                {
                    lbtn_Save.Visible = lbtn_Accept.Visible = mGetRole.EditRole;
                    link_Add.Visible = mGetRole.AddRole;
                }
                else
                {
                    lbtn_Save.Visible = lbtn_Accept.Visible = link_Add.Visible = mGetRole.AddRole;
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.Match, Member.MemberGroupID());
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

                //Lấy memberID nếu là trước hợp Sửa
                EditID = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

                MyAdmin.MasterPages.Admin mMaster = (MyAdmin.MasterPages.Admin)Page.Master;
                mMaster.str_PageTitle = mGetRole.PageName;
                mMaster.str_TitleSearchBox = "Thông tin về " + mGetRole.PageName;

                if (!IsPostBack)
                {

                    BindCombo(1);
                    BindCombo(2);
                    BindCombo(3);
                    tbx_CodeDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    tbx_PlayDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    tbx_BeginDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    tbx_EndDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    //Nếu là Edit
                    if (EditID > 0)
                    {
                        DataTable mTable = mMatch.Select(1, EditID.ToString());

                        //Lưu lại thông tin OldData để lưu vào MemberLog
                        ViewState["OldData"] = MyXML.GetXML(mTable);

                        if (mTable != null && mTable.Rows.Count > 0)
                        {
                            #region MyRegion
                            DataRow mRow = mTable.Rows[0];

                            if (mRow["StatusID"] != DBNull.Value)
                                sel_Status.SelectedIndex = sel_Status.Items.IndexOf(sel_Status.Items.FindByValue(mRow["StatusID"].ToString()));

                            tbx_TeamName1.Value = mRow["TeamName1"].ToString();
                            tbx_TeamName2.Value = mRow["TeamName2"].ToString();
                            tbx_Description.Value = mRow["Description"].ToString();
                          
                            tbx_AnswerKQ.Value = mRow["AnswerKQ"].ToString();
                            tbx_AnswerBT.Value = mRow["AnswerBT"].ToString();
                            tbx_AnswerGB.Value = mRow["AnswerGB"].ToString();
                            tbx_AnswerTS.Value = mRow["AnswerTS"].ToString();
                            tbx_AnswerTV.Value = mRow["AnswerTV"].ToString();

                            tbx_Priority.Value = mRow["Priority"].ToString();
                            if (mRow["CodeDate"] != DBNull.Value)
                            {
                                DateTime mDateTime = (DateTime)mRow["CodeDate"];
                                tbx_CodeDate.Value = mDateTime.ToString(MyConfig.ShortDateFormat);
                            }

                            if (mRow["PlayDate"] != DBNull.Value)
                            {
                                DateTime mDateTime = (DateTime)mRow["PlayDate"];
                                tbx_PlayDate.Value = mDateTime.ToString(MyConfig.ShortDateFormat);
                                sel_PlayHour.SelectedIndex = sel_PlayHour.Items.IndexOf(sel_PlayHour.Items.FindByValue(mDateTime.Hour.ToString()));
                                sel_PlayMinute.SelectedIndex = sel_PlayMinute.Items.IndexOf(sel_PlayMinute.Items.FindByValue(mDateTime.Minute.ToString()));
                            }

                            if (mRow["BeginDate"] != DBNull.Value)
                            {
                                DateTime mDateTime = (DateTime)mRow["BeginDate"];
                                tbx_BeginDate.Value = mDateTime.ToString(MyConfig.ShortDateFormat);
                                sel_BeginHour.SelectedIndex = sel_BeginHour.Items.IndexOf(sel_BeginHour.Items.FindByValue(mDateTime.Hour.ToString()));
                                sel_BeginMinute.SelectedIndex = sel_BeginMinute.Items.IndexOf(sel_BeginMinute.Items.FindByValue(mDateTime.Minute.ToString()));
                            }
                            if (mRow["EndDate"] != DBNull.Value)
                            {
                                DateTime mDateTime = (DateTime)mRow["EndDate"];
                                tbx_EndDate.Value = mDateTime.ToString(MyConfig.ShortDateFormat);
                                sel_EndHour.SelectedIndex = sel_EndHour.Items.IndexOf(sel_EndHour.Items.FindByValue(mDateTime.Hour.ToString()));
                                sel_EndMinute.SelectedIndex = sel_EndMinute.Items.IndexOf(sel_EndMinute.Items.FindByValue(mDateTime.Minute.ToString()));
                            }
                            #endregion
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.LoadDataError, true, ex);
            }

        }
        private bool CheckDate()
        {
            try
            {
                if (tbx_PlayDate.Value.Length > 0)
                {
                    int Hour = 0;
                    int Minute = 0;
                    DateTime TempDate = DateTime.ParseExact(tbx_PlayDate.Value, "dd/MM/yyyy", null);

                    if (sel_PlayHour.SelectedIndex > 0)
                        int.TryParse(sel_PlayHour.Value, out Hour);
                    if (sel_PlayMinute.SelectedIndex > 0)
                        int.TryParse(sel_PlayMinute.Value, out Minute);

                    DateTime PlayDate = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, 0);

                    if (PlayDate < DateTime.Now)
                    {
                        MyMessage.ShowError("Thời gian BẮT ĐẦU TRẬN ĐẤU không được nhỏ hơn ngày hiện tại");
                        return false;
                    }
                }
                if (tbx_BeginDate.Value.Length > 0)
                {
                    int Hour = 0;
                    int Minute = 0;
                    DateTime TempDate = DateTime.ParseExact(tbx_BeginDate.Value, "dd/MM/yyyy", null);

                    if (sel_BeginHour.SelectedIndex > 0)
                        int.TryParse(sel_BeginHour.Value, out Hour);
                    if (sel_BeginMinute.SelectedIndex > 0)
                        int.TryParse(sel_BeginMinute.Value, out Minute);

                    DateTime BeginDate = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, 0);

                    if (BeginDate < DateTime.Now)
                    {
                        MyMessage.ShowError("Thời gian BẮT ĐẦU DỮ ĐOÁN không được nhỏ hơn ngày hiện tại");
                        return false;
                    }
                }
                if (tbx_EndDate.Value.Length > 0)
                {
                    int Hour = 0;
                    int Minute = 0;
                    DateTime TempDate = DateTime.ParseExact(tbx_EndDate.Value, "dd/MM/yyyy", null);

                    if (sel_EndHour.SelectedIndex > 0)
                        int.TryParse(sel_EndHour.Value, out Hour);
                    if (sel_EndMinute.SelectedIndex > 0)
                        int.TryParse(sel_EndMinute.Value, out Minute);

                    DateTime EndDate = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, 0);

                    if (EndDate < DateTime.Now)
                    {
                        MyMessage.ShowError("Thời gian KẾT THÚC DỮ ĐOÁN không được nhỏ hơn ngày hiện tại");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AddNewRow(ref DataSet mSet)
        {
            MyConvert.ConvertDateColumnToStringColumn(ref mSet);
            DataRow mNewRow = mSet.Tables["Child"].NewRow();

            if (EditID > 0)
                mNewRow["MatchID"] = EditID;
         

            mNewRow["TeamName1"] = (tbx_TeamName1.Value);
            mNewRow["TeamName2"] = (tbx_TeamName2.Value);
            mNewRow["Description"] = tbx_Description.Value;
            mNewRow["CreateDate"] = DateTime.Now.ToString(MyConfig.DateFormat_InsertToDB);

            mNewRow["AnswerKQ"] =( tbx_AnswerKQ.Value);
            mNewRow["AnswerBT"] = (tbx_AnswerBT.Value);
            mNewRow["AnswerGB"] = (tbx_AnswerGB.Value);
            mNewRow["AnswerTS"] = (tbx_AnswerTS.Value);
            mNewRow["AnswerTV"] = (tbx_AnswerTV.Value);

            int Priority = 0;
            if (int.TryParse(tbx_Priority.Value, out Priority))
            {
                mNewRow["Priority"] = Priority;
            }

            if (sel_Status.SelectedIndex >= 0 && sel_Status.Items.Count > 0)
            {
                mNewRow["StatusID"] = int.Parse(sel_Status.Value);
                mNewRow["StatusName"] = sel_Status.Items[sel_Status.SelectedIndex].Text;
            }

            if (tbx_CodeDate.Value.Length > 0)
            {
                DateTime TempDate = DateTime.ParseExact(tbx_CodeDate.Value, "dd/MM/yyyy", null);
                mNewRow["CodeDate"] = TempDate.ToString(MyConfig.DateFormat_InsertToDB);

            }
            if (tbx_PlayDate.Value.Length > 0)
            {
                int Hour = 0;
                int Minute = 0;
                DateTime TempDate = DateTime.ParseExact(tbx_PlayDate.Value, "dd/MM/yyyy", null);

                if (sel_PlayHour.SelectedIndex > 0)
                    int.TryParse(sel_PlayHour.Value, out Hour);
                if (sel_PlayMinute.SelectedIndex > 0)
                    int.TryParse(sel_PlayMinute.Value, out Minute);

                DateTime PlayDate = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, 0);
              
                mNewRow["PlayDate"] = PlayDate.ToString(MyConfig.DateFormat_InsertToDB);
            }
            if (tbx_BeginDate.Value.Length > 0)
            {
                int Hour = 0;
                int Minute = 0;
                DateTime TempDate = DateTime.ParseExact(tbx_BeginDate.Value, "dd/MM/yyyy", null);

                if (sel_BeginHour.SelectedIndex > 0)
                    int.TryParse(sel_BeginHour.Value, out Hour);
                if (sel_BeginMinute.SelectedIndex > 0)
                    int.TryParse(sel_BeginMinute.Value, out Minute);

                mNewRow["BeginDate"] = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, 0).ToString(MyConfig.DateFormat_InsertToDB);
            }
            if (tbx_EndDate.Value.Length > 0)
            {
                int Hour = 0;
                int Minute = 0;
                DateTime TempDate = DateTime.ParseExact(tbx_EndDate.Value, "dd/MM/yyyy", null);

                if (sel_EndHour.SelectedIndex > 0)
                    int.TryParse(sel_EndHour.Value, out Hour);
                if (sel_EndMinute.SelectedIndex > 0)
                    int.TryParse(sel_EndMinute.Value, out Minute);

                mNewRow["EndDate"] = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, 0).ToString(MyConfig.DateFormat_InsertToDB);
            }

            mSet.Tables["Child"].Rows.Add(mNewRow);

            mSet.AcceptChanges();
        }

        private void Save(bool IsApply)
        {
            try
            {

                if (string.IsNullOrEmpty(tbx_TeamName1.Value) || string.IsNullOrEmpty(tbx_TeamName2.Value))
                {
                    MyMessage.ShowError("Tên đội bóng không được phép để trống");
                }
                if (string.IsNullOrEmpty(tbx_CodeDate.Value))
                {
                    MyMessage.ShowError("Ngày quay thưởng không được phép để trống");
                }
                if (string.IsNullOrEmpty(tbx_PlayDate.Value))
                {
                    MyMessage.ShowError("Thời gian diễn ra trận đấu dự đoán không được phép để trống");
                }
                if (string.IsNullOrEmpty(tbx_BeginDate.Value))
                {
                    MyMessage.ShowError("Ngày Bắt đầu dự đoán không được phép để trống");
                }
                if (string.IsNullOrEmpty(tbx_EndDate.Value))
                {
                    MyMessage.ShowError("Ngày Kết thúc dự đoán không được phép để trống");
                }

                if (!(EditID > 0) && !CheckDate())
                {
                    return;
                }

                DataSet mSet = mMatch.CreateDataSet();
                AddNewRow(ref mSet);
                //Nếu là Edit
                if (EditID > 0)
                {
                    if (mMatch.Update(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Update;
                        mLog.Insert("Match", ViewState["OldData"].ToString(), mSet.GetXml(), Action, true, string.Empty);
                        #endregion

                        if (IsApply)
                            MyMessage.ShowMessage("Cập nhật dữ liệu thành công.");
                        else
                        {
                            Response.Redirect(ParentPath, false);
                        }
                    }
                    else
                    {
                        MyMessage.ShowMessage("Cập nhật dữ liệu (KHÔNG) thành công!");
                    }
                }
                else
                {
                    if (mMatch.Insert(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Insert;
                        mLog.Insert("Match", string.Empty, mSet.GetXml(), Action, true, string.Empty);
                        #endregion

                        if (IsApply)
                            MyMessage.ShowMessage("Cập nhật dữ liệu thành công.");
                        else
                        {
                            Response.Redirect(ParentPath, false);
                        }
                    }
                    else
                    {
                        MyMessage.ShowMessage("Cập nhật dữ liệu (KHÔNG) thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Save(false);
            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.SaveDataError, true, ex);
            }
        }

        protected void lbtn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                Save(true);
            }
            catch (Exception ex)
            {
                mLog.Error(MyNotice.AdminError.SaveDataError, true, ex);
            }
        }

        protected void ddl_ServiceGroup_IndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindCombo(2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
