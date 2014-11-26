using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using MyUtility;
using MySetting;
using System.Text;
using System.Data;
using MySportMillion;
using MySportMillion.Service;
using MySportMillion.Sub;
using MySportMillion.Report;
using System.ComponentModel;

namespace MyAdmin.Admin_ReportVNP
{
    public class ExportExcelObject
    {
        public enum ExportType
        {
            [DescriptionAttribute("Tất cả")]
            Nothing = 0,
            [DescriptionAttribute("GiaHan_Ngay")]
            GiaHan_Ngay = 1,
            [DescriptionAttribute("GiaHan_Tuan")]
            GiaHan_Tuan = 2,
            [DescriptionAttribute("ThongKeThueBao")]
            TheuBao_Ngay = 3,
            [DescriptionAttribute("ThueBao_Tuan")]
            ThueBao_Tuan = 4,
            [DescriptionAttribute("ThongKeMO")]
            MODangKyHuy_Ngay = 5,
            [DescriptionAttribute("MODangKyHuy_Tuan")]
            MODangKyHuy_Tuan = 6,
            [DescriptionAttribute("MOMT_Ngay")]
            MOMT_Ngay = 7,
            [DescriptionAttribute("MOMT_Tuan")]
            MOMT_Tuan = 8,
            [DescriptionAttribute("MODuDoan_Ngay")]
            MODuDoan_Ngay = 9,
            [DescriptionAttribute("MODuDoan_Tuan")]
            MODuDoan_Tuan = 10,
        }

        public ExportType mExportType = ExportType.Nothing;
        public DateTime BeginDate = DateTime.MinValue;
        public DateTime EndDate = DateTime.MinValue;
        public DateTime RequestDate = DateTime.MinValue;
        public string ChartFileName = string.Empty;

        public string URLChartFileName
        {
            get
            {
                if (string.IsNullOrEmpty(ChartFileName))
                    return string.Empty;

                return MyConfig.Domain + "/u/" + ChartFileName;
            }
        }

        public ExportExcelObject()
        {

        }

       
        public ExportExcelObject(ExportType mExportType, DateTime BeginDate, DateTime EndDate, DateTime RequestDate, string ChartFileName)
        {
            this.mExportType = mExportType;
            this.BeginDate = BeginDate;
            this.EndDate = EndDate;
            this.RequestDate = RequestDate;
            this.ChartFileName = ChartFileName;
        }
        public ExportExcelObject(string Para)
        {
            try
            {
                Decrypt(Para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsNull
        {
            get
            {
                if (mExportType == ExportType.Nothing ||
                   BeginDate == DateTime.MinValue || EndDate == DateTime.MinValue || RequestDate == DateTime.MinValue)
                {
                    return true;
                }
                return false;

            }
        }

        /// <summary>
        /// Cho biết thông tin request này đã hết hạn hay chưa
        /// </summary>
        public bool IsExpire
        {
            get
            {
                try
                {
                    if (IsNull)
                        return true;

                    if (!Member.IsLogined())
                        return true;

                    TimeSpan RequestInterval = DateTime.Now - RequestDate;
                    if (RequestInterval.TotalMinutes > MyCurrent.CurrentPage.Session.Timeout)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private string DateFormat = "yyyyMMddHHmmss";

        public string Encrypt()
        {
            if (IsNull)
            {
                return string.Empty;
            }

            string Data = MySecurity.AES.Encrypt(((int)mExportType).ToString() + "|" + BeginDate.ToString(DateFormat) + "|" + EndDate.ToString(DateFormat) + "|" + RequestDate.ToString(DateFormat) + "|" + ChartFileName, AdminSetting.SpecialKey);
            return Data;
        }
        public ExportExcelObject Decrypt(string Para)
        {
            try
            {
                ExportExcelObject ReturnObj = new ExportExcelObject();
                string Decode = MySecurity.AES.Decrypt(Para, AdminSetting.SpecialKey);

                if (string.IsNullOrEmpty(Decode))
                    return ReturnObj;

                string[] arr = Decode.Split('|');
                if (arr.Length != 5)
                    return ReturnObj;

                mExportType = (ExportType)(int.Parse(arr[0]));
                DateTime.TryParseExact(arr[1], DateFormat, null, System.Globalization.DateTimeStyles.None, out BeginDate);
                DateTime.TryParseExact(arr[2], DateFormat, null, System.Globalization.DateTimeStyles.None, out EndDate);
                DateTime.TryParseExact(arr[3], DateFormat, null, System.Globalization.DateTimeStyles.None, out RequestDate);
                ChartFileName = arr[4];
                return this;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    /// Summary description for ExportExcel
    /// </summary>
    public class ExportExcel : IHttpHandler, IRequiresSessionState
    {
        string Para = string.Empty;
        ExportExcelObject mEPObject = new ExportExcelObject();

        public void ProcessRequest(HttpContext context)
        {
            
            try
            {
                if (!string.IsNullOrEmpty(context.Request.QueryString["Para"]))
                {
                    Para = context.Request.QueryString["Para"];
                    mEPObject = new ExportExcelObject(Para);
                }
                context.Response.ContentType = "application/ms-excel";
                context.Response.AddHeader("content-disposition", "attachment; filename=" + MyEnum.StringValueOf(mEPObject.mExportType) + ".xls");
                context.Response.Write(BuildExcel().ToString());

            }
            catch(Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                context.Response.Write(GetData("Defaul", "Yeu cau export excel khong hop le").ToString());
            }
        }

        public StringBuilder BuildExcel()
        {
            try
            {
                RP_Sub mRP_Sub = new RP_Sub();
                RP_MO mRP_MO = new RP_MO();

                if (mEPObject.IsNull || mEPObject.IsExpire)
                {
                    return GetData("Defaul", "Yeu cau export excel khong hop le");
                }

                DataTable mTable = new DataTable();
                string Template = string.Empty;
                string Template_Repeat = string.Empty;

                StringBuilder mBuilder_TR = new StringBuilder(string.Empty);

                switch (mEPObject.mExportType)
                {
                    case ExportExcelObject.ExportType.MODuDoan_Ngay:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOAnswerDay.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOAnswerDay_Repeat.html"));

                        mTable = mRP_MO.Search_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);

                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {((DateTime)mRow["ReportDay"]).ToString(MyConfig.ShortDateFormat),
                              ((double)mRow["MOAnswerTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerInvalid"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerOver"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerExpire"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerError"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.MODuDoan_Tuan:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOAnswerWeek.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOAnswerWeek_Repeat.html"));

                        if (!AdminSetting.ModifyDateByWeek(ref mEPObject.BeginDate, ref mEPObject.EndDate))
                        {
                            return GetData("Defaul", "Yeu cau export excel khong hop le");
                        }

                        mTable = mRP_MO.Search_Week_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);


                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {mRow["ReportWeek"].ToString()+"/" +mRow["ReportYear"].ToString() +
                                                   "("+AdminSetting.GetIntervalDay((int)mRow["ReportYear"],(int)mRow["ReportWeek"])+")",
                              ((double)mRow["MOAnswerTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerInvalid"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerOver"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerExpire"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerError"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOAnswerFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.MOMT_Ngay:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOMTDay.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOMTDay_Repeat.html"));

                        mTable = mRP_MO.Search_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);

                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {((DateTime)mRow["ReportDay"]).ToString(MyConfig.ShortDateFormat),
                              ((double)mRow["MTTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MTFail"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOInvalid"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOError"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.MOMT_Tuan:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOMTWeek.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MOMTWeek_Repeat.html"));

                        if (!AdminSetting.ModifyDateByWeek(ref mEPObject.BeginDate, ref mEPObject.EndDate))
                        {
                            return GetData("Defaul", "Yeu cau export excel khong hop le");
                        }

                        mTable = mRP_MO.Search_Week_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);


                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {mRow["ReportWeek"].ToString()+"/" +mRow["ReportYear"].ToString() +
                                                   "("+AdminSetting.GetIntervalDay((int)mRow["ReportYear"],(int)mRow["ReportWeek"])+")",
                              ((double)mRow["MTTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MTFail"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOInvalid"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOError"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["MOFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.MODangKyHuy_Ngay:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MORegDay.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MORegDay_Repeat.html"));

                        mTable = mRP_MO.Search_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);

                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {((DateTime)mRow["ReportDay"]).ToString(MyConfig.ShortDateFormat),
                              ((double)mRow["MTTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MTFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOInvalid"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOError"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            (((double)mRow["MOSuccess"]/(double)mRow["MOTotal"])*100 ).ToString(MyUtility.MyConfig.DoubleFormat),

                            ((double)mRow["MORegTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegBlanceTooLow"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegError"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            (((double)mRow["MORegSuccess"]/(double)mRow["MORegTotal"]) *100 ).ToString(MyUtility.MyConfig.DoubleFormat),

                            ((double)mRow["MODeregTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregConfirm"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregError"]).ToString(MyUtility.MyConfig.IntFormat),
                            (((double)mRow["MODeregSuccess"]/(double)mRow["MODeregConfirm"]) *100 ).ToString(MyUtility.MyConfig.DoubleFormat),
                        
                            ((double)mRow["MOAnswerTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOAnswerSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOAnswerInvalid"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOAnswerOver"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOAnswerExpire"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOAnswerError"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MOAnswerFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            (((double)mRow["MOAnswerSuccess"]/(double)mRow["MOAnswerTotal"]) *100 ).ToString(MyUtility.MyConfig.DoubleFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.MODangKyHuy_Tuan:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MORegWeek.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/MORegWeek_Repeat.html"));

                        if (!AdminSetting.ModifyDateByWeek(ref mEPObject.BeginDate, ref mEPObject.EndDate))
                        {
                            return GetData("Defaul", "Yeu cau export excel khong hop le");
                        }

                        mTable = mRP_MO.Search_Week_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);


                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {mRow["ReportWeek"].ToString()+"/" +mRow["ReportYear"].ToString() +
                                                   "("+AdminSetting.GetIntervalDay((int)mRow["ReportYear"],(int)mRow["ReportWeek"])+")",
                              ((double)mRow["MORegTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegBlanceTooLow"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegError"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MORegFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregConfirm"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregFail"]).ToString(MyUtility.MyConfig.IntFormat),
                            ((double)mRow["MODeregError"]).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.GiaHan_Ngay:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/RenewDay.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/RenewDay_Repeat.html"));

                        mTable = mRP_Sub.Search_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);

                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {((DateTime)mRow["ReportDay"]).ToString(MyConfig.ShortDateFormat),
                               ((double)mRow["RenewTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["RenewSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["RenewFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["RenewRate"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["SaleReg"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["SaleRenew"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["SaleReg"] + (double)mRow["SaleRenew"] ).ToString(MyUtility.MyConfig.IntFormat)
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.GiaHan_Tuan:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/RenewWeek.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/RenewWeek_Repeat.html"));

                        if (!AdminSetting.ModifyDateByWeek(ref mEPObject.BeginDate, ref mEPObject.EndDate))
                        {
                            return GetData("Defaul", "Yeu cau export excel khong hop le");
                        }

                        mTable = mRP_Sub.Search_Week_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);


                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {mRow["ReportWeek"].ToString()+"/" +mRow["ReportYear"].ToString() +
                                                   "("+AdminSetting.GetIntervalDay((int)mRow["ReportYear"],(int)mRow["ReportWeek"])+")",
                               ((double)mRow["RenewTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["RenewSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["RenewFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["RenewRate"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["SaleReg"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["SaleRenew"]).ToString(MyUtility.MyConfig.IntFormat),
                               ((double)mRow["SaleReg"] + (double)mRow["SaleRenew"] ).ToString(MyUtility.MyConfig.IntFormat)
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                    case ExportExcelObject.ExportType.TheuBao_Ngay:
                         #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/SubDay.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/SubDay_Repeat.html"));                     

                        mTable = mRP_Sub.Search_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);
                        
                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {((DateTime)mRow["ReportDay"]).ToString(MyConfig.ShortDateFormat),
                               ((double)mRow["SubTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubActive"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubNew"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubSMS"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubWAP"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubOther"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubFail"]).ToString(MyUtility.MyConfig.IntFormat),
                                (((double)mRow["SubNew"]/((double)mRow["SubNew"]+  (double)mRow["SubFail"])) *100 ).ToString(MyUtility.MyConfig.DoubleFormat),
                                ((double)mRow["UnsubTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubNew"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubSelf"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubExtend"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubOther"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubFail"]).ToString(MyUtility.MyConfig.IntFormat),
                                (((double)mRow["UnsubNew"]/((double)mRow["UnsubNew"]+   (double)mRow["UnsubFail"])) *100 ).ToString(MyUtility.MyConfig.DoubleFormat),
                                ((double)mRow["RenewTotal"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["RenewSuccess"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["RenewFail"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["RenewRate"]).ToString(MyUtility.MyConfig.DoubleFormat),
                                ((double)mRow["SaleReg"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SaleRenew"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SaleReg"] + (double)mRow["SaleRenew"] ).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion
                        break;
                    case ExportExcelObject.ExportType.ThueBao_Tuan:

                        #region MyRegion
                        Template = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/SubWeek.html"));
                        Template_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("~/Templates/SubWeek_Repeat.html"));

                        if (!AdminSetting.ModifyDateByWeek(ref mEPObject.BeginDate, ref mEPObject.EndDate))
                        {
                            return GetData("Defaul", "Yeu cau export excel khong hop le");
                        }

                        mTable = mRP_Sub.Search_Week_VNP(0, 0, 10000, mEPObject.BeginDate, mEPObject.EndDate, string.Empty);

                        
                        foreach (DataRow mRow in mTable.Rows)
                        {
                            string[] arr = {mRow["ReportWeek"].ToString()+"/" +mRow["ReportYear"].ToString() +
                                                   "("+AdminSetting.GetIntervalDay((int)mRow["ReportYear"],(int)mRow["ReportWeek"])+")",
                                ((double)mRow["SubNew"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubSMS"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubWAP"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubOther"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["SubFail"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubNew"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubSelf"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubExtend"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubOther"]).ToString(MyUtility.MyConfig.IntFormat),
                                ((double)mRow["UnsubFail"]).ToString(MyUtility.MyConfig.IntFormat),
                               };
                            mBuilder_TR.Append(string.Format(Template_Repeat, arr));

                        }
                        #endregion

                        break;
                }

                if(mBuilder_TR.Length == 0)
                {
                    return GetData("Defaul", "Yeu cau export excel khong hop le");
                }

                return GetData(MyEnum.StringValueOf(mEPObject.mExportType), string.Format(Template, new string[] { mBuilder_TR.ToString(), mEPObject.URLChartFileName }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StringBuilder GetData(string WorkSheetName, string Data)
        {
            StringBuilder mBuilder = new StringBuilder(string.Empty);

            mBuilder.Append("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            mBuilder.Append("<head>");
            mBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\">");
            mBuilder.Append("<!--[if gte mso 9]>");
            mBuilder.Append("<xml>");
            mBuilder.Append("<x:ExcelWorkbook>");
            mBuilder.Append("<x:ExcelWorksheets>");
            mBuilder.Append("<x:ExcelWorksheet>");
            //this line names the worksheet
            mBuilder.Append("<x:Name>" + WorkSheetName + "</x:Name>");
            mBuilder.Append("<x:WorksheetOptions>");
            //these 2 lines are what works the magic
            mBuilder.Append("<x:Panes>");
            mBuilder.Append("</x:Panes>");
            mBuilder.Append("</x:WorksheetOptions>");
            mBuilder.Append("</x:ExcelWorksheet>");
            mBuilder.Append("</x:ExcelWorksheets>");
            mBuilder.Append("</x:ExcelWorkbook>");
            mBuilder.Append("</xml>");
            mBuilder.Append("<![endif]-->");
            mBuilder.Append("</head>");
            mBuilder.Append("<body>");
            mBuilder.Append(Data);
            mBuilder.Append("</body>");
            mBuilder.Append("</html>");
            return mBuilder;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}