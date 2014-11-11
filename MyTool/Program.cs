using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using MyTool.ReportSync;
namespace MyTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ChargeSync mChargeSync = new ChargeSync();
                //mChargeSync.Run();

                SyncSub mSyncSub = new SyncSub();
                mSyncSub.Run();

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }

    }
}
