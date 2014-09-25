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
    /// Summary description for SportMillion
    /// </summary>
    public class SportMillion : MyWapBase
    {

        Keyword mKeyword = new Keyword("strConnection_SportMillion");

        private void _RedirectWebsite()
        {
            try
            {
                string ListRedirectWeb = MyConfig.GetKeyInConfigFile("ListRedirectWeb");
                if (!string.IsNullOrEmpty(ListRedirectWeb))
                {
                    string[] arr_url = ListRedirectWeb.Split('|');

                    if (arr_url.Length > 1)
                    {
                        Random mRand = new Random();
                        int mRand_Number = mRand.Next(0, arr_url.Length);

                        Response.Redirect(arr_url[mRand_Number]);
                        return;
                    }
                    else if (arr_url.Length == 1)
                    {
                        Response.Redirect(arr_url[0]);
                        return;
                    }
                }
               
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);

            }
            Response.Redirect("http://thethao.vnexpress.net/");
            return;
        }

        public override void WriteHTML()
        {
            string Para = string.Empty;
            string MSISDN_VNP = string.Empty;

            int KeywordID = 0;
            int PartnerID = 0;
            string Keyword = string.Empty;

            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            try
            {
                MSISDN_VNP = string.Empty;
                Para = "BhqUN3Fs0n91DKn2tCAUvo5CVs47MVDhdqTID4vVjfE2fJ7faCgkQEhBaKrdu6oWbWxdLexJzJ2RPgGtPPIyCw==";//Request.QueryString["para"];

                MyLoadHeader mHeader = new MyLoadHeader();
                Write(mHeader.GetHTML());

                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    int.TryParse(Request.QueryString["kid"].TrimEnd().TrimStart(), out KeywordID);
                }
                
                if (!string.IsNullOrEmpty(Para))
                {
                    string Para_Decode = MySecurity.AES.Decrypt(Para, MyConfig.GetKeyInConfigFile("GetMSISDN_Password"));
                    if (!string.IsNullOrEmpty(Para_Decode))
                    {
                        string[] arr = Para_Decode.Split('|');
                        if (arr.Length == 2)
                            MSISDN_VNP = arr[0];
                    }
                }

                MSISDN = MSISDN_VNP;

                ////kiểm tra nếu truy cập quá nhanh
                //if (Session["RequestTime"] != null)
                //{
                //    DateTime RequestTime = (DateTime)Session["RequestTime"];
                //    TimeSpan Interval_Time = DateTime.Now - RequestTime;
                //    if (Interval_Time.TotalSeconds < 60)
                //    {
                //        MyLoadNote mNote = new MyLoadNote("Bạn đang truy cập lặp lại quá nhanh, xin vui lòng chờ trong 1 phút và hãy thử lại.");
                //        Write(mNote.GetHTML());
                //        return;
                //    }
                //}

                //Session["RequestTime"] = DateTime.Now;

                DataTable mTable_Keyword = mKeyword.Select(2, KeywordID.ToString(), string.Empty);

                if (mTable_Keyword.Rows.Count < 1)
                {
                    MyLoadNote mNote = new MyLoadNote("Thông tin của đối tác không hợp lệ, xin vui lòng thử lại với thông tin khác.");
                    Write(mNote.GetHTML());
                    return;
                    //RedirectWebsite();
                    //return;
                }

                PartnerID = (int)mTable_Keyword.Rows[0]["PartnerID"];
                Keyword = mTable_Keyword.Rows[0]["Keyword"].ToString();

                // Trả về mã HTML cho header từ template (Fixed)
                if (string.IsNullOrEmpty(MSISDN_VNP))
                {
                    MyLoadNote mNote = new MyLoadNote("Không nhận diện được thuê bao khách hàng, xin vui lòng sử dụng 3G hoặc GPRS để truy cập.");
                    Write(mNote.GetHTML());
                    return;
                    //RedirectWebsite();
                    //return;
                }

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN_VNP, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyLoadNote mNote = new MyLoadNote("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone.");
                    Write(mNote.GetHTML());
                    return;
                    //RedirectWebsite();
                    //return;
                }

                WS_SportMillion.SportMillionSoapClient mClient = new WS_SportMillion.SportMillionSoapClient();
                string Signature = MSISDN_VNP + "|HBWap|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, "wre34WD45F");
                System.Net.ServicePointManager.Expect100Continue = false;
                string Result_Check = mClient.Check(Signature);
                string[] Arr_Result_Check = Result_Check.Split('|');

                ////Nếu đã từng sử dụng dịch vụ thì chuyển sang các trang thể thao 
                //if (Arr_Result_Check[0].Equals("2") || Arr_Result_Check[0].Equals("4"))
                //{
                //    RedirectWebsite();
                //    return;
                //}

                //Hiện thị confirm
                if ((bool)mTable_Keyword.Rows[0]["IsConfirm"])
                {
                    string ConfirmPara = MSISDN_VNP + "|" + PartnerID.ToString() + "|" + Keyword;
                    string ConfirmPara_Encode = MySecurity.AES.Encrypt(ConfirmPara, MyConfig.GetKeyInConfigFile("GetMSISDN_Password"));
                    MyLoadConfirm mLoadConfirm = new MyLoadConfirm(HttpUtility.UrlEncode(ConfirmPara_Encode));

                    MyLoadNote mNote = new MyLoadNote(mLoadConfirm.GetHTML());
                    Write(mNote.GetHTML());
                    return;
                }
               
                string Result = mClient.Reg((int)MyConfig.ChannelType.WAP, Signature, Keyword);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = string.Empty;

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
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLogfile.WriteLogData("REGISTER", "REGISTER INFO: PartnerID:" + PartnerID.ToString() + "|Keyword:" + Keyword + "|MSISDN:" + MSISDN_VNP + "|ErrorCode:" + ErrorCode + "|ErrorDesc:" + ErrorDesc);
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());
            }
           
        }
    }
}