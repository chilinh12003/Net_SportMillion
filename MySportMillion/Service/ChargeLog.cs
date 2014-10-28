using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MyConnect.SQLServer;
using MyUtility;
using System.Web;
using System.ComponentModel;


namespace MySportMillion.Service
{
    public class ChargeLog
    {
        public enum ChargeType
        {
            REG_DAILY = 1,
            RENEW_DAILY = 2,
            UNREG_DAILY = 3,

        }
        public enum ChargeStatus
        {
            ChargeSuccess = 0,
            BlanceTooLow = 1,
            WrongUserAndPassword = 2,
            ChargeNotComplete = 3,
            OtherError = 4,
            WrongSubscriberNumber = 5,
            SubDoesNotExist = 6,
            OverChargeLimit = 7,
            OverChargeLimit2 = 17,
            ServerInternalError = 8,
            ConfigError = 9,
            RequestIDIsNull = 10,
            InvalidSubscriptionState = 11,
            UnknowIP = 99,
            SynctaxXMLError = 100,
            UnknownRequest = 500,
            VNPAPIError = -1,

        }
        MyExecuteData mExec;
        MyGetData mGet;

        public ChargeLog()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public ChargeLog(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }

        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_ChargeLog_Select", mPara, mValue);
                if (mSet != null && mSet.Tables.Count >= 1)
                {
                    mSet.DataSetName = "Parent";
                    mSet.Tables[0].TableName = "Child";
                }
                return mSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">
        /// <para>Type = 8: Lay số lượng thuê bao theo từng dịch vụ và đối tác (Para_1 = PID, Para_2 = ChargeTypeID,Para_3 = ChargeStatusID, Para_4 = ChannelTypeID, Para_5 = BeginDate, Para_6 = EndDate</para>
        /// <para>Type = 11: Lấy tổng tiền theo từng dịch vụ và đối tác (Para_1 = PID, Para_2 = ChargeTypeID,Para_3 = ChargeStatusID, Para_4 = ChannelTypeID, Para_5 = BeginDate, Para_6 = EndDate</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <param name="Para_3"></param>
        /// <param name="Para_4"></param>
        /// <param name="Para_5"></param>
        /// <param name="Para_6"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3, string Para_4, string Para_5, string Para_6)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3", "Para_4", "Para_5", "Para_6" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3, Para_4, Para_5, Para_6 };
                return mGet.GetDataTable("Sp_ChargeLog_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">
        /// <para>Type = 7: Lay số lượng thuê bao theo từng dịch vụ và đối tác (Para_1 = PID, Para_2 = ChargeTypeID,Para_3 = ChargeStatusID, Para_4 = BeginDate, Para_5 = EndDate</para>
        /// <para>Type = 10: Lấy tổng tiền theo từng dịch vụ và đối tác (Para_1 = PID, Para_2 = ChargeTypeID,Para_3 = ChargeStatusID, Para_4 = BeginDate, Para_5 = EndDate</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <param name="Para_3"></param>
        /// <param name="Para_4"></param>
        /// <param name="Para_5"></param>
        /// <param name="Para_6"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3, string Para_4, string Para_5)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3", "Para_4", "Para_5" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3, Para_4, Para_5 };
                return mGet.GetDataTable("Sp_ChargeLog_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 2: Lấy tổng số lệnh charge (Para_1 = ChargeStatusID,Para_2= ChargeTypeID, Para_3=BeginDate, Para_4 = EndDate)</para>
        /// <para>Type = 6: Lay số lượng thuê bao theo từng dịch vụ và đối tác (Para_1 = PID, Para_2 = ChargeTypeID,Para_3 = BeginDate, Para_4 = EndDate</para>
        /// <para>Type = 9: Lấy tổng tiền theo từng dịch vụ và đối tác (Para_1 = PID, Para_2 = ChargeTypeID,Para_3 = BeginDate, Para_4 = EndDate</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <param name="Para_3"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3, string Para_4)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3", "Para_4" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3, Para_4 };
                return mGet.GetDataTable("Sp_ChargeLog_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 3: Lấy tổng số lệnh charge (Para_1= ChargeTypeID, Para_2=BeginDate, Para_3 = EndDate)</para>
        ///<para>Type = 4: Lấy tổng số lệnh charge (Para_1 = ChargeStatusID, Para_2=BeginDate, Para_3 = EndDate)</para>
        ///<para>Type = 6: Lấy tổng số lệnh charge group by (Para_1 = PID, Para_2=BeginDate, Para_3 = EndDate)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <param name="Para_3"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3 };
                return mGet.GetDataTable("Sp_ChargeLog_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 5: Lấy tổng số lệnh charge (Para_1 = BeginDate, Para_2 = EndDate)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2" };
                string[] mValue = { Type.ToString(), Para_1, Para_2 };
                return mGet.GetDataTable("Sp_ChargeLog_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 7: Lấy ngày nhỏ nhất theo PID (@Para_1 = PID)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_ChargeLog_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tổng lệnh charge theo tình trạng charge và theo loại charge
        /// </summary>
        /// <param name="mStatus"></param>
        /// <param name="mType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public int GetTotal(ChargeStatus mStatus, ChargeType mType, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                DataTable mTable = Select(2, ((int)mStatus).ToString(), ((int)mType).ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                if (mTable == null || mTable.Rows.Count < 1)
                    return 0;

                return int.Parse(mTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tổng lệnh charge theo kiểu charge
        /// </summary>
        /// <param name="mType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public int GetTotal(ChargeType mType, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                DataTable mTable = Select(3, ((int)mType).ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                if (mTable == null || mTable.Rows.Count < 1)
                    return 0;

                return int.Parse(mTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tổng lệnh charge theo tình trạng
        /// </summary>
        /// <param name="mStatus"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public int GetTotal(ChargeStatus mStatus, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                DataTable mTable = Select(4, ((int)mStatus).ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                if (mTable == null || mTable.Rows.Count < 1)
                    return 0;

                return int.Parse(mTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tổng lệnh charge
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public int GetTotal(DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                DataTable mTable = Select(5, BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                if (mTable == null || mTable.Rows.Count < 1)
                    return 0;

                return int.Parse(mTable.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_ChargeLog_Insert", mpara, mValue) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int TotalRow(int? Type, string SearchContent, int PID, int ChargeTypeID, int ChargeStatusID, int ChannelTypeID, DateTime BeginDate, DateTime EndDate)
        {
            try
            {

                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "SearchContent", "PID", "ChargeTypeID", "ChargeStatusID", "ChannelTypeID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), ChannelTypeID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_ChargeLog_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int ChargeTypeID, int ChargeStatusID, int ChannelTypeID, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "ChargeTypeID", "ChargeStatusID", "ChannelTypeID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), ChannelTypeID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_ChargeLog_Search", mpara, mValue);


                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TotalRow_ByDay(int? Type, int ChargeTypeID, int ChargeStatusID, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "ChargeTypeID", "ChargeStatusID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_ChargeLog_Search_ByDay", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_ByDay(int? Type, int BeginRow, int EndRow, int ChargeTypeID, int ChargeStatusID, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "ChargeTypeID", "ChargeStatusID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_ChargeLog_Search_ByDay", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int TotalRow_ByDay_Partner(int? Type, int ChargeTypeID, int ChargeStatusID, int PartnerID, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "ChargeTypeID", "ChargeStatusID", "BeginDate", "EndDate", "IsTotalRow", "PartnerID" };
                string[] mValue = { Type.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), str_BeginDate, str_EndDate, true.ToString(), PartnerID.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_ChargeLog_Search_ByDay_Partner", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_ByDay_Partner(int? Type, int BeginRow, int EndRow, int ChargeTypeID, int ChargeStatusID, int PartnerID, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "ChargeTypeID", "ChargeStatusID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow", "PartnerID" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString(), PartnerID.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_ChargeLog_Search_ByDay_Partner", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TotalRow_ByDay_Price(int? Type, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_ChargeLog_Search_ByDay_Price", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_ByDay_Price(int? Type, int BeginRow, int EndRow, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_ChargeLog_Search_ByDay_Price", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TotalRow_ByDay_Price_Partner(int? Type,int PartnerID, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "BeginDate", "EndDate", "IsTotalRow","PartnerID" };
                string[] mValue = { Type.ToString(), str_BeginDate, str_EndDate, true.ToString(), PartnerID.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_ChargeLog_Search_ByDay_Price_Partner", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_ByDay_Price_Partner(int? Type, int PartnerID, int BeginRow, int EndRow, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "BeginDate", "EndDate", "OrderBy", "IsTotalRow","PartnerID" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString(), PartnerID.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_ChargeLog_Search_ByDay_Price_Partner", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="SearchContent"></param>
        /// <param name="PID"></param>
        /// <param name="ChargeTypeID"></param>
        /// <param name="ChargeStatusID"></param>
        /// <param name="ChannelTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="SelectType">
        /// <para>SelectType= 1: Lấy lịch sử đăng ký, hủy</para>
        /// <para>SelectType= 2: Lấy lịch sử gia hạn</para>
        /// <para>SelectType= 3: Lấy lịch sử trử tiền</para>
        /// </param>
        /// <returns></returns>
        public int TotalRow_SelectType(int? Type, string SearchContent, int PID, int ChargeTypeID, int ChargeStatusID, int ChannelTypeID, DateTime BeginDate, DateTime EndDate, int SelectType)
        {
            try
            {

                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "SearchContent", "PID", "ChargeTypeID", "ChargeStatusID", "ChannelTypeID", "BeginDate", "EndDate", "SelectType", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), ChannelTypeID.ToString(), str_BeginDate, str_EndDate, SelectType.ToString(), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_ChargeLog_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_SelectType(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int ChargeTypeID, int ChargeStatusID, int ChannelTypeID, DateTime BeginDate, DateTime EndDate, int SelectType, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "ChargeTypeID", "ChargeStatusID", "ChannelTypeID", "BeginDate", "EndDate", "SelectType", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), ChargeTypeID.ToString(), ChargeStatusID.ToString(), ChannelTypeID.ToString(), str_BeginDate, str_EndDate, SelectType.ToString(), OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_ChargeLog_Search", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
