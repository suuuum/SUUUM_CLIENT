using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUUUM_CLIENT.Item
{
    /// <summary>
    /// リストのモデルクラスです。
    /// </summary>
    public class TweetList
    {
        /// <summary>
        /// リストのID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// リストの名称
        /// </summary>
        public string Name { get; set; }

        public TweetList(long id,string name)
        {
            Id = id;
            Name = name;
        }
    }
}
