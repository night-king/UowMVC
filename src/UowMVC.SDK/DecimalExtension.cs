using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.SDK
{
    public static class DecimalExtension
    {
        public static string Uniform(this decimal v)
        {
            var va = v.ToString();
            var array = va.Split('.');
            if (array.Length <= 1)
            {
                return va;
            }
            var foot = "";
            var l2 = array[1];
            for (int i = 0; i < l2.Length && i < 2; i++)
            {
                foot += l2[i].ToString();
            }
            if (foot == "00") {
                return va;
            }
            if (foot.EndsWith("0"))
            {
                return va+"."+ foot[0].ToString();
            }
            return va + "." + foot;
        }
    }
}
