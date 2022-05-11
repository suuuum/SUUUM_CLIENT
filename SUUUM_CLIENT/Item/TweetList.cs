using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUUUM_CLIENT.Item
{
    public class TweetList
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public TweetList(long id,string name)
        {
            Id = id;
            Name = name;
        }
    }
}
