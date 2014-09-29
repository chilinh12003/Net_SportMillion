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
    public class AnswerLog
    {
         MyExecuteData mExec;
        MyGetData mGet;

        public AnswerLog()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public AnswerLog(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }     

        public int TotalRow(int? Type, string SearchContent, int PID,  int MatchID, DateTime BeginDate,DateTime EndDate)
        {
            try
            {
                string[] mpara = { "Type", "SearchContent", "PID","MatchID", "BeginDate","EndDate","IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(),MatchID.ToString(),BeginDate.ToString(MyConfig.DateFormat_InsertToDB),EndDate.ToString(MyConfig.DateFormat_InsertToDB), true.ToString() };
                return (int)mGet.GetExecuteScalar("Sp_AnswerLog_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int PID,  int MatchID,DateTime BeginDate,DateTime EndDate, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "MatchID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), MatchID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_AnswerLog_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
