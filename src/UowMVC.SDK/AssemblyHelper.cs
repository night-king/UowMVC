using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.SDK
{
    public class AssemblyHelper
    {
        public static IEnumerable<Assembly> Search(string folderName = null, string[] searchPattern = null)
        {
            var path = string.IsNullOrWhiteSpace(folderName)
                ? AppDomain.CurrentDomain.BaseDirectory
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

            var fileNames = new List<string>();

            searchPattern.ToList().ForEach(x =>
            {
                var files = string.IsNullOrWhiteSpace(x) ? Directory.GetFiles(path) : Directory.GetFiles(path, x);

                fileNames.AddRange(files);
            });

            return fileNames.Select(x => Assembly.LoadFile(x));
        }
    }
}
