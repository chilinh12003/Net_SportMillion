using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MySportMillion.Service;
using System.Data;
using MyLoad_Wap.LoadVNP;
namespace MyWap.vnp
{
    /// <summary>
    /// Summary description for lp
    /// </summary>
    public class lp : MyWapBase
    {

        Keyword mKeyword = new Keyword("strConnection_SportMillion");
        GetMSISDN mGetMSISDN = new GetMSISDN();
        public override void WriteHTML()
        {
            string Para = string.Empty;

            int KeywordID = 0;
            int PartnerID = 0;
            string Keyword = string.Empty;

            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    int.TryParse(Request.QueryString["kid"].TrimEnd().TrimStart(), out KeywordID);
                }

                MSISDN = mGetMSISDN.GetMSISDN_VNP();

                // Trả về mã HTML cho header từ template (Fixed)
                if (string.IsNullOrEmpty(MSISDN))
                {
                    MyLoadNote mNote = new MyLoadNote("Không nhận diện được thuê bao khách hàng, xin vui lòng sử dụng 3G hoặc GPRS để truy cập.");
                    Write(mNote.GetHTML());
                    return;
                }

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                string MSISDN_TEMP = MSISDN;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN_TEMP, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyLoadNote mNote = new MyLoadNote("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone.");
                    Write(mNote.GetHTML());
                    return;
                }

                //kiểm tra nếu truy cập quá nhanh
                if (Session["RequestTime"] != null)
                {
                    DateTime RequestTime = (DateTime)Session["RequestTime"];
                    TimeSpan Interval_Time = DateTime.Now - RequestTime;
                    if (Interval_Time.TotalSeconds < 60)
                    {
                        MyLoadNote mNote = new MyLoadNote("Bạn đang truy cập lặp lại quá nhanh, xin vui lòng chờ trong 1 phút và hãy thử lại.");
                        Write(mNote.GetHTML());
                        return;
                    }
                }

                Session["RequestTime"] = DateTime.Now;

                DataTable mTable_Keyword = mKeyword.Select(2, KeywordID.ToString(), string.Empty);

                if (mTable_Keyword.Rows.Count < 1)
                {
                    MyLoadNote mNote = new MyLoadNote("Thông tin của đối tác không hợp lệ, xin vui lòng thử lại với thông tin khác.");
                    Write(mNote.GetHTML());
                    return;
                 }

                PartnerID = (int)mTable_Keyword.Rows[0]["PartnerID"];
                Keyword = mTable_Keyword.Rows[0]["Keyword"].ToString();

                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                string Signature = MSISDN + "|HBWap|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, "wre34WD45F");
                System.Net.ServicePointManager.Expect100Continue = false;
                string Result_Check = mClient.Check(Signature);
                string[] Arr_Result_Check = Result_Check.Split('|');

                //Nếu đã từng sử dụng dịch vụ
                if (Arr_Result_Check[0].Equals("2"))
                {
                    MyLoadRegistered mLoadReg = new MyLoadRegistered(MSISDN);
                    Write(mLoadReg.GetHTML());
                    return;
                }

                //Hiện thị confirm tính tiền. đã từng sử dụng dịch vụ (đã hủy dịch vụ)
                if (Arr_Result_Check[0].Equals("4"))
                {
                    MyLoadConfirm_Money mLoadConfirm = new MyLoadConfirm_Money(MSISDN,Keyword,PartnerID);
                    Write(mLoadConfirm.GetHTML());
                    return;
                }

                //nếu chưa từng sử dụng dịch vụ lần nào và keyword này là yêu cầu confirm
                if (Arr_Result_Check[0].Equals("5") &&  (bool)mTable_Keyword.Rows[0]["IsConfirm"])
                {
                    MyLoadConfirm_Free mLoadConfirm = new MyLoadConfirm_Free(MSISDN, Keyword, PartnerID);
                    Write(mLoadConfirm.GetHTML());
                    return;
                }
                //nếu không thì đăng ký ngay
                string Result = mClient.Reg((int)MyConfig.ChannelType.WAP, Signature, Keyword);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = string.Empty;

                switch (ErrorCode)
                {
                    case "1":
                        MyLoadSuccess mLoadSuccess = new MyLoadSuccess(MSISDN);
                        Write(mLoadSuccess.GetHTML());
                        return;
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
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLogfile.WriteLogData("REGISTER", "REGISTER INFO: PartnerID:" + PartnerID.ToString() + "|Keyword:" + Keyword + "|MSISDN:" + MSISDN + "|ErrorCode:" + ErrorCode + "|ErrorDesc:" + ErrorDesc);
            }
           
        }
    }
}