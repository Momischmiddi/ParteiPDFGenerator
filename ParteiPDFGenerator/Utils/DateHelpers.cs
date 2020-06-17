using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbobsPDFRendering.PDFCreators
{
    public class DateHelpers
    {
        public static Tuple<int, int, int, int, int> CalculateAge(DateTime dateTime)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(dateTime).Ticks).Year - 1;
            DateTime PastYearDate = dateTime.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;

            return new Tuple<int, int, int, int, int>(Years, Months, Days, Hours, Seconds);

        }
    }
}
