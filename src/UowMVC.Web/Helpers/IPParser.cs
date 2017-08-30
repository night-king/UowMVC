using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

namespace UowMVC.Web.Helpers
{
    public class SinaIPParser
    {
        public static string Parse(string ip)
        {
            var place = "";
            if (string.IsNullOrEmpty(ip) || ip == "::1")
            {
                place = "127.0.0.1";
                return place;
            }
            try
            {
                HttpClient client = new HttpClient();
                var ipresult = client.GetAsync("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + ip).Result;
                if (ipresult.IsSuccessStatusCode)
                {
                    var json = ipresult.Content.ReadAsStringAsync().Result;
                    var model = JsonConvert.DeserializeObject<SinaIPParseResult>(json);
                    return string.Format("{0}{1}{2}", model.country, model.city, model.district);
                }
            }
            catch
            {
                place = "";
            }
            return place;

        }
        class SinaIPParseResult
        {
            public int ret { set; get; }
            public int start { set; get; }
            public int end { set; get; }
            public string country { set; get; }
            public string province { set; get; }
            public string city { set; get; }
            public string district { set; get; }
            public string isp { set; get; }
            public string type { set; get; }
            public string desc { set; get; }
        }
    }
}