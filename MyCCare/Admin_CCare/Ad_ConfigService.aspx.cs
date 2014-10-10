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

namespace MyCCare.Admin_CCare
{
    public partial class Ad_ConfigService : System.Web.UI.Page
    {
        public int PageIndex = 1;
        Subscriber mSub = new Subscriber();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                MyCCare.MasterPages.Admin mMaster = (MyCCare.MasterPages.Admin)Page.Master;
                mMaster.Title = "GUI - Cài đặt dịch vụ";

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
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }
    }
}