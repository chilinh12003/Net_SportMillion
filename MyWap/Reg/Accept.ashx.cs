using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;

namespace MyWap.Reg
{
    /// <summary>
    /// Summary description for Accept
    /// </summary>
    public class Accept : MyWapBase
    {

        Keyword mKeyword = new Keyword("strConnection_SportMillion");

        public override void WriteHTML()
        {
            string Para = string.Empty;
            string MSISDN_VNP = string.Empty;

            int PartnerID = 0;
            string Keyword = string.Empty;

            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            try
            {
                Para = Request.QueryString["para"];

                MyLoadHeader mHeader = new MyLoadHeader();
                Write(mHeader.GetHTML());


                if (!string.IsNullOrEmpty(Para))
                {
                    string Para_Decode = MySecurity.AES.Decrypt(Para, MyConfig.GetKeyInConfigFile("GetMSISDN_Password"));
                    if (!string.IsNullOrEmpty(Para_Decode))
                    {
                        string[] arr = Para_Decode.Split('|');
                        if (arr.Length == 3)
                        {
                            MSISDN_VNP = arr[0];
                            int.TryParse(arr[1], out PartnerID);
                            Keyword = arr[2];
                        }
                    }
                }
                else
                {
                    MyLoadNote mNote = new MyLoadNote("Thông tin không hợp lệ, xin vui lòng thử lại với thông tin khác.");
                    Write(mNote.GetHTML());
                    return;
                }

                ////kiểm tra nếu truy cập quá nhanh
                //if (Session["RequestTime_Accept"] != null)
                //{
                //    DateTime RequestTime = (DateTime)Session["RequestTime_Accept"];
                //    TimeSpan Interval_Time = DateTime.Now - RequestTime;
                //    if (Interval_Time.TotalSeconds < 60)
                //    {
                //        MyLoadNote mNote = new MyLoadNote("Bạn đang truy cập lặp lại quá nhanh, xin vui lòng chờ trong 1 phút và hãy thử lại.");
                //        Write(mNote.GetHTML());
                //        return;
                //    }
                //}

                //Session["RequestTime_Accept"] = DateTime.Now;

                DataTable mTable_Keyword = mKeyword.Select(3, Keyword, PartnerID.ToString());

                if (mTable_Keyword.Rows.Count < 1)
                {
                    MyLoadNote mNote = new MyLoadNote("Thông tin của đối tác không hợp lệ, xin vui lòng thử lại với thông tin khác.");
                    Write(mNote.GetHTML());
                    return;
                }

                // Trả về mã HTML cho header từ template (Fixed)
                if (string.IsNullOrEmpty(MSISDN_VNP))
                {
                    MyLoadNote mNote = new MyLoadNote("Không nhận diện được thuê bao khách hàng, xin vui lòng sử dụng 3G hoặc GPRS để truy cập.");
                    Write(mNote.GetHTML());
                    return;
                }

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN_VNP, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyLoadNote mNote = new MyLoadNote("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone.");
                    Write(mNote.GetHTML());
                    return;
                }

                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                string Signature = MSISDN_VNP + "|HBWap|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, "wre34WD45F");
                System.Net.ServicePointManager.Expect100Continue = false;
                string Result = mClient.Reg((int)MyConfig.ChannelType.WAP, Signature, Keyword);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = Arr_Result[1];

                switch (ErrorCode)
                {
                    case "1":
                        ErrorDesc = "Chúc mừng bạn đã đăng ký thành công dịch vụ Triệu Phú Thể Thao.";
                        break;
                    case "0":
                        ErrorDesc = "Đăng ký dịch vụ không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "2":
                        ErrorDesc = "Bạn đã đăng ký dịch vụ này trước đây.";
                        break;
                    case "3":
                        ErrorDesc = "Đăng ký không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "-1":
                        ErrorDesc = "Đăng ký không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "-2":
                        ErrorDesc = "Đăng ký không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                }

                MyLoadNote mNote_1 = new MyLoadNote(ErrorDesc);
                Write(mNote_1.GetHTML());
            }
            catch (Exception ex)
            {
                mLog.Error(ex);
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());

            }
           
        }
    }
}