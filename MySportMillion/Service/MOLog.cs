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
    public class MOLog
    {
        MyExecuteData mExec;
        MyGetData mGet;

        public MOLog()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public MOLog(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }

        public string ConvertMTTypeIDToDescription(DefineMT.MTType mMTType, MyConfig.ChannelType mChannelType)
        {
            try
            {
                if (mChannelType == MyConfig.ChannelType.WAP)
                {
                    if (
                        mMTType == DefineMT.MTType.RegNewSuccess ||
                        mMTType == DefineMT.MTType.RegAgainSuccessFree ||
                        mMTType == DefineMT.MTType.RegAgainSuccessNotFree ||
                        mMTType == DefineMT.MTType.RegRepeatFree ||
                        mMTType == DefineMT.MTType.RegRepeatNotFree ||
                        mMTType == DefineMT.MTType.RegNotEnoughMoney ||
                        mMTType == DefineMT.MTType.RegFail ||
                        mMTType == DefineMT.MTType.RegSystemError)
                    {
                        return "Đăng ký từ wapsite";
                    }
                    else if( 
                        mMTType == DefineMT.MTType.DeregSuccess ||
                        mMTType == DefineMT.MTType.DeregNotRegister ||
                        mMTType == DefineMT.MTType.DeregConfirm ||
                        mMTType == DefineMT.MTType.DeregFail ||
                        mMTType == DefineMT.MTType.DeregSystemError ||
                        mMTType == DefineMT.MTType.DeregNotSendConfirm ||
                        mMTType == DefineMT.MTType.ExtendDereg)
                    {
                        return "Hủy đăng ký từ wapsite";
                    }
                    else
                        return "Truy cập wapsite";
                }
                else
                {
                    if (mMTType == DefineMT.MTType.Invalid)
                        return "Gửi MO sai cú pháp";
                    else if (mMTType == DefineMT.MTType.RegNewSuccess ||
                            mMTType == DefineMT.MTType.RegAgainSuccessFree ||
                            mMTType == DefineMT.MTType.RegAgainSuccessNotFree ||
                            mMTType == DefineMT.MTType.RegRepeatFree ||
                            mMTType == DefineMT.MTType.RegRepeatNotFree ||
                            mMTType == DefineMT.MTType.RegNotEnoughMoney ||
                            mMTType == DefineMT.MTType.RegFail ||
                            mMTType == DefineMT.MTType.RegSystemError)
                    {
                        return "Gửi MO DK dịch vụ";
                    }
                    else if (
                        mMTType == DefineMT.MTType.DeregSuccess ||
                        mMTType == DefineMT.MTType.DeregNotRegister ||
                        mMTType == DefineMT.MTType.DeregConfirm ||
                        mMTType == DefineMT.MTType.DeregFail ||
                        mMTType == DefineMT.MTType.DeregSystemError ||
                        mMTType == DefineMT.MTType.DeregNotSendConfirm ||
                        mMTType == DefineMT.MTType.ExtendDereg)
                    {
                        return "Gửi MO HUY dịch vụ";
                    }
                    else
                        return "Gửi MO";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConvertMTTypeIDToActionName(DefineMT.MTType mMTType, MyConfig.ChannelType mChannelType)
        {
            try
            {
                if (mChannelType == MyConfig.ChannelType.WAP)
                {
                    return "Truy cập WAP";
                }
                else
                {
                    if (mMTType == DefineMT.MTType.Invalid)
                        return "Gửi MO";
                    else if (
                        mMTType == DefineMT.MTType.RegNewSuccess ||
                        mMTType == DefineMT.MTType.RegAgainSuccessFree ||
                        mMTType == DefineMT.MTType.RegAgainSuccessNotFree ||
                        mMTType == DefineMT.MTType.RegRepeatFree ||
                        mMTType == DefineMT.MTType.RegRepeatNotFree ||
                        mMTType == DefineMT.MTType.RegNotEnoughMoney ||
                        mMTType == DefineMT.MTType.RegFail ||
                        mMTType == DefineMT.MTType.RegSystemError )
                    {
                        return "Gửi MO";
                    }
                    else if (
                        mMTType == DefineMT.MTType.DeregSuccess ||
                        mMTType == DefineMT.MTType.DeregNotRegister ||
                        mMTType == DefineMT.MTType.DeregConfirm ||
                        mMTType == DefineMT.MTType.DeregFail ||
                        mMTType == DefineMT.MTType.DeregSystemError ||
                        mMTType == DefineMT.MTType.DeregNotSendConfirm ||
                        mMTType == DefineMT.MTType.ExtendDereg)
                    {
                        return "Gửi MO";
                    }
                    else
                        return "Gửi MO";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_MOLog_Select", mPara, mValue);
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

        public bool Insert(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_MOLog_Insert", mpara, mValue) > 0)
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


        public int TotalRow(int? Type, string SearchContent, int PID,  int MTTypeID, int ChannelTypeID, DateTime BeginDate, DateTime EndDate)
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
                string[] mPara = { "Type", "SearchContent", "PID", "MTTypeID", "ChannelTypeID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(),  MTTypeID.ToString(),  ChannelTypeID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_MOLog_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int MTTypeID,  int ChannelTypeID, DateTime BeginDate, DateTime EndDate, string OrderBy)
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

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "MTTypeID",  "ChannelTypeID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), MTTypeID.ToString(),  ChannelTypeID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_MOLog_Search", mpara, mValue);              
              

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int TotalRow_Action(int? Type, string SearchContent, int PID, int ServiceID, DateTime BeginDate, DateTime EndDate)
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
                string[] mPara = { "Type", "SearchContent", "PID", "ServiceID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(), ServiceID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_MOLog_Search_Action", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_Action(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int ServiceID, DateTime BeginDate, DateTime EndDate, string OrderBy)
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

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "ServiceID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), ServiceID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_MOLog_Search_Action", mpara, mValue);
              

                DataColumn mCol_2 = new DataColumn("ActionName", typeof(string));
                mTable.Columns.Add(mCol_2);

                DataColumn mCol_3 = new DataColumn("Description", typeof(string));
                mTable.Columns.Add(mCol_3);

               
                foreach (DataRow mRow in mTable.Rows)
                {
                    mRow["ActionName"] = ConvertMTTypeIDToActionName((DefineMT.MTType)int.Parse(mRow["MTTypeID"].ToString()), (MyConfig.ChannelType)int.Parse(mRow["ChannelTypeID"].ToString()));
                    mRow["Description"] = ConvertMTTypeIDToDescription((DefineMT.MTType)int.Parse(mRow["MTTypeID"].ToString()), (MyConfig.ChannelType)int.Parse(mRow["ChannelTypeID"].ToString()));
                  
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
