using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using System.Data;
using System.IO;
using System.Reflection;
using MySportMillion.Service;
using MySportMillion.Report;
using MySportMillion.Sub;


namespace MyTool.ReportSync
{
    public class SyncMO
    {
        /// <summary>
        /// Cho biết dừng thread hay không
        /// </summary>
        public static bool StopThread = false;

        /// <summary>
        /// Key của chuỗi kết nối trong config
        /// </summary>
        public string ConnectionKey_Source = "ConnectionKey_Source";
        public string ConnectionKey_Des = "ConnectionKey_Des";

        public int MaxPID = 50;

        /// <summary>
        /// Thời gian delay cho mỗi một lần chạy
        /// </summary>
        public static int SleepSecond
        {
            get
            {
                try
                {
                    int Temp = 1;
                    int.TryParse(MyConfig.GetKeyInConfigFile("SleepSecond"), out Temp);
                    return Temp;
                }
                catch
                {
                    return 1;
                }
            }
        }

        MOLog mMOLog = null;

        RP_MO mRP_MO = null;

        public SyncMO()
        {
            try
            {
                mMOLog = new MOLog(ConnectionKey_Source);

                mRP_MO = new RP_MO(ConnectionKey_Des);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy danh sách report cho các dịch vụ theo ngáy (trong table RP_MO)
        /// </summary>
        /// <param name="ReportDay"></param>
        /// <returns></returns>
        List<RP_MO_Object> Get_RP_MO_ByDate(DateTime ReportDay)
        {
            try
            {
                DataTable mTable = mRP_MO.Select(1, ReportDay.ToString(MyConfig.DateFormat_InsertToDB));
                return RP_MO_Object.Convert(mTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ngày đầu tiên dịch vụ chạy
        /// </summary>
        DateTime FirstServiceDate = new DateTime(2013, 8, 1);

        DateTime BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        DateTime StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(MyConfig.GetKeyInConfigFile("StartDate")))
                {

                    return DateTime.ParseExact(MyConfig.GetKeyInConfigFile("StartDate"), "dd-MM-yyyy", null);
                }
                return DateTime.MinValue;
            }
        }

        DateTime EndDate
        {
            get
            {
                return BeginDate.AddDays(1);
            }
        }

        /// <summary>
        /// ngày đầu tiên của tháng
        /// </summary>
        DateTime FirstDateOfMonth
        {
            get
            {
                return new DateTime(BeginDate.Year, BeginDate.Month, 1, 0, 0, 0);
            }
        }


        /// <summary>
        /// Cập nhật thông tin và danh sách report hiện tại
        /// </summary>
        /// <param name="mList_Current"></param>
        /// <param name="mProType"></param>
        /// <param name="mTable"></param>
        /// <param name="IsSum"></param>
        void UpdateToList(ref List<RP_MO_Object> mList_Current, RP_MO_Object.PropertyType mProType, DataTable mTable, bool IsSum)
        {

            try
            {
                //Lấy thuộc tính cần update thông tin
                FieldInfo mFieldInfo = RP_MO_Object.GetField(mProType);
                if (mFieldInfo == null)
                    return;

                #region Update các trường được tạo từ các trường khác
                if (mProType == RP_MO_Object.PropertyType.MOSuccess)
                {
                    foreach (RP_MO_Object mObj in mList_Current)
                    {
                        mObj.MOSuccess = mObj.MOTotal - mObj.MOFail - mObj.MOInvalid - mObj.MOError;
                    }
                    return;
                }
                #endregion

                foreach (DataRow mRow in mTable.Rows)
                {
                    bool Exist = false;
                    foreach (RP_MO_Object mObj in mList_Current)
                    {
                        if ((int)mRow["PartnerID"] == mObj.PartnerID)
                        {
                            if (IsSum)
                            {
                                //nếu cho phép cộng dồn
                                double SumValue = (double)mFieldInfo.GetValue(mObj);
                                SumValue += double.Parse(mRow["Total"].ToString());

                                //Update giá trị của thuộc tính
                                mFieldInfo.SetValue(mObj, SumValue);
                            }
                            else
                            {
                                mFieldInfo.SetValue(mObj, double.Parse(mRow["Total"].ToString()));
                            }
                            Exist = true;
                            break;
                        }
                    }

                    //Nếu không tồn tại RP_MO_Object thì thêm mới
                    if (!Exist)
                    {
                        RP_MO_Object mObj = new RP_MO_Object();
                        mObj.ReportDay = BeginDate;
                        mObj.PartnerID = (int)mRow["PartnerID"];
                        mFieldInfo.SetValue(mObj, double.Parse(mRow["Total"].ToString()));
                        mList_Current.Add(mObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GetListMTTypeID(RP_MO_Object.PropertyType mProType)
        {
            try
            {

                string ListID = string.Empty;
                switch (mProType)
                {
                    #region MT
                    case RP_MO_Object.PropertyType.MTTotal:
                        ListID = string.Empty;
                        break;
                    case RP_MO_Object.PropertyType.MTFail:
                        ListID = string.Empty;
                        break;
                    #endregion

                    #region MO
                    case RP_MO_Object.PropertyType.MOTotal:
                        // bao gồm các MTTypeID không phải là MO thông báo
                        ListID = "500,501,502,600,601,602,603,604,605,710,715,716,717,718,719,807,808,810";
                        break;
                    case RP_MO_Object.PropertyType.MOFail:
                        ListID = "104,220,303,720";
                        break;
                    case RP_MO_Object.PropertyType.MOInvalid:
                        ListID = "101,700";
                        break;
                    case RP_MO_Object.PropertyType.MOError:
                        ListID = "103,221,304,721";
                        break; 
                    #endregion

                    #region MOReg
                    case RP_MO_Object.PropertyType.MORegTotal:
                        ListID = "200,201,202,203,204,205,220,221,222,223,230,231";
                        break;
                    case RP_MO_Object.PropertyType.MORegSuccess:
                        ListID = "200,201,204,222,223,230,231";
                        break;
                    case RP_MO_Object.PropertyType.MORegFail:
                        ListID = "202,203,220";
                        break;
                    case RP_MO_Object.PropertyType.MORegBlanceTooLow:
                        ListID = "205";
                        break;
                    case RP_MO_Object.PropertyType.MORegError:
                        ListID = "221";
                        break;
                    
                    #endregion

                    #region MODereg
                   
                    case RP_MO_Object.PropertyType.MODeregTotal:
                        ListID = "300,301,302,303,304,305,400";
                        break;
                    case RP_MO_Object.PropertyType.MODeregConfirm:
                        ListID = "302";
                        break;
                    case RP_MO_Object.PropertyType.MODeregSuccess:
                        ListID = "300";
                        break;
                    case RP_MO_Object.PropertyType.MODeregFail:
                        ListID = "301,303,305,400";
                        break;
                    case RP_MO_Object.PropertyType.MODeregError:
                        ListID = "304";
                        break; 
                    #endregion

                    #region MOAnswer
                    case RP_MO_Object.PropertyType.MOAnswerTotal:
                        ListID = "700,701,702,703,704,705,706,707,708,709,711,712,713,714,720,721";
                        break;
                    case RP_MO_Object.PropertyType.MOAnswerSuccess:
                        ListID = "701,702,703,704,705,706,707,708,709";
                        break;
                    case RP_MO_Object.PropertyType.MOAnswerFail:
                        ListID = "713,714,720";
                        break;
                    case RP_MO_Object.PropertyType.MOAnswerInvalid:
                        ListID = "700";
                        break;
                    case RP_MO_Object.PropertyType.MOAnswerError:
                        ListID = "721";
                        break;
                    case RP_MO_Object.PropertyType.MOAnswerOver:
                        ListID = "712";
                        break;
                    case RP_MO_Object.PropertyType.MOAnswerExpire:
                        ListID = "711";
                        break; 
                    #endregion
                }
                return ListID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Run()
        {
            try
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("BAT DAU CHAY CHUONG TRINH SyncMO");

                if (StartDate != DateTime.MinValue)
                {
                    BeginDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 0, 0, 0);
                }

                while (!StopThread)
                {
                    try
                    {
                        if (BeginDate > DateTime.Now)
                        {
                            BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        }
                        //Ngày hiện tại
                        List<RP_MO_Object> mList_Current = new List<RP_MO_Object>();

                        //Ngày hôm qua
                        List<RP_MO_Object> mList_Previous = Get_RP_MO_ByDate(BeginDate.AddDays(-1));

                        //Lấy thuê bao đang sử dụng dịch vụ (Active)
                        DataTable mTable = new DataTable();

                        for (int PID = 0; PID <= MaxPID; PID++)
                        {
                            if (StopThread)
                                break;

                            Console.WriteLine("BeginDate:" + BeginDate.ToString(MyConfig.LongDateFormat) + " || Lay du lieu voi pid = " + PID.ToString());

                            #region MT
                            mTable = mMOLog.Select(4, PID.ToString(),
                                                                    BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                    EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MTTotal, mTable, true); 
                            #endregion

                            #region MO
                            mTable = mMOLog.Select(6, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOTotal),
                                                                                            BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                            EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOTotal, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOFail),
                                                                       BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                       EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOFail, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOInvalid),
                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOInvalid, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOError),
                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOError, mTable, true); 
                            #endregion

                            #region MOReg
                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MORegTotal),
                                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MORegTotal, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MORegSuccess),
                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MORegSuccess, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MORegFail),
                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MORegFail, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MORegBlanceTooLow),
                                                                     BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                     EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MORegBlanceTooLow, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MORegError),
                                                                    BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                    EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MORegError, mTable, true); 
                            #endregion

                            #region MODereg
                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MODeregTotal),
                                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MODeregTotal, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MODeregConfirm),
                                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));

                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MODeregConfirm, mTable, true);


                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MODeregSuccess),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MODeregSuccess, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MODeregFail),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MODeregFail, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MODeregError),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MODeregError, mTable, true); 
                            #endregion

                            #region MOAnswer
                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerTotal),
                                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerTotal, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerSuccess),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerSuccess, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerFail),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerFail, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerInvalid),
                                                                                               BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                               EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerInvalid, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerError),
                                                                                               BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                               EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerError, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerOver),
                                                                                               BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                               EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerOver, mTable, true);

                            mTable = mMOLog.Select(5, PID.ToString(), GetListMTTypeID(RP_MO_Object.PropertyType.MOAnswerExpire),
                                                                                              BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                              EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                            UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOAnswerExpire, mTable, true); 
                            #endregion
                        }

                        //MOSuccess là trường được tính ra từ các trường đã có sẵn
                        UpdateToList(ref mList_Current, RP_MO_Object.PropertyType.MOSuccess, mTable, false);

                        //Insert vào table RP_MO
                        DataSet mSet = mRP_MO.CreateDataSet();
                        DataTable mTable_Insert = mSet.Tables[0];

                        foreach (RP_MO_Object mObj in mList_Current)
                        {
                            mObj.AddNewRow(ref mTable_Insert);
                        }

                        MyConvert.ConvertDateColumnToStringColumn(ref mSet);
                        mRP_MO.Insert(0, mSet.GetXml());

                        BeginDate = BeginDate.AddDays(1);
                    }
                    catch (Exception ex)
                    {
                        MyLogfile.WriteLogError(ex);
                    }

                    if (BeginDate < DateTime.Now)
                        continue;


                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("CHUONG TRINH SE DELAY " + (SleepSecond / 60).ToString() + " phut.");
                    System.Threading.Thread.Sleep(SleepSecond * 1000);
                }
            }

            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }
    }
}
