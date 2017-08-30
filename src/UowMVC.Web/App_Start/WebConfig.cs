using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UowMVC.Repository;
using UowMVC.Domain;
using UowMVC.Models;

namespace UowMVC.Web
{
    public static class WebConfig
    {
        public static string Domain;
        public static string ResourceFolder;
        public static string ResourceDomain;

        public static void Register()
        {
            Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            ResourceFolder = System.Configuration.ConfigurationManager.AppSettings["ResourceFolder"];
            ResourceDomain = System.Configuration.ConfigurationManager.AppSettings["ResourceDomain"];
        }
        public static object _lock = new object();
        private static Dictionary<string, Configuration> _data = new Dictionary<string, Configuration>();
        public static void Init()
        {
            lock (_lock)
            {
                var _uow = new UnitOfWork(new DefaultDataContext());
                var dataContext = new DefaultDataContext();
                var items = dataContext.Configurations.ToList();
                if (items != null)
                {
                    _data.Clear();
                    foreach (var m in items)
                    {
                        if (_data.Keys.Any(x => x == m.Key))
                        {
                            continue;
                        }
                        _data.Add(m.Key, m);
                    }
                }
            }
        }

        public static string Get(string key, string defaultValue = null)
        {
            if (!_data.Any())
            {
                Init();
            }
            lock (_lock)
            {
                if (_data.Keys.Any(x => x == key))
                {
                    return _data[key].Value;
                }
                else
                {
                    return defaultValue;
                }
            }
        }
    }

}