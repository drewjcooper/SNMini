using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNMini.Models
{
    public class Category
    {
        public string Text { get; set; }
        public Category SubCategory { get; set; }
    }
}