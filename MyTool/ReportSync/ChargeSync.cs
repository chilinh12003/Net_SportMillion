using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using System.Data;
using System.IO;
using MySportMillion.Service;
using MySportMillion.Report;
namespace MyTool.ReportSync
{
    public class ChargeSync
    {
        /// <summary>
        /// Cho biết dừng thread hay không
        /// </summary>
        public static bool StopThread = false;

        /// <summary>
        /// Key của chuỗi kết nối trong config
        /// </summary>
        public string ConnectionKey_Source = "SQLConnecton_MTraffic";
        public string ConnectionKey_Des = "SQLConnecton_MTraffic";

        public string PahtXML = "App_Data\\DataSyncVNP.XML";
        /// <summary>
        /// Chứa danh sách giá trị LastUpdate của record cuối cùng của lần chạy cuối cùng, ứng với từng PID
        /// </summary>
        Dictionary<int, string> ListLastUpdate = new Dictionary<int, string>();

        public int MaxPID = 50;

        public int RowCont = 10;
        private string FormatDay = "yyyy-MM-dd";

        /// <summary>
        /// Khoảng thời gian cho mỗi lần lấy dữ liệu
        /// </summary>
        private int IntervalTime
        {
            get {
                string Temp = MyConfig.GetKeyInConfigFile("IntervalTime");
                if(string.IsNullOrEmpty(Temp))
                    return 20;
                else return int.Parse(Temp);
            }
        }

        /// <summary>
        /// chỉ lấy đến thời gian trước thời gian hiện tại là: 15 minute
        /// </summary>
        private int BeforeCurrentTime
        {
            get
            {
                string Temp = MyConfig.GetKeyInConfigFile("BeforeCurrentTime");
                if (string.IsNullOrEmpty(Temp))
                    return 15;
                else return int.Parse(Temp);
            }
        }
        /// <summary>
        /// Thời gian delay cho mỗi một lần chạy
        /// </summary>
        public static int SleepMinute_DataSync
        {
            get
            {
                try
                {
                    int Temp = 1;
                    int.TryParse(MyConfig.GetKeyInConfigFile("SleepMinute_DataSync"), out Temp);
                    return Temp;
                }
                catch
                {
                    return 1;
                }
            }
        }

        DateTime CurrentDate = DateTime.MinValue;
        void SaveInfo()
        {
            try
            {
                DataTable mTable = new DataTable("DataSyncInfo");
                DataColumn col_PID = new DataColumn("PID", typeof(string));
                DataColumn col_LastUpdate = new DataColumn("LastUpdate", typeof(string));
                mTable.Columns.AddRange(new DataColumn[] { col_PID, col_LastUpdate });

                DataSet mSet = new DataSet("DataSet");
                mSet.Tables.Add(mTable);

                foreach (var item in ListLastUpdate)
                {
                    DataRow mRow = mTable.NewRow();
                    mRow["PID"] = item.Key;
                    mRow["LastUpdate"] = item.Value;
                    mTable.Rows.Add(mRow);
                }
                mSet.WriteXml(MyFile.GetFullPathFile(PahtXML));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy thông tin của các report đã được chạy lần cuối cùng
        /// </summary>
        void ReadInfo()
        {
            try
            {
                if (!File.Exists(MyFile.GetFullPathFile(PahtXML)))
                {
                    return;
                }
                DataSet mSet = MyXML.GetXMLData(MyFile.GetFullPathFile(PahtXML));
                if (mSet == null || mSet.Tables.Count < 1)
                    return;
                if (mSet.Tables[0].Rows.Count < 1)
                    return;
                ListLastUpdate.Clear();
                foreach (DataRow mRow in mSet.Tables[0].Rows)
                {
                    int PID = 0;
                    string LastUpdate = mRow["LastUpdate"].ToString();
                    if (int.TryParse(mRow["PID"].ToString(), out PID) && !string.IsNullOrEmpty(LastUpdate))
                    {
                        ListLastUpdate.Add(PID, LastUpdate);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy giá trị ngày nhỏ nhất từ table ChargeLog trong DB theo PID
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DateTime GetMinDate(int PID)
        {
            try
            {
                ChargeLog mChargeLog = new ChargeLog(ConnectionKey_Source);
                DataTable mTable = mChargeLog.Select(7, PID.ToString());
                if (mTable != null && mTable.Rows.Count > 0)
                    return (DateTime)mTable.Rows[0][0];
                else
                    return DateTime.MinValue;
                  
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                return DateTime.MinValue;
            }
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("BAT DAU CHAY CHUONG TRINH");

                ChargeLog mChargeLog = new ChargeLog(ConnectionKey_Source);

                RPChargeByDay mRPChargeByDay = new RPChargeByDay(ConnectionKey_Des);

                ReadInfo();

                while (!StopThread)
                {
                    try
                    {
                        CurrentDate = DateTime.Now;
                        for (int i = 0; i <= MaxPID; i++)
                        {
                            if (StopThread)
                                break;

                            int PID = i;
                            DateTime LastUpdate = DateTime.MinValue;
                            DateTime BeginDate = DateTime.MinValue;

                            if (ListLastUpdate.ContainsKey(PID))
                            {
                                DateTime.TryParseExact(ListLastUpdate[PID], MyConfig.DateFormat_InsertToDB, null, System.Globalization.DateTimeStyles.None, out LastUpdate);
                            }
                            
                            if(LastUpdate == DateTime.MinValue)
                            {
                                LastUpdate = GetMinDate(PID);

                                if (LastUpdate != DateTime.MinValue)
                                    ListLastUpdate.Add(PID, LastUpdate.ToString(MyConfig.DateFormat_InsertToDB));
                            }

                            if (LastUpdate == DateTime.MinValue)
                            {
                                Console.WriteLine("Khong co du lieu cho PID = " + PID.ToString());
                                continue;
                            }

                            while (LastUpdate < CurrentDate.AddMinutes(-BeforeCurrentTime) && !StopThread)
                            {
                                LastUpdate = LastUpdate.AddMilliseconds(1);
                                BeginDate = LastUpdate;
                                LastUpdate = LastUpdate.AddMinutes(IntervalTime);

                                if (LastUpdate > CurrentDate.AddMinutes(-BeforeCurrentTime))
                                {
                                    LastUpdate = CurrentDate.AddMinutes(-BeforeCurrentTime);
                                }

                                ListLastUpdate[PID] = LastUpdate.ToString(MyConfig.DateFormat_InsertToDB);
                                
                                DataTable mTable = mChargeLog.Select(6, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), LastUpdate.ToString(MyConfig.DateFormat_InsertToDB));

                                //MyLogfile.WriteLogData("_Time", "PID:" + PID.ToString() + "|BeginDate:" + BeginDate.ToString(MyConfig.DateFormat_InsertToDB) + "|LastUpdate:" + LastUpdate.ToString(MyConfig.DateFormat_InsertToDB) + "|TotalCount:" + TotalCount.ToString(MyConfig.IntFormat));

                                if (mTable != null && mTable.Rows.Count > 0)
                                {
                                    DataSet mSet_RP = new DataSet("Parent");
                                    DataTable mTable_RP = mTable.Copy();

                                    mTable_RP.TableName = "Child";
                                    mSet_RP.Tables.Add(mTable_RP);
                                    MyConvert.ConvertDateColumnToStringColumn(ref mSet_RP);
                                    try
                                    {
                                        //Update vao database
                                        if (mRPChargeByDay.Sync(0, mSet_RP.GetXml()))
                                        {
                                            SaveInfo();
                                            Console.WriteLine("Cap nhat thanh cong cho PID:" + PID.ToString() + " Trong khoang thoi gian [" + BeginDate.ToString(MyConfig.DateFormat_InsertToDB) + "]-[" + LastUpdate.ToString(MyConfig.DateFormat_InsertToDB) + "]");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cap nhat KHONG thanh cong cho PID:" + PID.ToString() + " Trong khoang thoi gian [" + BeginDate.ToString(MyConfig.DateFormat_InsertToDB) + "]-[" + LastUpdate.ToString(MyConfig.DateFormat_InsertToDB) + "]");
                                            MyLogfile.WriteLogData("NotSaveDB", mSet_RP.GetXml());
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MyLogfile.WriteLogData("ERROR_INSERT_DB_XML", mSet_RP.GetXml());
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Khong co du lieu cho PID:" + PID.ToString() + " Trong khoang thoi gian [" + BeginDate.ToString(MyConfig.DateFormat_InsertToDB) + "]-[" + LastUpdate.ToString(MyConfig.DateFormat_InsertToDB) + "]");
                                }
                            }
                            SaveInfo();
                        }
                    }
                    catch (Exception ex)
                    {
                        MyLogfile.WriteLogError(ex);
                    }
                  
                    SaveInfo();
                          
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("CHUONG TRINH SE DELAY " + SleepMinute_DataSync.ToString() + " phut.");
                    System.Threading.Thread.Sleep(SleepMinute_DataSync * 60 * 1000);
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }

            try
            {
                SaveInfo();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }
    }
}
