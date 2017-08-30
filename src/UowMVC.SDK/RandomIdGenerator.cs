using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.SDK
{
    public class RandomIdGenerator
    {
        public static string NewId(string prefix = "")
        {
            return string.Format("{0}{1}{2}{3}{4}", prefix, DateTime.Now.Year.ToString().Substring(2, 2), DateTime.Now.Month.Uniform(2), DateTime.Now.Day.Uniform(2), next().Uniform(10));
        }
        public static string GetRandomName()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(randomBytes);
            return Math.Abs(BitConverter.ToInt32(randomBytes, 0)).ToString();
        }
        /// <summary>
        /// 生成Id
        /// </summary>
        /// <returns></returns>
        private static int next()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(randomBytes);
            return Math.Abs(BitConverter.ToInt32(randomBytes, 0));
        }
    }
}
