using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUtility;
using System.IO;
using MyConnect.MySQL;
namespace PostXML
{
    public class Phone
    {
        public string MSISDN = string.Empty;
        public int BuyCount = 0;
        public bool IsCreateRandom = false;

    }
    public partial class Form2 : Form
    {
        List<string> mList_Text = new List<string>();
        List<Phone> mList_Phone = new List<Phone>();
        public Form2()
        {
            InitializeComponent();
        }

        public bool Insert(string USER_ID, string INFO, string REQUEST_ID)
        {
            try
            {
                MySQLExecuteData mExec = new MySQLExecuteData("MySQLConnection_Gateway");

                string SERVICE_ID = "1566";
                string COMMAND_CODE = "";
                MyConfig.ChannelType CHANNEL_TYPE = MyConfig.ChannelType.SMS;
                string MOBILE_OPERATOR = "GPC";

                string Format_Query = "INSERT INTO sms_receive_queue(user_id,service_id,mobile_operator,info,command_code,REQUEST_ID, CHANNEL_TYPE) " +
                                        "VALUE (@USER_ID, @SERVICE_ID, @MOBILE_OPERATOR, @INFO, @COMMAND_CODE, @REQUEST_ID, @CHANNEL_TYPE)";

                string[] arr_para = { "@USER_ID", "@SERVICE_ID", "@MOBILE_OPERATOR","@INFO", "@COMMAND_CODE", 
					                    "@REQUEST_ID", "@CHANNEL_TYPE"};
                string[] arr_value = { USER_ID, SERVICE_ID, MOBILE_OPERATOR,INFO, COMMAND_CODE, 
					                    REQUEST_ID, ((int)CHANNEL_TYPE).ToString()};


                if (mExec.ExecQuery(Format_Query, arr_para, arr_value) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        string[] mListAnswer = new string[] { "DD ChiLinh", "DD HaHo", "DD Ngocha", "DD ngocha", "DD hongocha", "DD hoho", "DD HoNgocHaaa", "DD Thuminh", "DD *HONGOCHA", "DD hOngOcHa2", "DD HoNgoc", "DD ngochaho3", "DD ngocho" };

        private bool ReadFile()
        {
            mList_Phone = new List<Phone>();
            System.IO.StreamReader file = null;
            try
            {
                if (string.IsNullOrEmpty(tbx_FileName.Text.Trim()))
                {
                    MessageBox.Show("Xin hay chon file truoc");
                    return false;
                }

                // Read the file and display it line by line.
                file = new System.IO.StreamReader(tbx_FileName.Text);
                string Line = string.Empty;
                int CountLine = 0;
                while ((Line = file.ReadLine()) != null)
                {
                    CountLine++;
                    Line = Line.Trim();
                    Phone mPhone = new Phone();
                    
                    mPhone.MSISDN = Line;                  
                    mList_Phone.Add(mPhone);
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
                if (file != null)
                    file.Close();
            }
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            try
            {

                timer_Update.Start();
                ReadFile();
                timer_Load.Start();
                timer_Load_2.Start();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
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

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_Load.Stop();
            timer_Update.Stop();
            timer_Load_2.Stop();
        }

        int CurrentIndex = 0;
        int RowCount = 0;
        private void timer_Update_Tick(object sender, EventArgs e)
        {
            try
            {
                if (mList_Text.Count < 1)
                {
                    return;
                }
                if (RowCount++ > 15)
                {
                    rtbx_UpdateText.Text = "";
                    RowCount = 0;
                }

                for (int i = CurrentIndex; i < mList_Text.Count; i++)
                {
                    MyLogfile.WriteLogData("Data_", mList_Text[i]);
                    rtbx_UpdateText.Text += mList_Text[i];
                    
                    rtbx_UpdateText.Text += Environment.NewLine;
                }
                
                mList_Text.Clear();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string Keyword_Buy = "MUA";
        private void timer_Load_Tick(object sender, EventArgs e)
        {
            try
            {
                if (mList_Phone.Count < 1)
                    return;

                string RequestID = string.Empty;
                Random mRand = new Random();                

                int Index = mRand.Next(0, mList_Phone.Count - 1);

                Phone mPhone = mList_Phone[Index];

                if (!mPhone.IsCreateRandom)
                {
                    mPhone.BuyCount = mRand.Next(0, 20);
                    mPhone.IsCreateRandom = true;
                }

                RequestID = DateTime.Now.ToString("yyyyddMMHHmmssfff");
                Insert(mPhone.MSISDN, Keyword_Buy, RequestID);


                mList_Text.Add(DateTime.Now.ToString(MyConfig.LongDateFormat) + "-->PROCESS_1: -->Info:" + Keyword_Buy + "||MSISDN:" + mPhone.MSISDN + "||RequestID:" + RequestID + "|| BuyCount:" + mPhone.BuyCount);

                System.Threading.Thread.Sleep(mRand.Next(10, 2000));

                int index_Answer = mRand.Next(0, mListAnswer.Length - 1);
                RequestID = DateTime.Now.ToString("yyyyddMMHHmmssfff");
                Insert(mPhone.MSISDN, mListAnswer[index_Answer], RequestID);
               

                mList_Text.Add(DateTime.Now.ToString(MyConfig.LongDateFormat) + "-->PROCESS_1: -->Info:" + mListAnswer[index_Answer] + "||MSISDN" + mPhone.MSISDN + "||RequestID:" + RequestID);

                mPhone.BuyCount--;

                if (mPhone.BuyCount == 0)
                    mList_Phone.Remove(mPhone);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void timer_Load_Tick_2(object sender, EventArgs e)
        {
            try
            {
                if (mList_Phone.Count < 1)
                    return;

                string RequestID = string.Empty;
                Random mRand = new Random();

                int Index = mRand.Next(0, mList_Phone.Count - 1);

                Phone mPhone = mList_Phone[Index];

                if (!mPhone.IsCreateRandom)
                {
                    mPhone.BuyCount = mRand.Next(0, 20);
                    mPhone.IsCreateRandom = true;
                }

                RequestID = DateTime.Now.ToString("yyyyddMMHHmmssfff");
                Insert(mPhone.MSISDN, Keyword_Buy, RequestID);
                System.Threading.Thread.Sleep(mRand.Next(10, 2000));

                mList_Text.Add(DateTime.Now.ToString(MyConfig.LongDateFormat) + "-->PROCESS_2: -->Info:" + Keyword_Buy + "||MSISDN:" + mPhone.MSISDN + "||RequestID:" + RequestID + "|| BuyCount:" + mPhone.BuyCount);

                int index_Answer = mRand.Next(0, mListAnswer.Length - 1);
                RequestID = DateTime.Now.ToString("yyyyddMMHHmmssfff");
                Insert(mPhone.MSISDN, mListAnswer[index_Answer], RequestID);
                

                mList_Text.Add(DateTime.Now.ToString(MyConfig.LongDateFormat) + "-->PROCESS_2: -->Info:" + mListAnswer[index_Answer] + "||MSISDN" + mPhone.MSISDN + "||RequestID:" + RequestID);

                mPhone.BuyCount--;

                if (mPhone.BuyCount == 0)
                    mList_Phone.Remove(mPhone);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
