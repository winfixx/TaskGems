using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGems.DataAccess.Helpers
{
    internal static class DoubleHelper
    {
        public static bool TryToDouble(string num, out double result) => double.TryParse(num, CultureInfo.InvariantCulture, out result);
    }
}
