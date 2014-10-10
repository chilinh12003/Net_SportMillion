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
    public class DefineMT
    {

        public enum MTType
        {
            Default = 100, Invalid = 101, Help = 102, SystemError = 103, Fail = 104,

            // -----ĐĂNG KÝ DỊCH VỤ
            #region MyRegion
            /// <summary>
            /// Đăng ký mới thành công
            /// </summary>
            RegNewSuccess = 200,

            /// <summary>
            /// Đăng ký lai thành công và miễn phí
            /// </summary>
            RegAgainSuccessFree = 201,
            /// <summary>
            /// Đăng ký lại thành công không miễn phí  = đăng ký lại nhưng hết thời
            /// gian khuyến mại
            /// </summary>
            RegAgainSuccessNotFree = 202,

            /// <summary>
            /// Đăng ký rồi nhưng lại đăng ký tiếp vần còn trong thời gian khuyến mại
            /// </summary>
            RegRepeatFree = 203,

            /// <summary>
            /// Đắng ký lặp trong thời gian hết khuyến mại
            /// </summary>
            RegRepeatNotFree = 204,

            /// <summary>
            /// Đăng ký nhưng tải khoản khách hàng không đủ tiền
            /// </summary>
            RegNotEnoughMoney = 205,
            /// <summary>
            /// Đăng ký không thành công
            /// </summary>
            RegFail = 220,

            /// <summary>
            /// DK nhưng hệ thống bị lỗi
            /// </summary>
            RegSystemError = 221,


            /// <summary>
            /// Đăng ký từ CCOS của vinaphone và được miễn phí chu kỳ cước đầu
            /// </summary>
            RegCCOSSuccessFree = 222,

            /// <summary>
            /// Đăng ký từ CCOS của vinaphone và không được miễn phí 
            /// </summary>
            RegCCOSSuccessNotFree = 223,
            #endregion

            // -----HỦY DỊCH VỤ
            #region MyRegion
            /// <summary>
            /// Hủy thành công dịch vụ
            /// </summary>
            DeregSuccess = 300,

            /// <summary>
            /// Huy khi mà chưa đăng ký dịch vụ
            /// </summary>
            DeregNotRegister = 301,

            /// <summary>
            /// Xác nhận hành động hủy
            /// </summary>
            DeregConfirm = 302,

            /// <summary>
            /// Hủy không thành công do lỗi hệ thống...
            /// </summary>
            DeregFail = 303,

            DeregSystemError = 304,

            /// <summary>
            /// Gửi Y trong khi chưa gửi Confirm trước đó
            /// </summary>
            DeregNotSendConfirm = 305,

            /// <summary>
            /// Hủy khi gia hạn không thành công
            /// </summary>
            ExtendDereg = 400,
            #endregion

            // -----CAC TIN NHẮN VỀ THÔNG BÁO----------------
            #region MyRegion
            /// <summary>
            /// Thông báo về điểm
            /// </summary>
            NotifyMark = 500,

            /// <summary>
            /// Thông báo khách hàng được miễn phí 1 ngày khi đăng ký lần
            /// đầu
            /// </summary>
            NotifyPromotion = 501,

            /// <summary>
            /// Thông báo hướng dẫn sử dụng dịch vụ
            /// </summary>
            NotifyGuide = 502,
            #endregion

            // -----HƯỚNG DẪN SỬ DỤNG DV
            /// <summary>
            /// Các tin nhắn phục vụ cho keyword DD
            /// </summary>
            Guide = 600, GuideKQ = 601, GuideBT = 602, GuideTS = 603, GuideGB = 604, GuideTV = 605, GuideMaxMO = 606,

            // -----TRẢ LỜI
            #region MyRegion
            /// <summary>
            /// Khách hàng gửi câu trả lời không giá trị VD: KQ hoac BT mà ko
            /// có giá trị theo sau
            /// </summary>
            AnswerInvalid = 700, AnswerKQ1 = 701, AnswerKQ2 = 702, AnswerKQ3 = 703,

            AnswerBT = 704,

            AnswerTS = 705,

            AnswerGB1 = 706, AnswerGB2 = 707, AnswerGB3 = 708,

            AnswerTV = 709,

            /// <summary>
            /// Tin nhắn dự đoán cuối cùng  = tin nhắn thứ 10
            /// </summary>
            AnswerFinal = 710,

            /// <summary>
            /// Gửi tin nhắn khi thời gian dự đoán đã hết
            /// </summary>
            AnswerExpire = 711,

            /// <summary>
            /// Khi nhắn tin vượt quá 10 tin  = tin nhắn thứ 11 trở đi
            /// </summary>
            AnswerOver = 712,

            /// <summary>
            /// Trả lời khi tài khoảng không thể trừ tiền được
            /// </summary>
            AnswerNotExtend = 713,

            /// <summary>
            /// Trả lời khi chưa đăng ký
            /// </summary>
            AnswerNotReg = 714,

            AnswerGuideKQ = 715,

            AnswerGuideBT = 716,

            AnswerGuideTS = 717,

            AnswerGuideGB = 718,

            AnswerGuideTV = 719,

            AnswerFail = 720, AnswerSystemError = 721,
            #endregion

            // -----CÁC TRƯỜNG HỢP KHÁC
            #region MyRegion
            /// <summary>
            /// Tra cứu mã dữ thưởng
            /// </summary>
            ConsultCode = 800,

            /// <summary>
            /// Tra cứu trận đấu
            /// </summary>
            ConsultMatch = 801,

            /// <summary>
            /// Ngừng nhận bản tin thông báo
            /// </summary>
            StopRemider = 802,
            /// <summary>
            /// Tiếp tục nhận bản tin thông báo
            /// </summary>
            StartRemider = 803,
            /// <summary>
            /// Chưa đăng ký đã tra cứu MDT
            /// </summary>
            ConsultCodeNotReg = 804,

            /// <summary>
            /// Tra cức khi thuê bao đang retry charge
            /// </summary>
            ConsultCodeNotExtend = 805,

            /// <summary>
            /// Hủy, đăng ký nhận tin khi mà chưa đăng ký
            /// </summary>
            RemiderNotReg = 806,

            /// <summary>
            /// Thông báo kết quả dự đoán sau khi trận đấu kết thúc
            /// </summary>
            NotifyResult = 807,

            PushMT = 808,

            GetOTPSuccess = 809,

            PushMTReminder = 810,
            #endregion

        }
        MyExecuteData mExec;
        MyGetData mGet;

        public DefineMT()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public DefineMT(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_DefineMT_Select", mPara, mValue);
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
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 1: Lấy chi tiết 1 Record (Para_1 = DefintMTID)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_DefineMT_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_DefineMT_Insert", mpara, mValue) > 0)
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

        public bool Delete(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_DefineMT_Delete", mpara, mValue) > 0)
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

        public bool Update(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_DefineMT_Update", mpara, mValue) > 0)
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

        public bool Active(int Type, bool IsActive, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "IsActive", "XMLContent" };
                string[] mValue = { Type.ToString(), IsActive.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_DefineMT_Active", mpara, mValue) > 0)
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

        public int TotalRow(int? Type, string SearchContent, int MTTypeID, bool? IsActive)
        {
            try
            {
                string[] mPara = { "Type", "SearchContent", "MTTypeID", "IsActive", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, MTTypeID.ToString(), (IsActive == null ? null : IsActive.ToString()), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_DefineMT_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int MTTypeID, bool? IsActive, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "MTTypeID", "IsActive", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, MTTypeID.ToString(), (IsActive == null ? null : IsActive.ToString()), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_DefineMT_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
