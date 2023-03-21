using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUUUM_CLIENT.Item
{
    /// <summary>
    /// メディアのモデルクラスです。
    /// </summary>
    public class ImageInformation
    {
        /// <summary>
        /// 画像URL
        /// </summary>
        public string ImageUrl { get; set; }

        public long StatusId { get; set; }

        public string UserId { get; set; }

        public string TweetUrl { get; set; }
    }
}
