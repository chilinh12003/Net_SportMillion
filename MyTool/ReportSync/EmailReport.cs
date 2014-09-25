using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using System.Data;
using System.IO;
using MySportMillion.Service;
using MySportMillion.Report;
using MySportMillion.Sub;
using System.Web;
namespace MyTool.ReportSync
{
    public class EmailReport
    {
        /// <summary>
        /// Key của chuỗi kết nối trong config
        /// </summary>
        public string ConnectionKey = "SQLConnecton_SportMillion";

        public string EmailAccount = string.Empty;
        public string Password = string.Empty;
        public string EmailReceiveList = string.Empty;
        public string Subject = string.Empty;

        public string Format = string.Empty;
        public string Format_TR_Repeat = string.Empty;

        public DataTable TableTemplate = new DataTable();

        public string FormatDay = "dd-MM-yyyy";
        public string FormatDay_DB = "yyyy-MM-dd";

        public DateTime BeginDay = DateTime.MinValue;
        public void Init()
        {
            try
            {
                EmailAccount = MyConfig.GetKeyInConfigFile("EmailReport_EmailAccount");
                Password = MyConfig.GetKeyInConfigFile("EmailReport_Password");
                EmailReceiveList = MyConfig.GetKeyInConfigFile("EmailReport_EmailReceiveList");
                Subject = MyConfig.GetKeyInConfigFile("EmailReport_Subject");

                string Temp = MyConfig.GetKeyInConfigFile("EmailReport_BeginDay");
                if (!string.IsNullOrEmpty(Temp))
                {
                    DateTime.TryParseExact(Temp, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out BeginDay);
                }

                Format = MyFile.ReadFile(MyFile.GetFullPathFile("\\Templates\\ReportByDay.htm"));
                Format_TR_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("\\Templates\\ReportByDay_TR_Repeat.htm"));
              
                TableTemplate = CreateTableTemplate();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Tạo table mẫu
        /// </summary>
        /// <returns></returns>
        public DataTable CreateTableTemplate()
        {
            try
            {
                DataTable mTable = new DataTable("Child");
                DataColumn col_ReportDay = new DataColumn("ReportDay", typeof(DateTime));
                DataColumn col_TotalSub = new DataColumn("TotalSub", typeof(int));
                DataColumn col_NewReg = new DataColumn("NewReg", typeof(int));
                DataColumn col_NewDereg = new DataColumn("NewDereg", typeof(int));
                DataColumn col_TotalUnSub = new DataColumn("TotalUnSub", typeof(int));
                DataColumn col_TotalCharge = new DataColumn("TotalCharge", typeof(int));
                DataColumn col_ChargeRate = new DataColumn("ChargeRate", typeof(double));
                DataColumn col_TotalMoney = new DataColumn("TotalMoney", typeof(double));

                mTable.Columns.AddRange(new DataColumn[] { col_ReportDay, col_TotalSub, col_NewReg, col_NewDereg, col_TotalUnSub, col_TotalCharge, col_ChargeRate, col_TotalMoney });
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Lấy dữ liệu từ file XML
        /// </summary>
        /// <param name="mDate"></param>
        /// <returns></returns>
        public DataSet GetDataFromXML(DateTime ReportDay)
        {
            try
            {
                DataSet mSet = new DataSet();
                string FileName = "ByMonth_" + ReportDay.ToString("yyyyMM") + ".xml";
                string FullPath = MyFile.GetFullPathFile("\\App_Data\\" + FileName);
                FileInfo mFile = new FileInfo(FullPath);

                DataSet mSet_Return = new DataSet("Parent");
                DataTable mTable = TableTemplate.Clone();
                mSet_Return.Tables.Add(mTable);

                if (!Directory.Exists(mFile.DirectoryName))
                {
                    System.IO.Directory.CreateDirectory(mFile.DirectoryName);
                }

                if (!File.Exists(FullPath))
                {
                    //File.Create(FullPath);
                    return mSet_Return;
                }

                mSet = MyXML.GetXMLData(FullPath);

                if (mSet == null || mSet.Tables.Count < 1)
                    return mSet_Return;

                foreach (DataRow mRow in mSet.Tables[0].Rows)
                {
                    DataRow mNewRow = mTable.NewRow();

                    foreach (DataColumn mCol in mTable.Columns)
                    {
                        if (!mSet.Tables[0].Columns.Contains(mCol.ColumnName))
                        {
                            continue;
                        }
                        mNewRow[mCol.ColumnName] = mRow[mCol.ColumnName].ToString();
                    }
                    mTable.Rows.Add(mNewRow);
                }

                return mSet_Return;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDataFromDBByDay(DateTime ReportDay)
        {
            try
            {
                DataTable Return_Table = TableTemplate.Clone();

                Subscriber mSub = new Subscriber(ConnectionKey);
                UnSubscriber mUnSub = new UnSubscriber(ConnectionKey);
                RPChargeByDay mReportCharge = new RPChargeByDay(ConnectionKey);

                int TotalSub = 0;
                int TotalUnSub = 0;
                int NewReg = 0;
                int NewDereg = 0;
                int TotalCharge = 0;
                double ChargeRate = 0;
                double TotalMoney = 0;

                DataTable mTable_TotalSub = mSub.Search_Count(0, string.Empty);
                if (mTable_TotalSub.Rows.Count > 0)
                {
                    int.TryParse(mTable_TotalSub.Rows[0][0].ToString(), out TotalSub);
                }
                DataTable mTable_TotalUnSub = mUnSub.Search_Count(0, string.Empty);
                if (mTable_TotalUnSub.Rows.Count > 0)
                {
                    int.TryParse(mTable_TotalUnSub.Rows[0][0].ToString(), out TotalUnSub);
                }
                //Lấy đăng ký mới
                DataTable mTable_Reg = mReportCharge.Select(2, ReportDay.ToString(FormatDay_DB), ((int)ChargeLog.ChargeType.REG_DAILY).ToString(), ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString());
                if (mTable_Reg.Rows.Count > 0)
                {
                    int.TryParse(mTable_Reg.Compute("SUM(TotalCount)", string.Empty).ToString(), out NewReg);
                }
                //Lấy số lượng Hủy trong ngày
                DataTable mTable_Dereg = mReportCharge.Select(2, ReportDay.ToString(FormatDay_DB), ((int)ChargeLog.ChargeType.UNREG_DAILY).ToString(), ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString());
                if (mTable_Dereg.Rows.Count > 0)
                {
                    int.TryParse(mTable_Dereg.Compute("SUM(TotalCount)", string.Empty).ToString(), out NewDereg);
                }
                //Charge thành công
                DataTable mTable_Renew = mReportCharge.Select(2, ReportDay.ToString(FormatDay_DB), ((int)ChargeLog.ChargeType.RENEW_DAILY).ToString(), ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString());
                if (mTable_Renew.Rows.Count > 0)
                {
                    int.TryParse(mTable_Renew.Compute("SUM(TotalCount)", string.Empty).ToString(), out TotalCharge);
                    ChargeRate = (double)TotalCharge / (double)TotalSub * 100;
                    foreach (DataRow mRow_Renew in mTable_Renew.Rows)
                    {
                        TotalMoney += int.Parse(mRow_Renew["TotalCount"].ToString()) * int.Parse(mRow_Renew["Price"].ToString());
                    }
                }

                DataRow mRow = Return_Table.NewRow();
                mRow["ReportDay"] = ReportDay;
                mRow["TotalSub"] = TotalSub;
                mRow["NewReg"] = NewReg;
                mRow["NewDereg"] = NewDereg;
                mRow["TotalUnSub"] = TotalUnSub;
                mRow["TotalCharge"] = TotalCharge;
                mRow["ChargeRate"] = ChargeRate;
                mRow["TotalMoney"] = TotalMoney;

                Return_Table.Rows.Add(mRow);
                return Return_Table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Lấy dữ liệu từ database
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataFromDB(DateTime ReportDay)
        {
            try
            {
                DataTable Return_Table = TableTemplate.Clone();
                if (BeginDay != DateTime.MinValue)
                {
                    BeginDay = new DateTime(BeginDay.Year,BeginDay.Month,BeginDay.Day,0,0,0);
                    DateTime EndDay = new DateTime(ReportDay.Year,ReportDay.Month,ReportDay.Day,0,0,0);
                    while (BeginDay <= EndDay)
                    {
                        DataTable mTable_Temp = GetDataFromDBByDay(BeginDay);

                        if (mTable_Temp != null && mTable_Temp.Rows.Count > 0)
                        {
                            Return_Table.DefaultView.RowFilter = " ReportDay = '" + mTable_Temp.Rows[0]["ReportDay"].ToString() + "'";
                            if (Return_Table.DefaultView.Count > 0)
                            {
                                Return_Table.DefaultView[0].Delete();
                                Return_Table.AcceptChanges();
                                Return_Table.DefaultView.RowFilter = string.Empty;
                               
                            }
                            Return_Table.ImportRow(mTable_Temp.Rows[0]);
                        }
                        BeginDay = BeginDay.AddDays(1);
                    }
                }
                else
                {
                    Return_Table = GetDataFromDBByDay(ReportDay);
                }
                return Return_Table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy dữ liệu trong file XML và trong DB, đồng thời lưu data đã lấy xuống XML
        /// </summary>
        /// <param name="mDate"></param>
        /// <returns></returns>
        private DataTable GetFullData(DateTime ReportDay)
        {
            try
            {
                DataSet mSet = GetDataFromXML(ReportDay);

                DataTable mTable = GetDataFromDB(ReportDay);

                if (mSet == null || mSet.Tables.Count < 1 || mSet.Tables[0].Rows.Count < 1)
                {
                    mSet = new DataSet();

                    mSet.DataSetName = "Parent";
                    mSet.Tables.Add(mTable);
                }
                else
                {
                    for (int i = mSet.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow mRow_Old = mSet.Tables[0].Rows[i];

                        mTable.DefaultView.RowFilter = " ReportDay = '" + mRow_Old["ReportDay"].ToString() + "'";
                        //nếu dòng đã tồn tại thì cập nhật thêm vào.
                        if (mTable.DefaultView.Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            DataRow mNewRow = mTable.NewRow();
                            foreach (DataColumn mCol in mTable.Columns)
                            {
                                if (!mSet.Tables[0].Columns.Contains(mCol.ColumnName))
                                {
                                    continue;
                                }
                                mNewRow[mCol.ColumnName] = mRow_Old[mCol.ColumnName].ToString();
                            }
                            int index = 0;
                            if (mTable.Rows.Count > 0)
                                index = mTable.Rows.Count;

                            mTable.Rows.InsertAt(mNewRow, index);
                        }
                    }
                    mTable.DefaultView.RowFilter = string.Empty;
                    mSet = new DataSet("Parent");
                    DataTable tbl_Save = mTable.Clone();

                    for (int j = mTable.Rows.Count - 1; j >= 0; j--)
                    {
                        tbl_Save.ImportRow(mTable.Rows[j]);
                    }

                    mSet.Tables.Add(tbl_Save);
                }


                SaveDataFromXML(mSet, ReportDay);

                return mTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lưu dữ liệu đã gửi email xuống file xml
        /// </summary>
        /// <param name="mSet"></param>
        /// <param name="mDate"></param>
        public void SaveDataFromXML(DataSet mSet, DateTime ReportDay)
        {
            try
            {
                string FileName = "ByMonth_" + ReportDay.ToString("yyyyMM") + ".xml";
                string FullPath = MyFile.GetFullPathFile("\\App_Data\\" + FileName);

                FileInfo mFile = new FileInfo(FullPath);
                if (!Directory.Exists(mFile.DirectoryName))
                {
                    System.IO.Directory.CreateDirectory(mFile.DirectoryName);
                }            
                System.Security.AccessControl.FileSecurity mSec = new System.Security.AccessControl.FileSecurity();

                mSet.WriteXml(FullPath);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BuildEmail(DateTime ReportDay)
        {
            try
            {
                StringBuilder mBuild_TR = new StringBuilder(string.Empty);

                DataTable mFullData = GetFullData(ReportDay);
                if (mFullData == null || mFullData.Rows.Count < 1)
                {
                    return string.Empty;
                }
                mFullData.DefaultView.Sort = "ReportDay DESC";
                double TotalMoney = 0;
                foreach (DataRowView mRow in mFullData.DefaultView)
                {
                    if (mRow["TotalMoney"] != DBNull.Value)
                    {
                        TotalMoney += int.Parse(mRow["TotalMoney"].ToString());
                    }
                    StringBuilder mBuild_TD = new StringBuilder(string.Empty);
                    mBuild_TR.Append(string.Format(Format_TR_Repeat, 
                                                    new string[] {  ((DateTime)mRow["ReportDay"]).ToString(FormatDay),
                                                                    int.Parse( mRow["TotalSub"].ToString()).ToString(MyConfig.IntFormat),
                                                                    int.Parse(  mRow["NewReg"].ToString()).ToString(MyConfig.IntFormat),
                                                                    int.Parse( mRow["NewDereg"].ToString()).ToString(MyConfig.IntFormat),
                                                                    int.Parse( mRow["TotalUnSub"].ToString()).ToString(MyConfig.IntFormat),
                                                                    int.Parse( mRow["TotalCharge"].ToString()).ToString(MyConfig.IntFormat),
                                                                    double.Parse( mRow["ChargeRate"].ToString()).ToString(MyConfig.DoubleFormat),
                                                                    double.Parse( mRow["TotalMoney"].ToString()).ToString(MyConfig.IntFormat)
                                                                }));
                }
                string Time = ReportDay.ToString(FormatDay) + " LÚC " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

                return string.Format(Format, Time, mBuild_TR.ToString(), TotalMoney.ToString(MyConfig.DoubleFormat));
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                throw ex;
            }
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("Bat dau chay chuong trinh");
                Console.WriteLine("Lay thong tin config, va khoi tao");
                Init();

                string pSubject = Subject + DateTime.Now.ToString("dd-MM-yyyy") + " LÚC " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;

                Console.WriteLine("Build Noi dung Email");
                DateTime ReportDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                string Body = BuildEmail(ReportDay);
                Console.WriteLine("Bat dau gui email...");

                bool Result = MySendEmail.SendEmail_Google_New(EmailAccount, Password, EmailReceiveList, pSubject, Body, string.Empty);

                MyLogfile.LogEmail(Subject, Body, EmailAccount, Password);

                if (!Result)
                {
                    Console.WriteLine("---GUI EMAIL KHONG THANH CONG----");
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false);
                Console.WriteLine("---CO LOI XAY RA----");
            }
        }
    }
}
