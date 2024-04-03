using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NASADataApi
{
    public static class DateParser
    {
        public static DateTime? ParseDate(string date)
        {
            DateTime dateValue;
            CultureInfo enUS = new CultureInfo("en-US");

            var formatStrings = new string[] { "MM/dd/yy", "MMMM d, yyyy", "MMM-d-yyyy" };

      if (DateTime.TryParseExact(date, formatStrings, enUS, System.Globalization.DateTimeStyles.None | System.Globalization.DateTimeStyles.AllowWhiteSpaces, out dateValue))
        return dateValue;
      //if (DateTime.TryParseExact(date, formatStrings, enUS, System.Globalization.DateTimeStyles.None, out dateValue))
      //          return dateValue;
            else
                return null;
        }
    }
}
