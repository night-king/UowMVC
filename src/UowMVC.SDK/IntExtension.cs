using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.SDK
{
    public static class IntExtension
    {
        public static string Uniform(this int v, int length)
        {
            var va = v.ToString();
            if (va.Length > length) { return va; }
            for (int i = 0; i < length - va.Length; i++)
            {
                va = "0" + va;
            }
            return va;
        }
        public static string Cute(this int v, int length)
        {
            var va = v.ToString();
            if (va.Length > length)
            {
                return va.Substring(va.Length - length, length);
            }
            for (int i = 0; i < length - va.Length; i++)
            {
                va = "0" + va;
            }
            return va;
        }
    }
}
