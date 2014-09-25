using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using MyBase.MyWap;
using MyUtility;
using MyLoad_Wap.LoadStatic;
namespace MyWap.test
{
    /// <summary>
    /// Summary description for wh
    /// </summary>
    public class wh : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                // Trả về mã HTML cho header từ template (Fixed)
                MyLoadHeader header = new MyLoadHeader();
                Write(header.GetHTML());

                MyLoadNote mContent = new MyLoadNote(mContent_InsertHTML_Change());
                Write(mContent.GetHTML());

                Write(mContent.GetHTML());
            }
            catch (Exception ex)
            {
                Write(ex.Message);
            }
            // Trả về mã HTML cho footer từ template (Fixed)
            MyLoadFooter footer = new MyLoadFooter();
            Write(footer.GetHTML());
        }
        string mContent_InsertHTML_Change()
        {
            try
            {
                StringBuilder mBuilder = new StringBuilder(string.Empty);
                mBuilder.Append("<p><h1>Get from Header</h1></p>");

                for (int i = 0; i < HttpContext.Current.Request.Headers.Count; i++)
                {
                    mBuilder.Append("<p>" + HttpContext.Current.Request.Headers.GetKey(i) + ":" + HttpContext.Current.Request.Headers[i] + "</p>");
                }
                mBuilder.Append("<p><h1>Get from ServerVariables</h1></p>");
                foreach (string key in HttpContext.Current.Request.ServerVariables.AllKeys)
                {
                    mBuilder.Append("<p>" + key + ":" + HttpContext.Current.Request.ServerVariables[key] + "</p>");
                }
                return mBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}