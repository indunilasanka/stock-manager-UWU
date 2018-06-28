using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Location
    {
        public string LocationName { set; get; }
        public string LocationSymbol { set; get; }
        public int LocationId { set; get; }

    }

    public class LocationViewModel
    {
        public string LocationName { set; get; }
        public string LocationSymbol { set; get; }
        public List<Location> LocationList { get; set; }

    }
}