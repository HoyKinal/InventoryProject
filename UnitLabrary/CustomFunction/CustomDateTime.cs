using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitLabrary.CustomFunction
{
    public static class CustomDateTime
    {
        public static DateTime ConvertDateTime(this string dateString)
        {
            try
            {
                return DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss tt", null);
            }
            catch
            {
                return DateTime.UtcNow.AddHours(7);
            }
        }
    }
}
