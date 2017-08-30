using log4net;
using log4net.Layout;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UowMVC.SDK
{
    /// <summary>
    /// Class to write logging data through Log4Net.
    /// </summary>
    public class LogWriter
    {
        private ILog _log4Net = null;
        private const string DEFAULT_LOGGER_NAME = "Logger";
        /// <summary>
        /// Prevents a default instance of the <see cref="LogWriter"/> class from being created.
        /// </summary>
        /// <param name="log4NetInstance">The log4net instance to be used.</param>
        private LogWriter(ILog log4NetInstance)
        {
            _log4Net = log4NetInstance;
        }

        /// <summary>
        /// Gets a logger with the specified configuration name.
        /// </summary>
        /// <param name="configName">Name of the logger in the configuration.</param>
        /// <returns>The logger obtained.</returns>
        /// <exception cref="System.Configuration.ConfigurationException">Thrown when no logger with the specified configuration name was found.</exception>
        public static LogWriter GetLogger(string configName)
        {
            //从配置文件中读取Logger对象  
            //WebLogger 里面的配置信息是用来将日志录入到数据库的
            //做为扩展 做判断来确定日志的记录形式，数据库也好，txt文档也好，控制台程序也好。
            var logger = LogManager.GetLogger(configName);
            if (logger == null)
            {
                throw new ArgumentException(string.Format("No logger configuration named '{0}' was found in the configuration.", configName), "configName");
            }
            return new LogWriter(logger);
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        public static LogWriter Default
        {
            get
            {
                return GetLogger(DEFAULT_LOGGER_NAME);
            }
        }

        /// <summary>
        /// Writes an information level logging message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteInfo(LogContent message)
        {
            _log4Net.Info(message);
        }

        /// <summary>
        /// Writes a warning level logging message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteWarning(LogContent message)
        {
            _log4Net.Warn(message);
        }

        /// <summary>
        /// Writes a warning level logging message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="exception">The exception.</param>
        public void WriteWarning(LogContent message, System.Exception exception)
        {
            _log4Net.Warn(message, exception);
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteError(LogContent message)
        {
            _log4Net.Error(message);

        }

        /// <summary>
        /// Writes the error level logging message..
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="exception">The exception.</param>
        public void WriteError(LogContent content, System.Exception exception)
        {
            _log4Net.Error(content, exception);
        }

        /// <summary>
        /// Writes the fatal error level logging message..
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteFatal(LogContent content)
        {
            _log4Net.Fatal(content);
        }

        /// <summary>
        /// Writes the fatal error level logging message..
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="exception">The exception.</param>
        public void WriteFatal(LogContent content, System.Exception exception)
        {
            _log4Net.Fatal(content, exception);
        }

    }

    public class LogContent
    {
        public LogContent(string id, string url, string message,string action)
        {
            Id = id;
            URL = url;
            Message = message;
            Action = action;
            Status = 1;
        }

        public LogContent(string id, string url, string message, string macAddress, string computerName, string action, string client, string statuscode)
        {
            Id = id;
            URL = url;
            Message = message;
            UserIP = macAddress;
            UserName = computerName;
            Action = action;
            Client = client;
            StatusCode = statuscode;
            Status = 1;
        }
        public LogContent(HttpRequestBase request, string message, string action,string username, string statuscode)
        {
            var browser = request.Browser.Browser.ToString();
            var browser_version = request.Browser.MajorVersion.ToString();
            var browser_platform = request.Browser.Platform.ToString();
            var client = string.Format("{0}-{1}-{2}", browser, browser_version, browser_platform);

            Id = RandomIdGenerator.NewId();
            URL = request.Url.AbsoluteUri;
            Message = message;
            UserIP = request.UserHostAddress;
            UserName = username;
            Action = action;
            Client = client;
            StatusCode = statuscode;
            Status = 1;
        }
        public LogContent(HttpRequestBase request, string message, string action)
        {
            var browser = request.Browser.Browser.ToString();
            var browser_version = request.Browser.MajorVersion.ToString();
            var browser_platform = request.Browser.Platform.ToString();
            var client = string.Format("{0}-{1}-{2}", browser, browser_version, browser_platform);

            Id = RandomIdGenerator.NewId();
            URL = request.Url.AbsoluteUri;
            Message = message;
            UserIP = request.UserHostAddress;
            Action = action;
            Client = client;
            Status = 1;
        }
        public string Id { set; get; }
        public string URL { get; set; }
        public int Status { set; get; }
        /// <summary>  
        /// 访问IP  
        /// </summary>  
        public string UserIP { get; set; }

        /// <summary>  
        /// 系统登陆用户  
        /// </summary>  
        public string UserName { get; set; }

        /// <summary>  
        /// 动作事件  
        /// </summary>  
        public string Action { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string Client { set; get; }
        /// <summary>
        /// Http错误代码
        /// </summary>
        public string StatusCode { set; get; }

        public string Message { set; get; }
    }
    public class CustomPatternLayout : PatternLayout
    {
        public CustomPatternLayout()
        {
            this.AddConverter("property", typeof(LogInfoPatternConverter));
        }
    }

    public class LogInfoPatternConverter : PatternLayoutConverter
    {

        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key  
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs  
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }
        /// <summary>  
        /// 通过反射获取传入的日志对象的某个属性的值  
        /// </summary>  
        /// <param name="property"></param>  
        /// <returns></returns>  

        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            return propertyValue;
        }
    }
}
