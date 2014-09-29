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
    public class SubCode
    {
         MyExecuteData mExec;
        MyGetData mGet;

        public SubCode()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public SubCode(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_SubCode_Select", mPara, mValue);
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


        public int TotalRow(int? Type, string SearchContent, int PID, string MSISDN, int MatchID)
        {
            try
            {
                string[] mpara = { "Type", "SearchContent", "PID","MSISDN","MatchID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(),MSISDN,MatchID.ToString(), true.ToString() };
                return (int)mGet.GetExecuteScalar("Sp_SubCode_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, string MSISDN, int MatchID, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "MSISDN", "MatchID", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), MSISDN, MatchID.ToString(), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_SubCode_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
