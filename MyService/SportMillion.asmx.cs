using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyUtility;
using MySportMillion;
using MySportMillion.Service;
using MySportMillion.Sub;
using MySportMillion.Gateway;
using System.ComponentModel;

using System.Data;
namespace MyService
{
    /// <summary>
    /// Summary description for SportMillion
    /// </summary>
    [WebService(Namespace = "http://hbcom.vn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SportMillion : System.Web.Services.WebService
    {
        Subscriber mSub = new Subscriber();
        UnSubscriber mUnSub = new UnSubscriber();
        sms_receive_queue mQuere = new sms_receive_queue(MySetting.AdminSetting.MySQLConnection_Gateway);

        private class PartnerSignature
        {
            public string MSISDN = string.Empty;
            public string UniqueID = string.Empty;
            public string PartnerKey = string.Empty;
            public bool Result = false;
        }
        private enum REGResult
        {
            [DescriptionAttribute("Không xác định được nguyên nhân")]
            UnknowError = -1,
            [DescriptionAttribute("Lỗi xảy ra trong quá trình thực hiện")]
            Error = -2,
            [DescriptionAttribute("Đăng ký dịch vụ thành công")]
            Success = 1,
            [DescriptionAttribute("Đắng ký thất bại")]
            Fail = 0,
            [DescriptionAttribute("Thuê bao đã đăng ký trước đó")]
            Exist = 2,
            [DescriptionAttribute("Thông tin truyền vào không hợp lệ")]
            InputInvalid = 3,
            [DescriptionAttribute("Đã hủy dịch vụ rồi")]
            Deregistered = 4,
            [DescriptionAttribute("Chưa từng sử dụng dịch vụ này")]
            NeverUseService = 5,
            [DescriptionAttribute("Thuê bao chưa đăng ký dịch vụ")]
            NotRegister = 6,
        }
        private string BuildResult(REGResult mResult)
        {
            return ((int)mResult).ToString() + "|" + MyEnum.StringValueOf(mResult);
        }
        private PartnerSignature CheckSignature(string Signature)
        {
            PartnerSignature mResult = new PartnerSignature();
            try
            {
                string Data = MySecurity.AES.Decrypt(Signature, MySetting.AdminSetting.RegWSKey);
                string[] arr = Data.Split('|');
                if (arr.Length != 3)
                    return mResult;

                mResult.MSISDN = arr[0];
                mResult.PartnerKey = arr[1];
                mResult.UniqueID = arr[2];
                if (string.IsNullOrEmpty(mResult.MSISDN) ||
                    string.IsNullOrEmpty(mResult.PartnerKey) ||
                    string.IsNullOrEmpty(mResult.UniqueID))
                {
                    mResult.Result = false;
                }
                else
                {
                    DateTime DateRequest = DateTime.MinValue;
                    DateTime.TryParseExact(mResult.UniqueID, "yyyyMMddHHmmssfff", null, System.Globalization.DateTimeStyles.None, out DateRequest);
                    TimeSpan RequestDelay = DateTime.Now - DateRequest;

                    //Nếu khoảng thời gian request lớn hơn 3 phút thì ko thực hiện
                    if (RequestDelay.TotalMinutes > 3)
                    {
                        mResult.Result = false;
                    }
                    else
                    {
                        mResult.Result = true;
                    }
                }
                return mResult;
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                return mResult;
            }
        }


        [WebMethod]
        public string Check(string Signature)
        {
            REGResult mResult = REGResult.UnknowError;
            PartnerSignature mSig = new PartnerSignature();
            string RequestID = string.Empty;
            try
            {
                mSig = CheckSignature(Signature);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (mSig.Result == false || !MyCheck.CheckPhoneNumber(ref mSig.MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    mResult = REGResult.InputInvalid;
                    return BuildResult(mResult);
                }

                int PID = MyPID.GetPIDByPhoneNumber(mSig.MSISDN, MySetting.AdminSetting.MaxPID);

                DataTable mTable_Sub = mSub.Select(2, PID.ToString(), mSig.MSISDN);
                if (mTable_Sub.Rows.Count > 0)
                {
                    mResult = REGResult.Exist;
                    return BuildResult(mResult);
                }

                DataTable mTable_UnSub = mUnSub.Select(2, PID.ToString(), mSig.MSISDN);
                if (mTable_UnSub.Rows.Count > 0)
                {
                    //Đã hủy dịch vụ trước đó
                    mResult = REGResult.Deregistered;
                    return BuildResult(mResult);
                }

                //Chưa từng sử dụng dịch vụ này
                mResult = REGResult.NeverUseService;
                return BuildResult(mResult);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                mResult = REGResult.Error;
                return BuildResult(mResult);
            }
            finally
            {
                MyLogfile.WriteLogData("CHECK-->PartnerKey:" + mSig.PartnerKey + "|MSISDN:" + mSig.MSISDN + "|UniqueID:" + mSig.UniqueID + "|Signature:" + Signature + "|Result:" + BuildResult(mResult));
            }
            
        }
      
        [WebMethod]
        public string Reg(int ChannelType, string Signature, string CommandCode)
        {
            REGResult mResult = REGResult.UnknowError;
            PartnerSignature mSig = new PartnerSignature();

            string RequestID = string.Empty;
            try
            {
                mSig = CheckSignature(Signature);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (mSig.Result == false || !MyCheck.CheckPhoneNumber(ref mSig.MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    mResult = REGResult.InputInvalid;
                    return BuildResult(mResult);
                }

                int PID = MyPID.GetPIDByPhoneNumber(mSig.MSISDN, MySetting.AdminSetting.MaxPID);

                DataTable mTable_Sub = mSub.Select(2, PID.ToString(), mSig.MSISDN);
                if (mTable_Sub.Rows.Count > 0)
                {
                    mResult = REGResult.Exist;
                    return BuildResult(mResult);
                }

                RequestID = MySecurity.CreateCode(9);
                if (mQuere.Insert(mSig.MSISDN, MySetting.AdminSetting.ShoreCode, CommandCode, CommandCode, RequestID, (MyConfig.ChannelType)ChannelType))
                {
                    mResult = REGResult.Success;
                    return BuildResult(mResult);
                }
                else
                {
                    mResult = REGResult.Fail;
                    return BuildResult(mResult);
                }
            }
            catch (Exception ex)
            {
                
                MyLogfile.WriteLogError(ex);
                mResult = REGResult.Error;
                return BuildResult(mResult);
            }
            finally
            {
                MyLogfile.WriteLogData("REQUEST-->PartnerKey:" + mSig.PartnerKey + "|MSISDN:" + mSig.MSISDN + "|UniqueID:" + mSig.UniqueID + "|ChannelType:" + ChannelType.ToString() + "|Signature:" + Signature + "|Result:" + BuildResult(mResult));
            }
            
        }


        [WebMethod]
        public string Dereg(int ChannelType, string Signature, string CommandCode)
        {
            REGResult mResult = REGResult.UnknowError;
            PartnerSignature mSig = new PartnerSignature();

            string RequestID = string.Empty;
            try
            {
                mSig = CheckSignature(Signature);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (mSig.Result == false || !MyCheck.CheckPhoneNumber(ref mSig.MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    mResult = REGResult.InputInvalid;
                    return BuildResult(mResult);
                }

                int PID = MyPID.GetPIDByPhoneNumber(mSig.MSISDN, MySetting.AdminSetting.MaxPID);

                DataTable mTable_Sub = mSub.Select(2, PID.ToString(), mSig.MSISDN);
                if (mTable_Sub.Rows.Count < 1)
                {
                    mResult = REGResult.NotRegister;
                    return BuildResult(mResult);
                }

                RequestID = MySecurity.CreateCode(9);

                if (mQuere.Insert(mSig.MSISDN, MySetting.AdminSetting.ShoreCode, CommandCode, CommandCode, RequestID, (MyConfig.ChannelType)ChannelType))
                {
                    mResult = REGResult.Success;
                    return BuildResult(mResult);
                }
                else
                {
                    mResult = REGResult.Fail;
                    return BuildResult(mResult);
                }
            }
            catch (Exception ex)
            {

                MyLogfile.WriteLogError(ex);
                mResult = REGResult.Error;
                return BuildResult(mResult);
            }
            finally
            {
                MyLogfile.WriteLogData("REQUEST-->PartnerKey:" + mSig.PartnerKey + "|MSISDN:" + mSig.MSISDN + "|UniqueID:" + mSig.UniqueID + "|ChannelType:" + ChannelType.ToString() + "|Signature:" + Signature + "|Result:" + BuildResult(mResult));
            }

        }

         [WebMethod]
        public DataTable GetInfo(string Signature)
        {
            DataTable mTable_Return = new DataTable();
            try
            {

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }

            return mTable_Return;
        }
    }
}
