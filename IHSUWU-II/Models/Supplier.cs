using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Supplier
    {
        public int SId { get; set; }
        public string SName { get; set; }
        public string SAddress { get; set; }
        public string SEmail { get; set; }
        public string SMobile { get; set; }
    }

    public class SupplierViewModel
    {
        public string SName { get; set; }
        public string SEmail { get; set; }
        public string SMobile { get; set; }
        public List<Supplier> SupplierList { get; set; }
    }
}