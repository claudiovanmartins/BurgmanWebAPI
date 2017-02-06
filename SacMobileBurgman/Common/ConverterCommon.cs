using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.Common
{
    public static class StringExtensions
    {
        public static Double ToDouble(this string input)
        {
            double value = Double.Parse(input, CultureInfo.InvariantCulture);
            return value;
        }
    }

    public static class DoubleExtensions
    {
        public static Double ToInvariantCulture(this double input)
        {
            return Double.Parse(input.ToString(), CultureInfo.InvariantCulture);
        }
    }
}