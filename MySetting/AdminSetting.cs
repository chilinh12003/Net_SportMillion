using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using MyUtility;
namespace MySetting
{
    public class AdminSetting
    {

        public enum ListPage
        {
            [DescriptionAttribute("Quản trị thể loại")]
            Categories,
            [DescriptionAttribute("Quản trị Menu")]
            MenuAdmin,
            [DescriptionAttribute("Cấu hình hệ thống")]
            SystemConfig,
            [DescriptionAttribute("Nhóm thành viên")]
            MemberGroup,
            [DescriptionAttribute("Quản trị tài khoản")]
            Member,
            [DescriptionAttribute("Phần quyền")]
            Permission,
            [DescriptionAttribute("Log thành viên")]
            MemberLog,
            [DescriptionAttribute("Đổi mật khẩu")]
            ChangePass,
            [DescriptionAttribute("Quản trị Đối tác")]
            Partner,
            [DescriptionAttribute("Quản trị Thể loại")]
            Category,
            [DescriptionAttribute("Tin tức")]
            News,
            [DescriptionAttribute("Đơn vị hành chính")]
            Position,
            [DescriptionAttribute("Đường phố")]
            Street,

            [DescriptionAttribute("Tin tức")]
            Article,

            [DescriptionAttribute("Dịch vụ")]
            Service,
            [DescriptionAttribute("Nhóm Dịch vụ")]
            ServiceGroup,

            [DescriptionAttribute("Số lượng thuê bao đăng ký dịch vụ")]
            ReportCountSub,

            [DescriptionAttribute("Số lượng thuê bao hủy dịch vụ")]
            ReportCountUnSub,

            [DescriptionAttribute("Lịch sử Đăng ký/Hủy/Gia hạn")]
            MOLog,

            [DescriptionAttribute("Lịch sử trả MT")]
            ReportMTHisTory,
            [DescriptionAttribute("Thống kê Sub")]
            RP_Sub,

            [DescriptionAttribute("Lịch sử trừ tiền")]
            ChargeLog,

            [DescriptionAttribute("Thống kê thê bao theo ngày")]
            RP_DaySub_VNP,

            [DescriptionAttribute("Thống kê gia hạn theo ngày")]
            RP_DayRenew_VNP,

            [DescriptionAttribute("Thống kê thê bao theo tuần")]
            RP_WeekSub_VNP,

            [DescriptionAttribute("Thống kê gia hạn theo tuần")]
            RP_WeekRenew_VNP,

            [DescriptionAttribute("Lịch sử đăng ký/huỷ dịch vụ của thuê bao")]
            History_Reg_Dereg,

            [DescriptionAttribute("Lịch sử gia hạn dịch vụ của thuê bao")]
            History_Renew,


            [DescriptionAttribute("Lịch sử MO/MT của thuê bao")]
            History_MO_MT,
            [DescriptionAttribute("Lịch sử tương tác, sử dụng dịch vụ của thuê bao")]
            History_Interaction,
            [DescriptionAttribute("Thông tin sử dụng dịch vụ của thuê bao")]
            CheckDetailInfo,
            [DescriptionAttribute("Chỉ số KPI")]
            KPI,

            [DescriptionAttribute("Thống kê sản lượng")]
            ChargeLogByDay,

            [DescriptionAttribute("Thống kê sản lượng theo giá")]
            ChargeLogByDay_Price,

            [DescriptionAttribute("Thống kê sản lượng theo giá")]
            RPPartnerPrice,
            [DescriptionAttribute("Thống kê sản lượng theo ngày")]
            RPPartnerDay,

            [DescriptionAttribute("Thống kê thuê bao")]
            RPPartnerSub,

            [DescriptionAttribute("Gửi lại MT cho khách hàng")]
            ResendMT,

            [DescriptionAttribute("Đăng ký/Hủy đăng ký")]
            Register,

            [DescriptionAttribute("Thông tin khác hàng")]
            CheckInfo,
            [DescriptionAttribute("Quản trị Trận đấu")]
            Match,
            [DescriptionAttribute("Thống kê lượt Đăng ký/Hủy")]
            ReportSubByDay,
        }

        public struct ParaSave
        {
            /// <summary>
            /// Lưu trữ thông tin Serivice vào session
            /// </summary>
            public static string Service = "Service";
            public static string Partner = "Partner";

        }

        public static string MySQLConnection_Gateway
        {
            get
            {
                return "MySQLConnection_Gateway";
            }
        }

        public static string ShoreCode
        {
            get
            {
                return "9696";
            }
        }

        public static int MaxPID
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Lưu MSISDN xuống session
        /// </summary>
        public static string MSISDN
        {
            get
            {
                try
                {
                    if (MyCurrent.CurrentPage.Session["MSISDN"] == null ||
                        string.IsNullOrEmpty(MyCurrent.CurrentPage.Session["MSISDN"].ToString()))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return MyCurrent.CurrentPage.Session["MSISDN"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MyLogfile.WriteLogError(ex);
                    return string.Empty;
                }

            }
            set
            {
                try
                {
                    MyCurrent.CurrentPage.Session["MSISDN"] = value;
                }
                catch (Exception ex)
                {
                    MyLogfile.WriteLogError(ex);
                }

            }
        }
        /// <summary>
        /// Key dùng để mã hóa tạo chữ ký khi call WS đăng ký dịch vụ
        /// </summary>
        public static string RegWSKey = "wre34WD45F";

        /// <summary>
        /// Key dùng để mã hóa dữ liệu nhạy cảm
        /// </summary>
        public static string SpecialKey = "ChIlINh154";

        public static string AllowIPFile
        {
            get
            {
                string Temp = MyConfig.GetKeyInConfigFile("AllowIPFile");
                if (string.IsNullOrEmpty(Temp))
                    return @"~/App_Data/AllowIP.xml";
                else
                    return Temp;
            }
        }

        /// <summary>
        /// Tắt chức năng kiểm tra IP
        /// </summary>
        public static bool DisableCheckIP
        {
            get
            {
                string Temp = MyConfig.GetKeyInConfigFile("DisableCheckIP");
                if (string.IsNullOrEmpty(Temp))
                {

                    return false;
                }
                else
                {
                    Temp = Temp.Trim();
                    bool bValue = false;
                    bool.TryParse(Temp, out bValue);
                    return bValue;
                }
            }
        }
    }
}
