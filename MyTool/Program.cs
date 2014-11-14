using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using MyTool.ReportSync;
using System.Globalization;

namespace MyTool
{
    class Program
    {
        static Calendar cal = new GregorianCalendar();

        private static void ShowWeekNumber(DateTime dat, CalendarWeekRule rule,
                                      DayOfWeek firstDay)
        {
            Console.WriteLine("{0:d} with {1:F} rule and {2:F} as first day of week: week {3}",
                              dat, rule, firstDay, cal.GetWeekOfYear(dat, rule, firstDay));
        }   
      
        static void Main(string[] args)
        {
            //DateTime date = new DateTime(2013, 1, 5);
            //DayOfWeek firstDay = DayOfWeek.Sunday;
            //CalendarWeekRule rule;

            //rule = CalendarWeekRule.FirstFullWeek;
            //ShowWeekNumber(date, rule, firstDay);

            //rule = CalendarWeekRule.FirstFourDayWeek;
            //ShowWeekNumber(date, rule, firstDay);

            //Console.WriteLine();
            //date = new DateTime(2010, 1, 5);
            //ShowWeekNumber(date, rule, firstDay);

            //DateTime Test = MyConvert.GetFirstDayOfWeek(2014, 2);
            //int weekNumber = MyConvert.GetWeekOfYear(Test);
            //int weekNumber_2 = MyConvert.GetWeekOfYear(new DateTime(2014, 1, 5));

            try
            {
                //SyncMO mSyncMO = new SyncMO();
                //mSyncMO.Run();

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
