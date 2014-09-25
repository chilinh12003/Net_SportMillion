using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;
namespace MyWap.Page
{
    /// <summary>
    /// Summary description for checkcode
    /// </summary>
    public class checkcode : MyWapBase
    {
        public override void WriteHTML()
        {
            string Para = string.Empty;
            string MSISDN_VNP = string.Empty;
            int PageIndex = 0;

            try
            {
                MSISDN_VNP = string.Empty;
                Para = Request.QueryString["para"];
                if (!string.IsNullOrEmpty(Request.QueryString["i"]))
                {
                    int.TryParse(Request.QueryString["i"].Trim(), out PageIndex);
                }
                MyLoadHeader mHeader = new MyLoadHeader();
                Write(mHeader.GetHTML());
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

                if (string.IsNullOrEmpty(MSISDN_VNP))
                {
                    MyLoadNote mNote = new MyLoadNote("Không nhận diện được thuê bao, xin vui lòng sử dụng 3G, GPRS để truy cập.");
                    Write(mNote.GetHTML());
                    return;
                }
                MSISDN = MSISDN_VNP;
                MyLoadListCode mListCode = new MyLoadListCode(MSISDN, PageIndex);
                Write(mListCode.GetHTML());
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLogfile.WriteLogData("CHECK_CODE", "MSISDN INFO:" + MSISDN_VNP );
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());
            }
        }
    }
}