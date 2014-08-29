using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;

namespace MyLoad_Wap.LoadService
{
    public class MyLoadListCode : MyLoadBase
    {
        public string MSISDN = string.Empty;
        public int PageIndex = 0;

        SubCode mSubCode = new SubCode();
        int PID = 0;

        public MyLoadListCode(string MSISDN, int PageIndex)
        {
            mTemplatePath = "~/Templates/Service/ListCode.htm";
            mTemplatePath_Repeat = "~/Templates/Service/ListCode_Repeat.htm";
            this.MSISDN = MSISDN;
            this.PageIndex = PageIndex;
            
            PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.WebSetting.MaxPID);
            Init();

        }
        protected override string BuildHTML()
        {
            try
            {
                string OrderBy = " CodeDate DESC ";

                MyLoadPaging mPaging = new MyLoadPaging();

                mPaging.PageLink = "checkcode.ashx";
                mPaging.CurrentPageIndex = PageIndex;

                mPaging.PageSize = 15;
                DateTime BeginDate = MyConfig.StartDayOfMonth;
                DateTime EndDate = DateTime.Now;

                PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.WebSetting.MaxPID);
                mPaging.TotalRow = mSubCode.TotalRow(0, string.Empty, PID, MSISDN, 0);

                DataTable mTable = mSubCode.Search(0, mPaging.BeginRow, mPaging.EndRow, string.Empty, PID, MSISDN, 0, OrderBy);

                if (mTable.Rows.Count < 1)
                {
                    LoadStatic.MyLoadNote mNote = new LoadStatic.MyLoadNote("Không có dữ liệu về mã dự thưởng, có thể bạn chưa đăng ký dịch vụ hoặc mới đăng ký trong hôm nay.");
                    return mNote.GetHTML();
                }

                StringBuilder mBuilder = new StringBuilder(string.Empty);
                foreach (DataRow mRow in mTable.Rows)
                {
                    
                    mBuilder.Append(mLoadTempLate.LoadTemplateByArray(
                                        mTemplatePath_Repeat,
                                        new string[] {((DateTime)mRow["CodeDate"]).ToString(MyUtility.MyConfig.ShortDateFormat) ,
                                                        mRow["Code"].ToString()}));
                }
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, new string[] { MSISDN, mBuilder.ToString(), mPaging.GetHTML() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}