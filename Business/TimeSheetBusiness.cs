using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workday.Common;
using Workday.DataAccess;

namespace Workday.Business
{
    public class TimeSheetBusiness
    {
        public static bool AddTimeSheet(TimeSheet thistime)
        {
            return TimeSheetDataAccess.AddTimeSheet(thistime);
        }

        public static byte[] GetStartImageByID(TimeSheet thistime)
        {
            return TimeSheetDataAccess.GetStartImageByID(thistime);
        }
    }
}
