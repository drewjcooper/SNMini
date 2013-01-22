using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNMini.Models
{
    public class Item
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PubDate { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string Comments { get; set; }
        public ITunes ITunes { get; set; }
        public string Url { get; set; }
        public int Filesize { get; set; }
    }
}