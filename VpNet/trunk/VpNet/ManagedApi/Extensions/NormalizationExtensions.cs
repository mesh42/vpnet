using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VpNet.ManagedApi.Extensions
{
    public static class NormalizationExtensions
    {
        public static float Truncate(this float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }
    }
}
