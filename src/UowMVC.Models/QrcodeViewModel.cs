using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class QrcodeViewModel
    {
        public string Id { set; get; }
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

        public DateTime CreateAt { set; get; }

        public QrcodeViewModel()
        {
        }

        public QrcodeViewModel(Qrcode entity)
        {
            Id = entity.Id;
            Content = entity.Content;
            ExpireAt = entity.ExpireAt;
            Purpose = entity.Purpose;
            ScanedCount = entity.ScanedCount;
            Creator = entity.Creator;
            CreateAt = entity.CreateAt;
            Path = entity.Path;
        }
    }
}
