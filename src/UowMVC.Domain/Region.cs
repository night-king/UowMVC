using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    public class Region : BaseEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        {
            set;
            get;
        }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            set;
            get;
        }

        /// <summary>
        /// 市
        /// </summary>
        public string City
        {
            set;
            get;
        }

        /// <summary>
        /// 县、区
        /// </summary>
        public string Area
        {
            set;
            get;
        }

        public string Detail
        {
            get
            {
                return Province + City + Area;
            }
        }
    }
}
