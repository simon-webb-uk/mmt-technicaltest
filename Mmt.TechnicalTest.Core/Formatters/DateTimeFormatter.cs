using System;

namespace Mmt.TechnicalTest.Core.Formatters
{
    public static class DateTimeFormatter
    {
        public static string ToShortDateString(DateTime candidate)
        {
            return candidate.ToString("dd-MMM-yyyy");
        }
    }
}
