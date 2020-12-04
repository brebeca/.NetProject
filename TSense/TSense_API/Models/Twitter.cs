using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSense_API.Models
{
    public class Twitter
    {
        public string Link { get; set; }
        public Twitter() { }
        public Twitter(string Link)
        {
            this.Link = Link;
        }
    }
}
