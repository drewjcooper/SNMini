using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNMini.Models
{
    public class ITunes
    {
        public string Author { get; set; }
        public string Subtitle { get; set; }
        public string Summary { get; set; }
        public string Keywords { get; set; }
        public string Image { get; set; }
        public Owner Owner { get; set; }
        public string Block { get; set; }
        public string Explicit { get; set; }
        public Category Category { get; set; }
        public TimeSpan Duration { get; set; }
    }
}