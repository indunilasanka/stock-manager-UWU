using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int? ParentMenuId { get; set; }
        public string MenuUrl { get; set; }

        public int? MenuOrder { get; set; }
        public int? FunctionId { get; set; }
        public string MenuIcon { get; set; }
    }
}
