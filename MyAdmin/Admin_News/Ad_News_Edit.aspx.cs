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
using MySportMillion.News;
using MySportMillion.Service;
namespace MyAdmin.Admin_News
{
    public partial class Ad_News_Edit : MyASPXBase
    {
        public GetRole mGetRole;
        News mNews = new News();    
        int EditID = 0;
        public int Content_MaxLength = 480;

        public string ParentPath = "../Admin_News/Ad_News.aspx";

        /// <summary>
        /// Lưu dữ liêu service mỗi lần thay đổi select index của ddl_service
        /// </summary>
        public DataTable SaveService
        {
            get
            {
                if (ViewState["SaveService"] == null)
                    return null;
                else
                    return (DataTable)ViewState["SaveService"];

            }
            set
            {
                ViewState["SaveService"] = value;
            }

        }
        private void BindCombo(int type)
        {
            try
            {
                Category mCate = new Category();

                switch (type)
                {
                  
                    case 1:
                        sel_Status.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(News.Status), true);
                        sel_Status.DataTextField = "Text";
                        sel_Status.DataValueField = "ID";
                        sel_Status.DataBind();
                        break;
                    case 2:
                        sel_NewsType.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(News.NewsType), true);
                        sel_NewsType.DataTextField = "Text";
                        sel_NewsType.DataValueField = "ID";
                        sel_NewsType.DataBind();
                        break;                   
                    case 3: // Bind dữ liệu về giờ
                        sel_PushHour.DataSource =  MyEnum.GetDataFromTime(3, string.Empty, string.Empty);
                        sel_PushHour.DataValueField =  "ID";
                        sel_PushHour.DataTextField = "Text";
                        sel_PushHour.DataBind();
                       
                        sel_PushHour.Items.Insert(0, new ListItem("--Giờ--", "-1"));
                        
                        break;
                    case 4: // Bind dữ liệu về Phút
                        sel_PushMinute.DataSource =  MyEnum.GetDataFromTime(4, string.Empty, string.Empty);
                        sel_PushMinute.DataValueField =  "ID";
                        sel_PushMinute.DataTextField =  "Text";
                        sel_PushMinute.DataBind();
                        sel_PushMinute.Items.Insert(0, new ListItem("--Phút--", "0"));
                        break;
                    case 5: // Bind dữ liệu về Giây
                        sel_PushSecond.DataSource =  MyEnum.GetDataFromTime(5, string.Empty, string.Empty);
                        sel_PushSecond.DataValueField =  "ID";
                        sel_PushSecond.DataTextField = "Text";
                        sel_PushSecond.DataBind();
                        sel_PushSecond.Items.Insert(0, new ListItem("--Giây--", "0"));
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.News, Member.MemberGroupID());
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
                    BindCombo(4);
                    BindCombo(5);

                    tbx_PushTime.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    //Nếu là Edit
                    if (EditID > 0)
                    {
                        DataTable mTable = mNews.Select(1, EditID.ToString());

                        //Lưu lại thông tin OldData để lưu vào MemberLog
                        ViewState["OldData"] = MyXML.GetXML(mTable);

                        if (mTable != null && mTable.Rows.Count > 0)
                        {
                            #region MyRegion
                            DataRow mRow = mTable.Rows[0];
                          
                            if (mRow["StatusID"] != DBNull.Value)
                                sel_Status.SelectedIndex = sel_Status.Items.IndexOf(sel_Status.Items.FindByValue(mRow["StatusID"].ToString()));

                            if (mRow["NewsTypeID"] != DBNull.Value)
                                sel_NewsType.SelectedIndex = sel_NewsType.Items.IndexOf(sel_NewsType.Items.FindByValue(mRow["NewsTypeID"].ToString()));

                            tbx_NewsName.Value = mRow["NewsName"].ToString();
                            tbx_Contents.Value = mRow["Content"].ToString();

                            tbx_Priority.Value = mRow["Priority"].ToString();
                            
                            if (mRow["PushTime"] != DBNull.Value)
                            {
                                DateTime mDateTime = (DateTime)mRow["PushTime"];
                                tbx_PushTime.Value = mDateTime.ToString(MyConfig.ShortDateFormat);
                                sel_PushHour.SelectedIndex = sel_PushHour.Items.IndexOf(sel_PushHour.Items.FindByValue(mDateTime.Hour.ToString()));
                                sel_PushMinute.SelectedIndex = sel_PushMinute.Items.IndexOf(sel_PushMinute.Items.FindByValue(mDateTime.Minute.ToString()));
                                sel_PushSecond.SelectedIndex = sel_PushSecond.Items.IndexOf(sel_PushSecond.Items.FindByValue(mDateTime.Second.ToString()));
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
        private void AddNewRow(ref DataSet mSet)
        {
            MyConvert.ConvertDateColumnToStringColumn(ref mSet);
            DataRow mNewRow = mSet.Tables["Child"].NewRow();

            if (EditID > 0)
                mNewRow["NewsID"] = EditID;          
          
            mNewRow["NewsName"] = tbx_NewsName.Value;
            mNewRow["Content"] = tbx_Contents.Value;
            mNewRow["CreateDate"] = DateTime.Now.ToString(MyConfig.DateFormat_InsertToDB);

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

            if (sel_NewsType.SelectedIndex >= 0 && sel_NewsType.Items.Count > 0)
            {
                mNewRow["NewsTypeID"] = int.Parse(sel_NewsType.Value);
                mNewRow["NewsTypeName"] = sel_NewsType.Items[sel_NewsType.SelectedIndex].Text;
            }
         

            if (tbx_PushTime.Value.Length > 0)
            {
                int Hour = 0;
                int Minute = 0;
                int Second = 0;
                DateTime TempDate = DateTime.ParseExact(tbx_PushTime.Value, "dd/MM/yyyy", null);

                if (sel_PushHour.SelectedIndex > 0)
                    int.TryParse(sel_PushHour.Value, out Hour);
                if (sel_PushMinute.SelectedIndex > 0)
                    int.TryParse(sel_PushMinute.Value, out Minute);
                if (sel_PushSecond.SelectedIndex > 0)
                    int.TryParse(sel_PushSecond.Value, out Second);

                mNewRow["PushTime"] = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, Second).ToString(MyConfig.DateFormat_InsertToDB);
            }


            mSet.Tables["Child"].Rows.Add(mNewRow);
           
            mSet.AcceptChanges();
        }

        private void Save(bool IsApply)
        {
            try
            {
                if (tbx_Contents.Value.Length > Content_MaxLength)
                {
                    MyMessage.ShowError("Nội dung bản tin không được vượt quá " + Content_MaxLength .ToString()+ " ký tự, xin hãy xem lại");
                    return;
                }

                DataSet mSet = mNews.CreateDataSet();
                AddNewRow(ref mSet);
                //Nếu là Edit
                if (EditID > 0)
                {
                    if (mNews.Update(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Update;
                        mLog.Insert("News", ViewState["OldData"].ToString(), mSet.GetXml(), Action, true, string.Empty);
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
                    if (mNews.Insert(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Insert;
                        mLog.Insert("News", string.Empty, mSet.GetXml(), Action, true, string.Empty);
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
