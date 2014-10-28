using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using MyUtility;
using System.Threading;
namespace PostXML
{
    

       

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Request(string MSISDN)
        {
            string XMLRquest = string.Empty;
            string XMLResponse = string.Empty;
            WebRequest req = null;
            WebResponse rsp = null;
            try
            {
                string XMLFormat = tbx_Request.Text;
                XMLRquest = string.Format(XMLFormat, new string[] { MSISDN });
                System.Net.ServicePointManager.Expect100Continue = false;
                req = WebRequest.Create(tbx_Link.Text);
                //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
                req.Method = "POST";        // Post method
                req.ContentType = "text/xml";     // content type
                // Wrap the request stream with a text-based writer

                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                
                //Write the XML text into the stream
                writer.WriteLine(XMLRquest);
                writer.Close();
                //Send the data to the webserver
                rsp = req.GetResponse();
                XMLResponse = new StreamReader(rsp.GetResponseStream()).ReadToEnd();
                tbx_Response.Text += XMLResponse;
                tbx_Response.Text += Environment.NewLine;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyLogfile.WriteLogError(ex);
            }
            finally
            {
                MyLogfile.WriteLogData("RESEND_MT", "REQUEST XML --> " + XMLRquest);
                MyLogfile.WriteLogData("RESEND_MT", "RESPONSE XML --> " + XMLResponse);

                if (req != null) req.GetRequestStream().Close();
                if (rsp != null) rsp.GetResponseStream().Close();
            }
        }
        private void Request()
        {
            string XMLRquest = string.Empty;
            string XMLResponse = string.Empty;

            WebRequest req = null;
            WebResponse rsp = null;

            try
            {
                XMLRquest = tbx_Request.Text;
                System.Net.ServicePointManager.Expect100Continue = false;
                req = WebRequest.Create(tbx_Link.Text);
                //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
                req.Method = "POST";        // Post method
                req.ContentType = "text/xml";     // content type
                // Wrap the request stream with a text-based writer

                StreamWriter writer = new StreamWriter(req.GetRequestStream());

                // Write the XML text into the stream
                writer.WriteLine(XMLRquest);
                writer.Close();
                //Send the data to the webserver
                rsp = req.GetResponse();
                XMLResponse = new StreamReader(rsp.GetResponseStream()).ReadToEnd();
                tbx_Response.Text += XMLResponse;
                tbx_Response.Text += Environment.NewLine;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyLogfile.WriteLogError(ex);
            }
            finally
            {
                MyLogfile.WriteLogData("RESEND_MT", "REQUEST XML --> " + XMLRquest);
                MyLogfile.WriteLogData("RESEND_MT", "RESPONSE XML --> " + XMLResponse);

                if (req != null) req.GetRequestStream().Close();
                if (rsp != null) rsp.GetResponseStream().Close();
            }
        }
        private void btn_Post_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbx_FileName.Text))
                {
                    Request();
                }
                else
                {
                    List<string> mList = new List<string>();
                    if (ReadFile(ref mList))
                    {
                        foreach (string MSISDN in mList)
                        {
                            Request(MSISDN);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyLogfile.WriteLogError(ex);
            }
        }

        private bool ReadFile(ref List<string> mList)
        {
            mList = new List<string>();
            System.IO.StreamReader file = null;
            try
            {
                // Read the file and display it line by line.
                file = new System.IO.StreamReader(tbx_FileName.Text);
                string Line = string.Empty;
                int CountLine = 0;
                while ((Line = file.ReadLine()) != null)
                {
                    CountLine++;
                    Line = Line.Trim();

                    MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                    //if (!MyCheck.CheckPhoneNumber(ref Line, ref mTelco, "84"))
                    //{
                    //    if (MessageBox.Show("Định dạng số điện thoại không đúng tại dòng " + CountLine + " | Giá trị:" + Line + ". Bạn có muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    //    {
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                        mList.Add(Line);
                    //}
                }
                return true;

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                return false;
            }
            finally
            {
                if(file != null)
                    file.Close();
            }
        }
        private void btn_ChoiceFile_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = false;
            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = false;

            try
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK && openFileDialog1.FileNames.Count() < 501)
                {
                    tbx_FileName.Text = openFileDialog1.FileNames[0];
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void btn_SoapPost_Click(object sender, EventArgs e)
        {
            tbx_Response.Text += Environment.NewLine + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            tbx_Response.Text += Environment.NewLine + DateTime.Now.ToString(MyConfig.LongDateFormat);
        }

        protected virtual WebRequest CreateRequest(ISoapMessage soapMessage)
        {
            var wr = WebRequest.Create(soapMessage.Uri);
            wr.ContentType = "text/xml;charset=utf-8";
            wr.ContentLength = soapMessage.ContentXml.Length;

            wr.Headers.Add("SOAPAction", soapMessage.SoapAction);
            wr.Credentials = soapMessage.Credentials;
            wr.Method = "POST";
            wr.GetRequestStream().Write(Encoding.UTF8.GetBytes(soapMessage.ContentXml), 0, soapMessage.ContentXml.Length);

            return wr;
        }
    }
}
