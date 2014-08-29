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


namespace MyAdmin.Admin_Report
{
    public partial class Ad_Partner_Reg : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        Subscriber mSub = new Subscriber();
        UnSubscriber mUnSub = new UnSubscriber();

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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.RPPartnerSub, Member.MemberGroupID());
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

        public string TotalSub()
        {
            try
            {
                DataTable mTable_Sub = mSub.Select(6, Member.PartnerID().ToString());
                if (mTable_Sub.Rows.Count > 0)
                    return mTable_Sub.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
            return "0";
        }
        public string TotalUnSub()
        {
            try
            {
                DataTable mTable_UnSub = mUnSub.Select(6, Member.PartnerID().ToString());
                if (mTable_UnSub.Rows.Count > 0)
                    return mTable_UnSub.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
            return "0";
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
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }
       
    }
}