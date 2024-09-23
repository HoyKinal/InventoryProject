using System;

namespace UnitLabrary.Transaction
{
    public static class Kinal
    {

        public static decimal KinalDecimal(this string value)
        {
			try
			{
				return decimal.Parse(value);
			}
			catch (Exception)
			{
                return 0;
            }
        }
    }
}
