using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Domain
{
    /// <summary>
    /// 二维码
    /// </summary>
    public class Qrcode : FakeDeleteEntity
    {
        /// <summary>
        /// 二维码解码后的内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 二维码图片路径
        /// </summary>
        public string Path { set; get; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? ExpireAt { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAvailable
        {
            get
            {
                if (ExpireAt.HasValue)
                {
                    return ExpireAt.Value >= DateTime.Now;
                }
                return true;
            }
        }

        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose { set; get; }

        /// <summary>
        /// 扫描次数
        /// </summary>
        public int ScanedCount { set; get; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { set; get; }

    }
}
