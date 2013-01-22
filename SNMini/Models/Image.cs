using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNMini.Models
{
    public class Image
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }
    }
}