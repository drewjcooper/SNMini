using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNMini.Models
{
    public class Channel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string ManagingEditor { get; set; }
        public string WebMaster { get; set; }
        public DateTime PubDate { get; set; }
        public DateTime LastBuildDate { get; set; }
        public int Ttl { get; set; }
        public Image Image { get; set; }
        public string AtomLink { get; set; }
        public ITunes ITunes { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}